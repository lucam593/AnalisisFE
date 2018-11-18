using System;
using System.Collections.Generic;

namespace FactuCR.Models
{
    public partial class Tax
    {
        public Tax()
        {
            Product = new HashSet<Product>();
        }

        public int IdTax { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public int Percentage { get; set; }

        public ICollection<Product> Product { get; set; }
    }
}
