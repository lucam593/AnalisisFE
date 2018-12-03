using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FactuCR.Models
{
    public partial class Address
    {
        public int idCodificacion { get; set; }
        public uint IdUser { get; set; }

        [Required(ErrorMessage = "Debe ingresar otras señales")]
        [Display(Name = "Otras señales")]
        public string OtherSigns { get; set; }
        
    }
}
