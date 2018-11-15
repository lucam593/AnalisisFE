using System;
using System.Collections.Generic;

namespace FactuCR.Models
{
    public partial class MasterSaleCondition
    {
        public MasterSaleCondition()
        {
            MasterInvoiceVoucher = new HashSet<MasterInvoiceVoucher>();
        }

        public int IdCondition { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }

        public ICollection<MasterInvoiceVoucher> MasterInvoiceVoucher { get; set; }
    }
}
