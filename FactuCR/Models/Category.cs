using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FactuCR.Models
{
    public partial class Category
    {
        public Category()
        {
            Product = new HashSet<Product>();
        }

        public int IdCategory { get; set; }

        [Required(ErrorMessage = "Debe ingregar el nombre de la categoría")]
        [Display(Name = "Nombre")]
        public string Name { get; set; }

        [Display(Name = "Descripcion de la categoria")]
        public string Description { get; set; }

        public ICollection<Product> Product { get; set; }
    }
}
