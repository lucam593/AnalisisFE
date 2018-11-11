using System;
using System.Collections.Generic;

namespace FactuCR.Models
{
    public partial class SalePrice
    {
        public int IdPrice { get; set; }
        public string Description { get; set; }
        public double Amount { get; set; }
        public int IdCoin { get; set; }
        public string CodeProduct { get; set; }
        public uint IdUser { get; set; }

        public Coin IdCoinNavigation { get; set; }
        public Product Product { get; set; }
    }
}
