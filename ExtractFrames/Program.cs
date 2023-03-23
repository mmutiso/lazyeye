using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCvSharp;
using System.IO;

namespace ExtractFrames
{
    internal class Program
    {
        private static string INPUT_FILE = @"D:\\repos\\LazyEye\\LazyEyePrototype\\original.mp4";
        private static string OUTPUT_FOLDER = @"D:\repos\LazyEye\LazyEyePrototype\ModerationOutput";
        static void Main(string[] args)
        {
            VideoCapture videoCapture = new VideoCapture(INPUT_FILE);
            if (videoCapture.FrameCount <= 10)
            {
                for (int i = 0; i < videoCapture.FrameCount; i++)
                {
                    WriteFrame(videoCapture, i, OUTPUT_FOLDER);                    
                }
                return; 
            }

            foreach(int frameIndex in GenerateFrameSampleIndices(videoCapture.FrameCount))
                WriteFrame(videoCapture, frameIndex, OUTPUT_FOLDER);


            Console.WriteLine($"Done.");

            Console.ReadLine();
        }

 
        static List<int> GenerateFrameSampleIndices(int frameCount)
        {
            Random random = new Random();
            int batchSize = (frameCount / 10);
            List<int> indices = new List<int>();

            for (int i = 0; i < frameCount;)
            {
                int sampleIndex = random.Next(i, i+batchSize);

                if(sampleIndex < frameCount)
                    indices.Add(sampleIndex);   

                i += batchSize;
            }

            Console.WriteLine(string.Join(",", indices));
            return indices;
        }

        static void WriteFrame(VideoCapture videoCapture, int frameIndex, string outputBasePath)
        {
            videoCapture.Set(VideoCaptureProperties.PosFrames, frameIndex);
            Mat frame = new Mat();
            videoCapture.Read(frame);

            string framePath = Path.Combine(outputBasePath, "frame" + frameIndex.ToString() + ".jpg");
            Cv2.ImWrite(framePath, frame);
        }
    }
}
