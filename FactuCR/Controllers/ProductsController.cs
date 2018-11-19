using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FactuCR.Models;

namespace FactuCR.Controllers
{
    public class ProductsController : Controller
    {
        private readonly db_facturacionContext _context;

        public ProductsController(db_facturacionContext context)
        {
            _context = context;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            var db_facturacionContext = _context.Product.Include(p => p.IdCategoryNavigation).Include(p => p.IdCurrencyNavigation).Include(p => p.IdDiscountNavigation).Include(p => p.IdProviderNavigation).Include(p => p.IdTaxNavigation).Include(p => p.IdUnitNavigation);
            return View(await db_facturacionContext.ToListAsync());
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .Include(p => p.IdCategoryNavigation)
                .Include(p => p.IdCurrencyNavigation)
                .Include(p => p.IdDiscountNavigation)
                .Include(p => p.IdProviderNavigation)
                .Include(p => p.IdTaxNavigation)
                .Include(p => p.IdUnitNavigation)
                .FirstOrDefaultAsync(m => m.IdProduct == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            ViewData["IdCategory"] = new SelectList(_context.Category, "IdCategory", "Name");
            ViewData["IdCurrency"] = new SelectList(_context.Currency, "IdCurrency", "Name");
            ViewData["IdDiscount"] = new SelectList(_context.Discount, "IdDiscount", "Name");
            ViewData["IdProvider"] = new SelectList(_context.Provider, "IdProvider", "Name");
            ViewData["IdUnit"] = new SelectList(_context.MeasuredUnit, "IdUnit", "Name");
            ViewData["IdTax"] = new SelectList(_context.Tax, "IdTax", "Name");

            return View();
        }

        // POST: Products/Create
        // CUSTOM MADE
        [HttpPost]
        public IActionResult Create(ProductManagement model)
        {
            ViewData["IdCategory"] = new SelectList(_context.Category, "IdCategory", "Name");
            ViewData["IdCurrency"] = new SelectList(_context.Currency, "IdCurrency", "Name");
            ViewData["IdDiscount"] = new SelectList(_context.Discount, "IdDiscount", "Name");
            ViewData["IdProvider"] = new SelectList(_context.Provider, "IdProvider", "Name");
            ViewData["IdUnit"] = new SelectList(_context.MeasuredUnit, "IdUnit", "Name");
            ViewData["IdTax"] = new SelectList(_context.Tax, "IdTax", "Name");

            Product product = new Product();
            product.CodeProduct = model.Product.CodeProduct;
            product.NameProduct = model.Product.NameProduct;
            product.Description = model.Product.Description;
            product.Status = model.Product.Status;
            product.Barcode = model.Product.Barcode;
            product.ProfitPercentage = model.Product.ProfitPercentage;
            product.CostPrice = model.Product.CostPrice;
            product.SalePrice = model.Product.SalePrice;
            product.Quantity = model.Product.Quantity;
            product.IdTax = model.Product.IdTax;
            product.IdProvider = model.Product.IdProvider;
            product.IdUnit = model.Product.IdUnit;
            product.IdDiscount = model.Product.IdDiscount;
            product.IdCurrency = model.Product.IdCurrency;

            if (model.IdCurrentCategory == 0)
            {
                Category category = new Category();
                category.Name = model.NameNewCategory;
                category.Description = model.DescriptionNewCategory;

                _context.Category.Add(category);
                _context.SaveChanges();

                Category savedCategory = _context.Category.Last();
                product.IdCategory = savedCategory.IdCategory;
            }
            else
            {
                product.IdCategory = model.IdCurrentCategory;
            }

            _context.Product.Add(product);
            _context.SaveChanges();
            /*
            Family family = _context.Family.Find(model.IdFamily);

            ProductHasFamily productHasFamily = new ProductHasFamily();
            productHasFamily.IdProductNavigation = product;
            productHasFamily.IdFamilyNavigation = family;

            product.ProductHasFamily.Add(productHasFamily);
            _context.SaveChanges();
            */
            return RedirectToAction(nameof(Index));
        }

        /* POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdProduct,CodeProduct,NameProduct,Description,ComercialBranch,Status,SalePrice,CostPrice,ProfitPercentage,Quantity,Barcode,IdTax,IdProvider,IdUnit,IdDiscount,IdCategory,IdCurrency")] Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCategory"] = new SelectList(_context.Category, "IdCategory", "Description", product.IdCategory);
            ViewData["IdCurrency"] = new SelectList(_context.Currency, "IdCurrency", "Code", product.IdCurrency);
            ViewData["IdDiscount"] = new SelectList(_context.Discount, "IdDiscount", "Name", product.IdDiscount);
            ViewData["IdProvider"] = new SelectList(_context.Provider, "IdProvider", "Email", product.IdProvider);
            ViewData["IdTax"] = new SelectList(_context.Tax, "IdTax", "Code", product.IdTax);
            ViewData["IdUnit"] = new SelectList(_context.MeasuredUnit, "IdUnit", "Name", product.IdUnit);
            return View(product);
        }
        */

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["IdCategory"] = new SelectList(_context.Category, "IdCategory", "Description", product.IdCategory);
            ViewData["IdCurrency"] = new SelectList(_context.Currency, "IdCurrency", "Code", product.IdCurrency);
            ViewData["IdDiscount"] = new SelectList(_context.Discount, "IdDiscount", "Name", product.IdDiscount);
            ViewData["IdProvider"] = new SelectList(_context.Provider, "IdProvider", "Email", product.IdProvider);
            ViewData["IdTax"] = new SelectList(_context.Tax, "IdTax", "Code", product.IdTax);
            ViewData["IdUnit"] = new SelectList(_context.MeasuredUnit, "IdUnit", "Name", product.IdUnit);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdProduct,CodeProduct,NameProduct,Description,ComercialBranch,Status,SalePrice,CostPrice,ProfitPercentage,Quantity,Barcode,IdTax,IdProvider,IdUnit,IdDiscount,IdCategory,IdCurrency")] Product product)
        {
            if (id != product.IdProduct)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.IdProduct))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCategory"] = new SelectList(_context.Category, "IdCategory", "Description", product.IdCategory);
            ViewData["IdCurrency"] = new SelectList(_context.Currency, "IdCurrency", "Code", product.IdCurrency);
            ViewData["IdDiscount"] = new SelectList(_context.Discount, "IdDiscount", "Name", product.IdDiscount);
            ViewData["IdProvider"] = new SelectList(_context.Provider, "IdProvider", "Email", product.IdProvider);
            ViewData["IdTax"] = new SelectList(_context.Tax, "IdTax", "Code", product.IdTax);
            ViewData["IdUnit"] = new SelectList(_context.MeasuredUnit, "IdUnit", "Name", product.IdUnit);
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .Include(p => p.IdCategoryNavigation)
                .Include(p => p.IdCurrencyNavigation)
                .Include(p => p.IdDiscountNavigation)
                .Include(p => p.IdProviderNavigation)
                .Include(p => p.IdTaxNavigation)
                .Include(p => p.IdUnitNavigation)
                .FirstOrDefaultAsync(m => m.IdProduct == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Product.FindAsync(id);
            _context.Product.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Product.Any(e => e.IdProduct == id);
        }
    }
}
