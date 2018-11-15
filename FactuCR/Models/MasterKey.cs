using System;
using System.Collections.Generic;

namespace FactuCR.Models
{
    public partial class MasterKey
    {
        public int IdKey { get; set; }
        public string Country { get; set; }
        public string Day { get; set; }
        public string Month { get; set; }
        public string Year { get; set; }
        public string IdTrasmitter { get; set; }
        public int IdConsecutive { get; set; }
        public int IdSituation { get; set; }
        public string NumberKey { get; set; }

        public MasterConsecutive IdConsecutiveNavigation { get; set; }
        public SituationDocument IdSituationNavigation { get; set; }
        public MasterInvoiceReference MasterInvoiceReference { get; set; }
        public MasterInvoiceVoucher MasterInvoiceVoucher { get; set; }
    }
}
