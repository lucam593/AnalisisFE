using System;
using System.Collections.Generic;

namespace FactuCR.Models
{
    public partial class MasterLogs
    {
        public int IdLog { get; set; }
        public int IdUsers { get; set; }
        public string Log { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
