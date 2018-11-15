using System;
using System.Collections.Generic;

namespace FactuCR.Models
{
    public partial class Discount
    {
        public Discount()
        {
            Product = new HashSet<Product>();
        }

        public int IdDiscount { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public int Percentage { get; set; }

        public ICollection<Product> Product { get; set; }
    }
}
