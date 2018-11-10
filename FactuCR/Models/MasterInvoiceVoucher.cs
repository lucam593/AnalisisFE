using System;
using System.Collections.Generic;

namespace FactuCR.Models
{
    public partial class MasterInvoiceVoucher
    {
        public MasterInvoiceVoucher()
        {
            MasterDetail = new HashSet<MasterDetail>();
            MasterMonetaryDetails = new HashSet<MasterMonetaryDetails>();
        }

        public int Idvoucher { get; set; }
        public int IdInvoice { get; set; }
        public int IdPayment { get; set; }
        public int IdCondition { get; set; }
        public int IdUser { get; set; }
        public string Status { get; set; }
        public string XmlEnviadoBase64 { get; set; }
        public string RespuestaMhbase64 { get; set; }
        public int? IdReceiver { get; set; }
        public string Env { get; set; }

        public ICollection<MasterDetail> MasterDetail { get; set; }
        public ICollection<MasterMonetaryDetails> MasterMonetaryDetails { get; set; }
    }
}
