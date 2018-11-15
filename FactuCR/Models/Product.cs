using System;
using System.Collections.Generic;

namespace FactuCR.Models
{
    public partial class Product
    {
        public Product()
        {
            Inventary = new HashSet<Inventary>();
            ProductHasFamily = new HashSet<ProductHasFamily>();
        }

        public int IdProduct { get; set; }
        public string CodeProduct { get; set; }
        public string NameProduct { get; set; }
        public string Description { get; set; }
        public string ComercialBranch { get; set; }
        public sbyte Status { get; set; }
        public double SalePrice { get; set; }
        public double CostPrice { get; set; }
        public double ProfitPercentage { get; set; }
        public int? Quantity { get; set; }
        public string Barcode { get; set; }
        public int? IdTax { get; set; }
        public int? IdProvider { get; set; }
        public int IdUnit { get; set; }
        public int? IdDiscount { get; set; }
        public int? IdCategory { get; set; }
        public int IdCurrency { get; set; }

        public Category IdCategoryNavigation { get; set; }
        public Currency IdCurrencyNavigation { get; set; }
        public Discount IdDiscountNavigation { get; set; }
        public Provider IdProviderNavigation { get; set; }
        public Tax IdTaxNavigation { get; set; }
        public MeasuredUnit IdUnitNavigation { get; set; }
        public ICollection<Inventary> Inventary { get; set; }
        public ICollection<ProductHasFamily> ProductHasFamily { get; set; }
    }
}
