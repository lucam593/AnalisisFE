using System;
using System.Collections.Generic;

namespace FactuCR.Models
{
    public partial class Family
    {
        public Family()
        {
            ProductHasFamily = new HashSet<ProductHasFamily>();
        }

        public int IdFamily { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<ProductHasFamily> ProductHasFamily { get; set; }
    }
}
