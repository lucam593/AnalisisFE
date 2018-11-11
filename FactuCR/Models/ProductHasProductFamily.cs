using System;
using System.Collections.Generic;

namespace FactuCR.Models
{
    public partial class ProductHasProductFamily
    {
        public int ProductFamilyIdFamily { get; set; }
        public string ProductCodeProduct { get; set; }
        public uint ProductIdUser { get; set; }

        public Product Product { get; set; }
        public ProductFamily ProductFamilyIdFamilyNavigation { get; set; }
    }
}
