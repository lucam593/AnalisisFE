using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FactuCR.Models
{
    public partial class Provider
    {
        public Provider()
        {
            Product = new HashSet<Product>();
        }
        
        public int IdProvider { get; set; }

        [Required(ErrorMessage = "Debe seleccionar el tipo de identificación")]
        [Display(Name = "Tipo de identificación")]
        public int IdType { get; set; }

        [Required(ErrorMessage = "Debe ingresar un número de identificación")]
        [RegularExpression("^([0-9]{1})?([-]?)([0-9]{3}\\2([0-9]{6}))$", ErrorMessage = "Debe usar un formato válido (X-XXX-XXXXXX)")]
        [Display(Name = "Número de identificación")]
        public string IdentificationNumber { get; set; }

        [Required(ErrorMessage = "Debe ingresar un nombre")]
        [Display(Name = "Nombre")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Debe ingresar un correo")]
        [EmailAddress(ErrorMessage = "Debe ingresar un correo válido (usuario@correo.com)")]
        [Display(Name = "Correo electrónico")]
        public string Email { get; set; }
        public string Description { get; set; }

        public IdentificationType IdTypeNavigation { get; set; }
        public ICollection<Product> Product { get; set; }
    }
}
