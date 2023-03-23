using Microsoft.AspNetCore.Mvc;
using LazyEye.Core;
using Microsoft.Extensions.Options;
using Microsoft.Azure.CognitiveServices.ContentModerator;
using System.Text.Json;

namespace LazyEye.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VideoEvaluatorController : Controller
    {
        private ModerationSettings _moderationSettings;

        public VideoEvaluatorController(ModerationSettings moderationSettings)
        {
            _moderationSettings = moderationSettings;
        }

        [HttpPost]
        [RequestSizeLimit(20*1024*1024)]
        public async Task<IActionResult> Evaluate([FromForm] VideoToValidateModel videoToEvaluate)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            string path = Path.Combine(Environment.CurrentDirectory, "FileStaging");
            if(!Directory.Exists(path))
                Directory.CreateDirectory(path);

           path = Path.Combine(path, Guid.NewGuid().ToString().Replace("-",""));
           using (FileStream fileStream = new FileStream(path, FileMode.Create))
           {
                if (videoToEvaluate.VideoToValidate != null)
                    await videoToEvaluate.VideoToValidate.CopyToAsync(fileStream);
           }

            ExtractFrames extractFrames = new ExtractFrames();
            var sampleFrames = extractFrames.Extract(path);

            var client = new ContentModeratorClient(new ApiKeyServiceClientCredentials(_moderationSettings.APIKEY))
            {
                Endpoint = _moderationSettings.MODERATIONENDPOINT
            };

            int counter = 1;
            foreach (var frameStream in sampleFrames)
            {
                var result = client.ImageModeration.EvaluateFileInput(frameStream);
                if (result != null)
                    if (result.IsImageAdultClassified.HasValue)
                        if (result.IsImageAdultClassified.Value)
                            return Content(JsonSerializer.Serialize(new VideoEvaluationresult(result.IsImageAdultClassified.Value, JsonSerializer.Serialize(result), counter)));

                counter++;
            }

            return Content(JsonSerializer.Serialize(new VideoEvaluationresult(false, "No adult content found in the uploaded video")));
        }
    }
}
