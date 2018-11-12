using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FactuCR.Models
{
    public partial class ClientType
    {
        public int IdClientType { get; set; }

        [Required(ErrorMessage = "Debe ingresar un nombre")]
        [Display(Name = "Nombre")]
        public string Code { get; set; }

        [Required(ErrorMessage = "Debe ingresar una descripcion")]
        public string Description { get; set; }

        [Required]
        public uint UsersIdUser { get; set; }

        public Users UsersIdUserNavigation { get; set; }
    }
}
