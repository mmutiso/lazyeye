using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace LazyEye.Core
{
    internal class FrameResult
    {
        public FrameResult()
        {
            
        }

        public FrameResult(string fileName, MemoryStream memoryStream)
        {
            this.FileName = fileName;
            this.FileMemoryStream = memoryStream;
        }
        public string? FileName { get; set; }
        public MemoryStream? FileMemoryStream { get; set; }
    }
}
