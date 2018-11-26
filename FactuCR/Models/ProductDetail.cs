using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FactuCR.Models
{
    public class ProductDetail
    {
        public int IdProduct { get; set; }

        public string CodeProduct { get; set; }

        public string NameProduct { get; set; }

        public double UnitPrice { get; set; }

        public int Quantity { get; set; }

        public double TotalAmount { get; set; }

        public int IdUnit { get; set; }
    }
}
