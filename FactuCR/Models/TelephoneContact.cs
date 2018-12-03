using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FactuCR.Models
{
    public partial class TelephoneContact
    {
        public int IdContact { get; set; }
        public string IdOwner { get; set; }

        [Required(ErrorMessage = "Debe ingresar un número telefónico")]
        [RegularExpression("^([0-9]{8})$", ErrorMessage = "Debe usar un formato válido (XXXXXXXX/8 dígitos)")]
        public string TelephoneNumber { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }

        [Required(ErrorMessage = "Debe seleccionar el código del país")]
        public string CountryCode { get; set; }
        public int? Extension { get; set; }
    }
}
