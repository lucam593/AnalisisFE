using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FactuCR.Models
{
    public partial class Category
    {
        public Category()
        {
            CategoryHasProduct = new HashSet<CategoryHasProduct>();
        }

        public int IdCategory { get; set; }

        [Required]
        public uint IdUser { get; set; }

        [Required(ErrorMessage = "Ingrese el nombre")]
        [Display(Name = "Nombre")]
        public string Description { get; set; }

        public Users IdUserNavigation { get; set; }
        public ICollection<CategoryHasProduct> CategoryHasProduct { get; set; }
    }
}
