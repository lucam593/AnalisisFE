using System;
using System.Collections.Generic;

namespace FactuCR.Models
{
    public partial class MasterRol
    {
        public int IdRol { get; set; }
        public uint IdUser { get; set; }
        public string Kind { get; set; }
    }
}
