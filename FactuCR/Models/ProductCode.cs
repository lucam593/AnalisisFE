using System;
using System.Collections.Generic;

namespace FactuCR.Models
{
    public partial class ProductCode
    {
        public string IdCode { get; set; }
        public string NameCode { get; set; }
        public int ProductIdProduct { get; set; }

        public Product ProductIdProductNavigation { get; set; }
    }
}
