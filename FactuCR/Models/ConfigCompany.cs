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
        [Display(Name = "Nombre Compañia")]
        public string CompannyName { get; set; }

        [Required(ErrorMessage = "Debe ingresar un correo")]
        [Display(Name = "Correo electrónico")]
        public string Email { get; set; }
        [Display(Name = "Telefono")]
        public string Telephone { get; set; }
        public string Fax { get; set; }
        [Required(ErrorMessage = "Debe ingresar una provincia")]
        public string Province { get; set; }
        [Required(ErrorMessage = "Debe ingresar un cantón")]
        public string Canton { get; set; }
        [Required(ErrorMessage = "Debe ingresar un distrito")]
        public string District { get; set; }
        [Required(ErrorMessage = "Debe ingresar un barrio")]
        public string neighborhood { get; set; }
        [Required(ErrorMessage = "Debe ingresar otras señas")]
        public string OtherSigns { get; set; }
        public string Country { get; set; }
        [Required(ErrorMessage = "Debe ingresar el usuario de hacienda")]
        public string UserTax { get; set; }
        [Required(ErrorMessage = "Debe ingresar la contraseña de hacienda")]
        public string PasswordTax { get; set; }
        public string Currency { get; set; }
        public double CurrencyValue { get; set; }

        //[Required(ErrorMessage = "Debe ingresar un status")]
        //[Display(Name = "Estado")]
        public string Status { get; set; }

        [Required(ErrorMessage = "Debe ingresar un pin")]
        [Display(Name = "PIN")]
        public string pin { get; set; }
        public IdentificationType IdTypeNavigation { get; set; }
    }
}
