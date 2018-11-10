using System;
using System.Collections.Generic;

namespace FactuCR.Models
{
    public partial class MasterKey
    {
        public MasterKey()
        {
            MasterInvoice = new HashSet<MasterInvoice>();
        }

        public int IdClave { get; set; }
        public int Country { get; set; }
        public string Day { get; set; }
        public string Month { get; set; }
        public string Year { get; set; }
        public string Idtransmitter { get; set; }
        public int IdConsecutivo { get; set; }
        public string SituationDocument { get; set; }
        public string CodeKey { get; set; }

        public ICollection<MasterInvoice> MasterInvoice { get; set; }
    }
}
