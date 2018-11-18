using System;
using System.Collections.Generic;

namespace FactuCR.Models
{
    public partial class MasterCertificate
    {
        public int IdCertificate { get; set; }
        public string CommpannyName { get; set; }
        public int IdUser { get; set; }
        public int PinCertificate { get; set; }
        public string Env { get; set; }
        public string DownloadCode { get; set; }
    }
}
