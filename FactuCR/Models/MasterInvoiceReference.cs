using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FactuCR.Models
{
    public partial class MasterInvoiceReference
    {
        public int IdKey { get; set; }
        
        [Display(Name = "Tipo de Documento")]
        public string Document_Type { get; set; }
        
        [Display(Name = "Clave")]
        public string DocumentNumber { get; set; }
        
        [Display(Name = "Tipo de Referencia")]
        public string Type_Reference { get; set; }
        
        [Display(Name = "Codigo de referencia")]
        public string ReferenceCode { get; set; }
        
        [Display(Name = "Razón")]
        public string Detail { get; set; }
        public string RespuestaMhbase64 { get; set; }
        public string XmlenviadoBase64 { get; set; }

        public MasterKey IdKeyNavigation { get; set; }
    }
}
