using System;
using System.Collections.Generic;

namespace FactuCR.Models
{
    public partial class CategoryHasProduct
    {
        public int CategoryIdCategory { get; set; }
        public string ProductCodeProduct { get; set; }
        public uint ProductIdUser { get; set; }

        public Category CategoryIdCategoryNavigation { get; set; }
        public Product Product { get; set; }
    }
}
