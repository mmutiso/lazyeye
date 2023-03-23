namespace LazyEye.Api
{
    public class VideoEvaluationresult
    {
        public VideoEvaluationresult(bool hasAdultContent, string cognitiveServicesResponse, int frameNumber = -1)
        {
            this.HasAdultContent = hasAdultContent;
            this.CognitiveServicesResponse = cognitiveServicesResponse; 
            this.FoundAtFrameNumber = frameNumber;
        }
        public bool HasAdultContent { get; set; }
        public string CognitiveServicesResponse { get; set; }
        public int FoundAtFrameNumber { get; set; }
        
    }
}
