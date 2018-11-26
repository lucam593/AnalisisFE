using System;
using System.Collections.Generic;

namespace FactuCR.Models
{
    public partial class MasterDetail
    {
        public int IdDetail { get; set; }
        public int IdKey { get; set; }
        public string Code { get; set; }
        public string NameProduct { get; set; }
        public string MeasuredUnitSymbology { get; set; }
        public int Quantity { get; set; }
        public double? DiscountAmount { get; set; }
        public double UnitPrice { get; set; }
        public double Subtotal { get; set; }
        public double TotalAmount { get; set; }
        public double TotalLineAmount { get; set; }

        public MasterInvoiceVoucher IdKeyNavigation { get; set; }
    }
}
