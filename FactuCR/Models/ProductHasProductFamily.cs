using System;
using System.Collections.Generic;

namespace FactuCR.Models
{
    public partial class ProductHasProductFamily
    {
        public int ProductIdProduct { get; set; }
        public int ProductFamilyIdFamily { get; set; }

        public ProductFamily ProductFamilyIdFamilyNavigation { get; set; }
        public Product ProductIdProductNavigation { get; set; }
    }
}
