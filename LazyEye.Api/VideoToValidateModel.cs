using System.ComponentModel.DataAnnotations;

namespace LazyEye.Api
{
    public class VideoToValidateModel
    {

        [Required]
        //[FileExtensions(Extensions = "mp4")]
        public IFormFile? VideoToValidate { get; set; }
    }
}
