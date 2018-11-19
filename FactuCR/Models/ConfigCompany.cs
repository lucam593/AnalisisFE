using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FactuCR.Models
{
    public partial class ConfigCompany
    {
        public int IdConfig { get; set; }

        [Required(ErrorMessage = "Debe ingresar un nombre")]
        [Display(Name = "Nombre")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Debe seleccionar el tipo de identificación")]
        [Display(Name = "Tipo de identificación")]
        public int IdType { get; set; }

        [Required(ErrorMessage = "Debe ingresar un número de identificación")]
        [RegularExpression("^([0-9]{1})?([-]?)([0-9]{3}\\2([0-9]{6}))$", ErrorMessage = "Debe usar un formato válido (X-XXX-XXXXXX)")]
        [Display(Name = "Número de identificación")]
        public string IdUser { get; set; }

        [Required(ErrorMessage = "Debe ingresar un nombre")]
        [Display(Name = "Nombre")]
        public string CompannyName { get; set; }

        [Required(ErrorMessage = "Debe ingresar un correo")]
        [Display(Name = "Correo electrónico")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Debe ingresar un número telefónico")]
        public int Telephone { get; set; }
        public string Fax { get; set; }
        public string Province { get; set; }
        public string Canton { get; set; }
        public string District { get; set; }
        public string OtherSigns { get; set; }
        public string Country { get; set; }
        public string UserTax { get; set; }
        public string PasswordTax { get; set; }
        public string Currency { get; set; }
        public double CurrencyValue { get; set; }

        public IdentificationType IdTypeNavigation { get; set; }
    }
}
