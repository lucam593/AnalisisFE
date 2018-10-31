using System;
using System.Collections.Generic;

namespace FactuCR.Models
{
    public partial class InvoiceReference
    {
        public string DocumentNumber { get; set; }
        public string ReferenceCode { get; set; }
        public string Detail { get; set; }
        public int InvoiceIdInvoice { get; set; }
        public int InvoiceDocumentIdDocument { get; set; }

        public Invoice InvoiceIdInvoiceNavigation { get; set; }
    }
}
