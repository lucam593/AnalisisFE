using System;
using System.Collections.Generic;

namespace FactuCR.Models
{
    public partial class Category
    {
        public Category()
        {
            CategoryHasProduct = new HashSet<CategoryHasProduct>();
        }

        public int IdCategory { get; set; }
        public string Description { get; set; }

        public ICollection<CategoryHasProduct> CategoryHasProduct { get; set; }
    }
}
