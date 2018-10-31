using System;
using System.Collections.Generic;

namespace FactuCR.Models
{
    public partial class Product
    {
        public Product()
        {
            CategoryHasProduct = new HashSet<CategoryHasProduct>();
            Detail = new HashSet<Detail>();
            Inventory = new HashSet<Inventory>();
            ProductCode = new HashSet<ProductCode>();
            ProductHasDiscount = new HashSet<ProductHasDiscount>();
            ProductHasProductFamily = new HashSet<ProductHasProductFamily>();
            SalePrice = new HashSet<SalePrice>();
        }

        public int IdProduct { get; set; }
        public int CommercialBrandIdBrand { get; set; }
        public string Description { get; set; }
        public double CostPrice { get; set; }
        public double IVA { get; set; }

        public CommerciallBrand CommercialBrandIdBrandNavigation { get; set; }
        public ICollection<CategoryHasProduct> CategoryHasProduct { get; set; }
        public ICollection<Detail> Detail { get; set; }
        public ICollection<Inventory> Inventory { get; set; }
        public ICollection<ProductCode> ProductCode { get; set; }
        public ICollection<ProductHasDiscount> ProductHasDiscount { get; set; }
        public ICollection<ProductHasProductFamily> ProductHasProductFamily { get; set; }
        public ICollection<SalePrice> SalePrice { get; set; }
    }
}
