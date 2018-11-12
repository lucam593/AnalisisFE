using System;
using System.Collections.Generic;

namespace FactuCR.Models
{
    public partial class MasterMonetaryDetails
    {
        public string IdMonetary { get; set; }
        public int Idvoucher { get; set; }
        public double ValueCoin { get; set; }
        public string CodeCoin { get; set; }
        public double? TotalTaxedServices { get; set; }
        public double? TotalExemptServices { get; set; }
        public double? TotalTaxedGoods { get; set; }
        public double? TotalExemptFreight { get; set; }
        public double? TotalTaxed { get; set; }
        public double? TotalExempt { get; set; }
        public double TotalSale { get; set; }
        public double TotalDiscount { get; set; }
        public double TotalNetSale { get; set; }
        public double TotalTax { get; set; }
        public double Total { get; set; }

        public MasterInvoiceVoucher IdvoucherNavigation { get; set; }
    }
}
