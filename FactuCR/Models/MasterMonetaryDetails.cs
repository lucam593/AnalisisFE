using System;
using System.Collections.Generic;

namespace FactuCR.Models
{
    public partial class MasterMonetaryDetails
    {
        public int IdKey { get; set; }
        public double ValueCurrency { get; set; }
        public string CodeCurrency { get; set; }
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
        public double TotalFinal { get; set; }

        public MasterInvoiceVoucher IdKeyNavigation { get; set; }
    }
}
