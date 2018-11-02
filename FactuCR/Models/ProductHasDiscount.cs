using System;
using System.Collections.Generic;

namespace FactuCR.Models
{
    public partial class ProductHasDiscount
    {
        public int ProductIdProduct { get; set; }
        public int DiscountIdDiscount { get; set; }

        public Discount DiscountIdDiscountNavigation { get; set; }
        public Product ProductIdProductNavigation { get; set; }
    }
}
