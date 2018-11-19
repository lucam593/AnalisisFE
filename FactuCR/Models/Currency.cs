using System;
using System.Collections.Generic;

namespace FactuCR.Models
{
    public partial class Currency
    {
        public Currency()
        {
            Product = new HashSet<Product>();
        }

        public int IdCurrency { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Country { get; set; }
        public double? CurrentChange { get; set; }

        public ICollection<Product> Product { get; set; }
    }
}
