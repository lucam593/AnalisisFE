using System;
using System.Collections.Generic;

namespace FactuCR.Models
{
    public partial class Product
    {
        public Product()
        {
            CategoryHasProduct = new HashSet<CategoryHasProduct>();
            Discount = new HashSet<Discount>();
            Inventory = new HashSet<Inventory>();
            ProductHasProductFamily = new HashSet<ProductHasProductFamily>();
            SalePrice = new HashSet<SalePrice>();
        }

        public int IdProduct { get; set; }
        public uint IdUser { get; set; }
        public int IdUnit { get; set; }
        public int IdBrand { get; set; }
        public int IdTax { get; set; }
        public string Name { get; set; }
        public string KindCode { get; set; }
        public string Status { get; set; }
        public double CostPrice { get; set; }
        public string Description { get; set; }
        public string Barcode { get; set; }

        public CommerciallBrand IdBrandNavigation { get; set; }
        public TaxExemption IdTaxNavigation { get; set; }
        public MeasuredUnit IdUnitNavigation { get; set; }
        public Users IdUserNavigation { get; set; }
        public ICollection<CategoryHasProduct> CategoryHasProduct { get; set; }
        public ICollection<Discount> Discount { get; set; }
        public ICollection<Inventory> Inventory { get; set; }
        public ICollection<ProductHasProductFamily> ProductHasProductFamily { get; set; }
        public ICollection<SalePrice> SalePrice { get; set; }
    }
}
