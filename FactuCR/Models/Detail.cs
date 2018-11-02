using System;
using System.Collections.Generic;

namespace FactuCR.Models
{
    public partial class Detail
    {
        public int IdDetail { get; set; }
        public int BillInvoiceIdInvoice { get; set; }
        public int MeasuredUnitIdUnit { get; set; }
        public int ProductIdProduct { get; set; }
        public string TypeCode { get; set; }
        public string Code { get; set; }
        public int Quantity { get; set; }
        public double? Discount { get; set; }
        public string TypeDiscount { get; set; }
        public double InitialAmount { get; set; }
        public double Subtotal { get; set; }
        public double TotalAmount { get; set; }

        public MeasuredUnit MeasuredUnitIdUnitNavigation { get; set; }
        public Product ProductIdProductNavigation { get; set; }
    }
}
