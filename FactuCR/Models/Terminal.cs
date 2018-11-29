using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FactuCR.Models
{
    public partial class Terminal
    {
        public int IdTerminal { get; set; }

        [Required(ErrorMessage = "Debe ingresar un nombre de terminal")]
        [Display(Name = "Nombre Terminal")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Debe ingresar un número terminal")]
        [Display(Name = "Nombre Terminal")]
        public int TerminalNumber { get; set; }

        [Required(ErrorMessage = "Debe ingresar un número de sucursal")]
        [Display(Name = "Número Sucursal")]
        public int IdOffice { get; set; }

        public BranchOffice IdOfficeNavigation { get; set; }
    }
}
