using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FactuCR.Models
{
    public partial class ProductsManagement
    {
        public Product Product { get; set; }
        
        [Required(ErrorMessage = "Debe seleccionar una categoria")]
        public int IdCategory { get; set; }

        [Required(ErrorMessage = "Debe seleccionar una marca")]
        public int IdBrand { get; set; }

        [Required(ErrorMessage = "Debe seleccionar una unidad")]
        public int IdUnit { get; set; }

        [Required(ErrorMessage = "Debe seleccionar un tipo de impuesto")]
        public int IdTax { get; set; }
    }
}
