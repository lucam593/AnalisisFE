using System;
using System.Collections.Generic;

namespace FactuCR.Models
{
    public partial class TaxExemption
    {
        public int Code { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Percentage { get; set; }
    }
}
