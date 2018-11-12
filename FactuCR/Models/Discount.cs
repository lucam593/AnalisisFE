using System;
using System.Collections.Generic;

namespace FactuCR.Models
{
    public partial class Discount
    {
        public int IdDiscount { get; set; }
        public int IdProduct { get; set; }
        public string Type { get; set; }
        public int Percentage { get; set; }
        public string Description { get; set; }
        public string CodeDiscount { get; set; }
        public string Discountcol { get; set; }

        public Product IdProductNavigation { get; set; }
    }
}
