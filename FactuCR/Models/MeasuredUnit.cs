using System;
using System.Collections.Generic;

namespace FactuCR.Models
{
    public partial class MeasuredUnit
    {
        public MeasuredUnit()
        {
            Detail = new HashSet<Detail>();
        }

        public int IdUnit { get; set; }
        public string Symboly { get; set; }
        public string Name { get; set; }

        public ICollection<Detail> Detail { get; set; }
    }
}
