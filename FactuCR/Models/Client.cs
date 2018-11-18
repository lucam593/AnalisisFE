using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FactuCR.Models
{
    public partial class Client
    {

        [Display(Name = "id")]
        public int IdClient { get; set; }

        [Required(ErrorMessage = "Debe seleccionar un tipo de identificacion")]
        [Display(Name = "Tipo de Identificacion")]
        public int IdType { get; set; }

        [Required(ErrorMessage = "Debe ingresar un numero identificacion")]
        [RegularExpression("^([0-9]{1})?([-]?)([0-9]{3}\\2([0-9]{6}))$", ErrorMessage = "Debe usar un formato válido (X-XXX-XXXXXX)")]
        [Display(Name = "Numero de Identificacion")]
        public string IdentificationNumber { get; set; }

        [Required(ErrorMessage = "Debe ingresar el nombre")]
        [Display(Name = "Nombre")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Debe ingresar el Apellido")]
        [Display(Name = "Apellido")]
        public string LastName { get; set; }

        [EmailAddress(ErrorMessage = "Debe ingresar un correo electronico valido")]
        [Required(ErrorMessage = "Debe ingresar un correo electronico")]
        [Display(Name = "Correo Electronico")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Ingrese un pais")]
        [Display(Name = "Pais")]
        public string Country { get; set; }

        
        [Display(Name = "estado")]
        public sbyte Status { get; set; }

      
        [Display(Name = "Fecha de Ingreso")]       
        [DataType(DataType.Date)]
        public DateTime AdmissionDate { get; set; }

        public IdentificationType IdTypeNavigation { get; set; }
    }
}
