using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FactuCR.Models
{
    public partial class MasterInvoiceVoucher
    {
        public MasterInvoiceVoucher()
        {
            MasterDetail = new HashSet<MasterDetail>();
        }

        public int IdKey { get; set; }
        public string ApiKey { get; set; }
        public string ApiConsecutive { get; set; }

        [Required(ErrorMessage = "Debe seleccionar un método de pago")]
        public int IdPayment { get; set; }

        [Required(ErrorMessage = "Debe seleccionar la condición del pago")]
        public int IdCondition { get; set; }
        public string Status { get; set; }
        public string XmlEnviadoBase64 { get; set; }
        public string RespuestaMhbase64 { get; set; }
        public string Env { get; set; }

        public MasterSaleCondition IdConditionNavigation { get; set; }
        public MasterKey IdKeyNavigation { get; set; }
        public MasterPayment IdPaymentNavigation { get; set; }
        public MasterMonetaryDetails MasterMonetaryDetails { get; set; }
        public MasterReceiver MasterReceiver { get; set; }
        public MasterTransmitter MasterTransmitter { get; set; }
        public ICollection<MasterDetail> MasterDetail { get; set; }
    }
}
