using System;
using System.Collections.Generic;

namespace FactuCR.Models
{
    public partial class MasterInvoiceReference
    {
        public string IdReference { get; set; }
        public int IdInvoice { get; set; }
        public string DocumentNumber { get; set; }
        public string ReferenceCode { get; set; }
        public string Detail { get; set; }
        public string RespuestaMhbase64 { get; set; }
        public string XmlenviadoBase64 { get; set; }

        public MasterInvoice IdInvoiceNavigation { get; set; }
    }
}
