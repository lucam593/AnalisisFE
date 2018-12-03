using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FactuCR.Models
{
    public class ReferenceManagement
    {
        public MasterInvoiceReference MasterInvoiceReference { get; set; }

        public DateTime date { get; set; }

        public MasterKey Keyreference { get; set; }

        public int key { get; set; }

        public string situation { get; set; }

        public List<MasterInvoiceVoucher> invoiceVouchers { get; set; }
    }
}
