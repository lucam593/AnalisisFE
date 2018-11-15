using System;
using System.Collections.Generic;

namespace FactuCR.Models
{
    public partial class Sessions
    {
        public uint IdSession { get; set; }
        public uint IdUser { get; set; }
        public string SessionKey { get; set; }
        public string Ip { get; set; }
        public uint LastAccess { get; set; }
    }
}
