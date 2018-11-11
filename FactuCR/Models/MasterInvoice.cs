using System;
using System.Collections.Generic;

namespace FactuCR.Models
{
    public partial class MasterInvoice
    {
        public int IdClave { get; set; }
        public DateTime EmissionDate { get; set; }

        public MasterKey IdClaveNavigation { get; set; }
        public MasterInvoiceReference MasterInvoiceReference { get; set; }
    }
}
