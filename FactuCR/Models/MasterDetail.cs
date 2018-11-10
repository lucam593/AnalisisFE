using System;
using System.Collections.Generic;

namespace FactuCR.Models
{
    public partial class MasterDetail
    {
        public int IdDetail { get; set; }
        public int Idvoucher { get; set; }
        public string TypeCode { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string MeasuredUnit { get; set; }
        public int Quantity { get; set; }
        public double? Discount { get; set; }
        public string TypeDiscount { get; set; }
        public double InitialAmount { get; set; }
        public double Subtotal { get; set; }
        public double TotalAmount { get; set; }

        public MasterInvoiceVoucher IdvoucherNavigation { get; set; }
    }
}
