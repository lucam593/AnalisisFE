using System;
using System.Collections.Generic;

namespace FactuCR.Models
{
    public partial class Discount
    {
        public Discount()
        {
            ProductHasDiscount = new HashSet<ProductHasDiscount>();
        }

        public int IdDiscount { get; set; }
        public string Type { get; set; }
        public int Percentage { get; set; }
        public string Description { get; set; }

        public ICollection<ProductHasDiscount> ProductHasDiscount { get; set; }
    }
}
