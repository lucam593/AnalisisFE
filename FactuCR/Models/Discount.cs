using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FactuCR.Models
{
    public partial class Discount
    {
        public Discount()
        {
            Product = new HashSet<Product>();
        }

        public int IdDiscount { get; set; }

        [Required(ErrorMessage = "Debe ingresar el nombre del descuento")]
        [Display(Name = "Nombre")]
        public string Name { get; set; }

        [Range(0, 100, ErrorMessage = "Debe ingresar un valor entre 0 y 100")]
        [Required(ErrorMessage = "Debe ingresar el porcentaje del descuento")]
        [Display(Name = "Porcentaje")]
        public int Percentage { get; set; }

        public ICollection<Product> Product { get; set; }
    }
}
