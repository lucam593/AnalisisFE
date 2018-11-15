using System;
using System.Collections.Generic;

namespace FactuCR.Models
{
    public partial class Files
    {
        public uint IdFile { get; set; }
        public string Md5 { get; set; }
        public string Name { get; set; }
        public uint Timestamp { get; set; }
        public uint Size { get; set; }
        public uint IdUser { get; set; }
        public string DownloadCode { get; set; }
        public string FileType { get; set; }
        public string Type { get; set; }
    }
}
