using System;
using System.Collections.Generic;

namespace FactuCR.Models
{
    public partial class Discount
    {
        public int IdDiscount { get; set; }
        public string ProductCodeProduct { get; set; }
        public uint ProductIdUser { get; set; }
        public string Type { get; set; }
        public int Percentage { get; set; }
        public string Description { get; set; }
        public string CodeDiscount { get; set; }
        public string Discountcol { get; set; }

        public Product Product { get; set; }
    }
}
