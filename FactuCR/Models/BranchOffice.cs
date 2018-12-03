using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FactuCR.Models
{
    public partial class BranchOffice
    {
        public BranchOffice()
        {
            Terminal = new HashSet<Terminal>();
        }

        public int IdOffice { get; set; }

        [Required(ErrorMessage = "Debe ingresar un nombre de sucursal")]
        [Display(Name = "Nombre Sucursal")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Debe ingresar un número de sucursal")]
        [Display(Name = "Número Sucursal")]
        public string OfficeNumber { get; set; }

        public ICollection<Terminal> Terminal { get; set; }
    }
}
