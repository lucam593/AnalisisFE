using System;
using System.Collections.Generic;

namespace FactuCR.Models
{
    public partial class MasterInvoice
    {
        public MasterInvoice()
        {
            MasterInvoiceReference = new HashSet<MasterInvoiceReference>();
        }

        public int IdInvoice { get; set; }
        public DateTime EmissionDate { get; set; }
        public int IdKey { get; set; }

        public MasterKey IdKeyNavigation { get; set; }
        public ICollection<MasterInvoiceReference> MasterInvoiceReference { get; set; }
    }
}
