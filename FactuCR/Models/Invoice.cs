using System;
using System.Collections.Generic;

namespace FactuCR.Models
{
    public partial class Invoice
    {
        public Invoice()
        {
            Bill = new HashSet<Bill>();
            InvoiceReference = new HashSet<InvoiceReference>();
        }

        public int IdInvoice { get; set; }
        public DateTime EmissionDate { get; set; }
        public int DocumentIdDocument { get; set; }

        public Document DocumentIdDocumentNavigation { get; set; }
        public ICollection<Bill> Bill { get; set; }
        public ICollection<InvoiceReference> InvoiceReference { get; set; }
    }
}
