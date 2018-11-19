using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FactuCR.Models
{
    public class ProductManagement
    {
        public Product Product { get; set; }

        public Category Category { get; set; }

        public Provider Provider { get; set; }

        public int IdFamily { get; set; }
        
        [Required]
        public String CategorySelection { get; set; }

        [Required(ErrorMessage = "Debe seleccionar o crear una categoría para el producto")]
        public int IdCurrentCategory { get; set; }

        [Required(ErrorMessage = "Debe ingresar un nombre para la categoría")]
        public String NameNewCategory { get; set; }

        public String DescriptionNewCategory { get; set; }
    }
}
