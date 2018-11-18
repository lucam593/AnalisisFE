using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FactuCR.Models
{
    public partial class Family
    {
        public Family()
        {
            ProductHasFamily = new HashSet<ProductHasFamily>();
        }

        public int IdFamily { get; set; }

        [Required(ErrorMessage = "Debe ingresar el nombre de la Familia")]
        [Display(Name = "Nombre de la Familia")]
        public string Name { get; set; }

        [Display(Name = "Descripcion de la Familia")]
        public string Description { get; set; }

        public ICollection<ProductHasFamily> ProductHasFamily { get; set; }
    }
}

