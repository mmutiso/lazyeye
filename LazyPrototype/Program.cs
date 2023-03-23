using System;
using System.Net;
using LazyEye.Core;
using Microsoft.Azure.CognitiveServices.ContentModerator;

namespace LazyPrototype 
{
    internal class Program
    {
        private const string INPUT_FILE = @"D:\repos\LazyEye\LazyPrototype\RawFiles\bad1.mp4";
        private const string API_KEY = "";
        private const string ENDPOINT = "";
        static void Main(string[] args)
        {

            Console.WriteLine("Hello frames");

            ExtractFrames framesFactory = new ExtractFrames();

            var frames  = framesFactory.Extract(INPUT_FILE);

            frames.ForEach(frame =>
            {
                AnalyzeImageForAdultContent(frame);
            });

            Console.WriteLine(string.Join("\n", frames));
            Console.ReadLine();
        }

        static bool? AnalyzeImageForAdultContent(MemoryStream imageMemoryStream)
        {
            var client = new ContentModeratorClient(new ApiKeyServiceClientCredentials(API_KEY))
            {
                Endpoint = ENDPOINT
            };

            var result = client.ImageModeration.EvaluateFileInput(imageMemoryStream);

            Console.WriteLine($"Is Adult: {result.IsImageAdultClassified}");

            return result.IsImageAdultClassified;
        }
    }
    
}