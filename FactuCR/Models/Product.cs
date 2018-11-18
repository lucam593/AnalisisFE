using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FactuCR.Models
{
    public partial class Product
    {
        public Product()
        {
            Inventory = new HashSet<Inventory>();
            ProductHasFamily = new HashSet<ProductHasFamily>();
        }

        public int IdProduct { get; set; }


        [Required(ErrorMessage = "Debe ingresar un codigo de producto")]
        [Display(Name = "Codigo del producto")]
        public string CodeProduct { get; set; }

        [Required]
        [Display(Name = "Nombre del producto")]
        public string NameProduct { get; set; }

        [Required]
        [Display(Name = "Descripcion")]
        public string Description { get; set; }
        public sbyte Status { get; set; }

        [Required]
        [Display(Name = "Precio de venta")]
        public double SalePrice { get; set; }

        [Required]
        [RegularExpression("^[0-9]+(\\.[0-9]+)?$", ErrorMessage = "Debe ingresar un valor válido (Por ejemplo: 100 o 110.5)")]
        [Display(Name = "Precio de costo")]
        public double CostPrice { get; set; }

        [Required]
        [Display(Name = "Porcentaje de ganancia")]
        public double ProfitPercentage { get; set; }

        [Required]
        [Display(Name = "Cantidad")]
        public int? Quantity { get; set; }
        
        [Display(Name = "Código de barras")]
        public string Barcode { get; set; }

        [Required]
        [Display(Name = "Tipo de impuesto")]
        public int? IdTax { get; set; }

        [Required]
        [Display(Name = "Proveedor")]
        public int? IdProvider { get; set; }

        [Required]
        [Display(Name = "Unidad de medida")]
        public int IdUnit { get; set; }
        public int? IdDiscount { get; set; }

        [Required(ErrorMessage = "Debe seleccionar o crear una categoría para el producto")]
        [Display(Name = "Categoría")]
        public int? IdCategory { get; set; }

        [Required]
        [Display(Name = "Moneda")]
        public int IdCurrency { get; set; }

        public Category IdCategoryNavigation { get; set; }
        public Currency IdCurrencyNavigation { get; set; }
        public Discount IdDiscountNavigation { get; set; }
        public Provider IdProviderNavigation { get; set; }
        public Tax IdTaxNavigation { get; set; }
        public MeasuredUnit IdUnitNavigation { get; set; }
        public ICollection<Inventory> Inventory { get; set; }
        public ICollection<ProductHasFamily> ProductHasFamily { get; set; }
    }
}
