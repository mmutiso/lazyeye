using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.IO;

namespace LazyEye.Core
{
    public class ExtractFrames
    {
        public List<MemoryStream> Extract(string videoFilePath)
        {
            List<MemoryStream> frames = new List<MemoryStream>();
            VideoCapture videoCapture = new VideoCapture(videoFilePath);
            if (videoCapture.FrameCount <= 10)
            {
                
                for (int i = 0; i < videoCapture.FrameCount; i++)
                {
                    frames.Add(WriteFrame(videoCapture, i));
                }
                return frames;
            }

            foreach (int frameIndex in GenerateFrameSampleIndices(videoCapture.FrameCount))
                frames.Add(WriteFrame(videoCapture, frameIndex));

            return frames;
        }
        static List<int> GenerateFrameSampleIndices(int frameCount)
        {
            Random random = new Random();
            int batchSize = (frameCount / 10);
            List<int> indices = new List<int>();

            for (int i = 0; i < frameCount;)
            {
                int sampleIndex = random.Next(i, i + batchSize);

                if (sampleIndex < frameCount)
                    indices.Add(sampleIndex);

                i += batchSize;
            }

            Console.WriteLine(string.Join(",", indices));
            return indices;
        }

        static MemoryStream WriteFrame(VideoCapture videoCapture, int frameIndex)
        {
            videoCapture.Set(VideoCaptureProperties.PosFrames, frameIndex);
            Mat frame = new Mat();
            videoCapture.Read(frame);

            //string framePath = Path.Combine(outputBasePath, "frame" + frameIndex.ToString() + ".jpg");
            //Cv2.ImWrite(framePath, frame);

            var memoryStream = frame.ToMemoryStream(".jpg");

            return memoryStream;
        }
    }
}
