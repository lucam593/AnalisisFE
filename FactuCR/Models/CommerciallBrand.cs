using System;
using System.Collections.Generic;

namespace FactuCR.Models
{
    public partial class CommerciallBrand
    {
        public CommerciallBrand()
        {
            Product = new HashSet<Product>();
        }

        public int IdBrand { get; set; }
        public string IdProvider { get; set; }
        public string Name { get; set; }

        public Provider IdProviderNavigation { get; set; }
        public ICollection<Product> Product { get; set; }
    }
}
