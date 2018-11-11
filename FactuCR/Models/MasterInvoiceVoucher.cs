using System;
using System.Collections.Generic;

namespace FactuCR.Models
{
    public partial class MasterInvoiceVoucher
    {
        public MasterInvoiceVoucher()
        {
            MasterDetail = new HashSet<MasterDetail>();
        }

        public int IdClave { get; set; }
        public int IdPayment { get; set; }
        public int IdCondition { get; set; }
        public string Status { get; set; }
        public string XmlEnviadoBase64 { get; set; }
        public string RespuestaMhbase64 { get; set; }
        public string Env { get; set; }

        public MasterMonetaryDetails MasterMonetaryDetails { get; set; }
        public ICollection<MasterDetail> MasterDetail { get; set; }
    }
}
