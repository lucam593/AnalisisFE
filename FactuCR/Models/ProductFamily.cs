using System;
using System.Collections.Generic;

namespace FactuCR.Models
{
    public partial class ProductFamily
    {
        public ProductFamily()
        {
            ProductHasProductFamily = new HashSet<ProductHasProductFamily>();
        }

        public int IdFamily { get; set; }
        public string Description { get; set; }
        public string FamilyType { get; set; }

        public ICollection<ProductHasProductFamily> ProductHasProductFamily { get; set; }
    }
}
