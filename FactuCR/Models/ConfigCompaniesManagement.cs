using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace FactuCR.Models
{
    public class ConfigCompaniesManagement
    {
        public MasterAddress Address { get; set; }
        public Currency Currency { get; set; }
        public ConfigCompany configCompany { get; set; }

        [Required(ErrorMessage = "Debe ingresar un número de identificación")]
        [RegularExpression("^([0-9]{1})?([-]?)([0-9]{4}\\2([0-9]{4}))$", ErrorMessage = "Debe usar un formato válido (X-XXXX-XXXX)")]
        [Display(Name = "Número de identificación")]
        public string IdentificationNumberCedFisica { get; set; }

        [Required(ErrorMessage = "Debe ingresar un número de identificación")]
        [RegularExpression("^([0-9]{1})?([-]?)([0-9]{3}\\2([0-9]{6}))$", ErrorMessage = "Debe usar un formato válido (X-XXX-XXXXXX)")]
        [Display(Name = "Número de identificación")]
        public string IdentificationNumberCedJuridica { get; set; }

        [Required(ErrorMessage = "Debe ingresar un número de identificación")]
        [RegularExpression("^([0-9]{10})$", ErrorMessage = "Debe usar un formato válido (XXXXXXXXXX/10 dígitos)")]
        [Display(Name = "Número de identificación")]
        public string IdentificationNumberNITE { get; set; }

        [Required(ErrorMessage = "Debe ingresar un número de identificación")]
        [RegularExpression("^([0-9]{11,12})$", ErrorMessage = "Debe usar un formato válido (XXXXXXXXXX/11 o 12 dígitos)")]
        [Display(Name = "Número de identificación")]
        public string IdentificationNumberDIMEX { get; set; }


    }
}
