using System;
using System.Collections.Generic;

namespace FactuCR.Models
{
    public partial class MeasuredUnit
    {
        public MeasuredUnit()
        {
            Product = new HashSet<Product>();
        }

        public int IdUnit { get; set; }
        public string Symbology { get; set; }
        public string Name { get; set; }

        public ICollection<Product> Product { get; set; }
    }
}
