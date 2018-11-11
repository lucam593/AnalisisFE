using System;
using System.Collections.Generic;

namespace FactuCR.Models
{
    public partial class Coin
    {
        public Coin()
        {
            SalePrice = new HashSet<SalePrice>();
        }

        public int IdCoin { get; set; }
        public string Code { get; set; }
        public string TypeCoin { get; set; }
        public string Symbology { get; set; }
        public double CurrentChange { get; set; }
        public DateTime LastModification { get; set; }
        public string Country { get; set; }

        public ICollection<SalePrice> SalePrice { get; set; }
    }
}
