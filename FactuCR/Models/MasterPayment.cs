using System;
using System.Collections.Generic;

namespace FactuCR.Models
{
    public partial class MasterPayment
    {
        public MasterPayment()
        {
            MasterInvoiceVoucher = new HashSet<MasterInvoiceVoucher>();
        }

        public int IdPayment { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }

        public ICollection<MasterInvoiceVoucher> MasterInvoiceVoucher { get; set; }
    }
}
