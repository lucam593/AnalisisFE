using System;
using System.Collections.Generic;

namespace FactuCR.Models
{
    public partial class CategoryHasProduct
    {
        public int CategoryIdCategory { get; set; }
        public int ProductIdProduct { get; set; }

        public Category CategoryIdCategoryNavigation { get; set; }
        public Product ProductIdProductNavigation { get; set; }
    }
}
