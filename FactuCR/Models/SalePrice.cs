using System;
using System.Collections.Generic;

namespace FactuCR.Models
{
    public partial class SalePrice
    {
        public int IdPrice { get; set; }
        public string Description { get; set; }
        public double Amount { get; set; }
        public int ProductIdProduct { get; set; }
        public int CoinIdCoin { get; set; }

        public Coin CoinIdCoinNavigation { get; set; }
        public Product ProductIdProductNavigation { get; set; }
    }
}
