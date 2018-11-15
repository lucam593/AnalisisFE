using System;
using System.Collections.Generic;

namespace FactuCR.Models
{
    public partial class ProductHasFamily
    {
        public int IdProduct { get; set; }
        public int IdFamily { get; set; }

        public Family IdFamilyNavigation { get; set; }
        public Product IdProductNavigation { get; set; }
    }
}
