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
            var db_facturacionContext = _context.Product.Include(p => p.IdBrandNavigation).Include(p => p.IdTaxNavigation).Include(p => p.IdUnitNavigation).Include(p => p.IdUserNavigation);
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
                .Include(p => p.IdBrandNavigation)
                .Include(p => p.IdTaxNavigation)
                .Include(p => p.IdUnitNavigation)
                .Include(p => p.IdUserNavigation)
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
            int currentUser = 1234;

            List<Category> list = _context.Category.Where(o => o.IdUser == currentUser).ToList();
            ViewBag.Categories = new SelectList(list, "IdCategory", "Description");

            List<CommerciallBrand> brandsList = _context.CommerciallBrand.ToList();
            ViewBag.Brands = new SelectList(brandsList, "IdBrand", "Name");

            List<MeasuredUnit> unitsList = _context.MeasuredUnit.ToList();
            ViewBag.Units = new SelectList(unitsList, "IdUnit", "Name");

            List<TaxExemption> taxesList = _context.TaxExemption.ToList();
            ViewBag.Taxes = new SelectList(taxesList, "IdTax", "Name");

            return View();
        }

        // POST: Products/Create
        // CUSTOM
        [HttpPost]
        public IActionResult Create(ProductsManagement model)
        {
            int currentUser = 1234;

            List<Category> categorieslist = _context.Category.Where(o => o.IdUser == currentUser).ToList();
            ViewBag.Categories = new SelectList(categorieslist, "IdCategory", "Description");

            List<CommerciallBrand> brandsList = _context.CommerciallBrand.ToList();
            ViewBag.Brands = new SelectList(brandsList, "IdBrand", "Name");

            List<MeasuredUnit> unitsList = _context.MeasuredUnit.ToList();
            ViewBag.Units = new SelectList(unitsList, "IdUnit", "Name");

            List<TaxExemption> taxesList = _context.TaxExemption.ToList();
            ViewBag.Taxes = new SelectList(taxesList, "IdTax", "Name");

            Product product = new Product();
            product.IdUser = model.Product.IdUser;
            product.KindCode = model.Product.KindCode;
            product.CostPrice = model.Product.CostPrice;
            product.Name = model.Product.Name;
            product.Description = model.Product.Description;
            product.IdBrand = model.IdBrand;
            product.IdUnit = model.IdUnit;
            product.IdTax = model.IdTax;
            product.Barcode = model.Product.Barcode;
            product.Status = model.Product.Status;
            
            _context.Product.Add(product);
            _context.SaveChanges();
            

            int lastProductId = model.Product.IdProduct;

            Category category = _context.Category.Find(model.IdCategory);

            CategoryHasProduct productWithCategory = new CategoryHasProduct();
            productWithCategory.CategoryIdCategoryNavigation = category;
            productWithCategory.ProductIdProductNavigation = product;

            category.CategoryHasProduct.Add(productWithCategory);
            _context.SaveChanges();
            
            return RedirectToAction(nameof(Index));
        }

        public String GetTaxName(int code)
        {
            String name = _context.TaxExemption.Find(code).Name;

            return name;
        }

        /* POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdProduct,CodeProduct,IdUser,IdUnit,IdBrand,IdTax,Name,KindCode,Status,CostPrice,Description,Barcode")] Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdBrand"] = new SelectList(_context.CommerciallBrand, "IdBrand", "IdProvider", product.IdBrand);
            ViewData["IdTax"] = new SelectList(_context.TaxExemption, "IdTax", "Code", product.IdTax);
            ViewData["IdUnit"] = new SelectList(_context.MeasuredUnit, "IdUnit", "Name", product.IdUnit);
            ViewData["IdUser"] = new SelectList(_context.Users, "IdUser", "About", product.IdUser);
            return View(product);
        }*/

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
            ViewData["IdBrand"] = new SelectList(_context.CommerciallBrand, "IdBrand", "IdProvider", product.IdBrand);
            ViewData["IdTax"] = new SelectList(_context.TaxExemption, "IdTax", "Code", product.IdTax);
            ViewData["IdUnit"] = new SelectList(_context.MeasuredUnit, "IdUnit", "Name", product.IdUnit);
            ViewData["IdUser"] = new SelectList(_context.Users, "IdUser", "About", product.IdUser);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdProduct,CodeProduct,IdUser,IdUnit,IdBrand,IdTax,Name,KindCode,Status,CostPrice,Description,Barcode")] Product product)
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
            ViewData["IdBrand"] = new SelectList(_context.CommerciallBrand, "IdBrand", "IdProvider", product.IdBrand);
            ViewData["IdTax"] = new SelectList(_context.TaxExemption, "IdTax", "Code", product.IdTax);
            ViewData["IdUnit"] = new SelectList(_context.MeasuredUnit, "IdUnit", "Name", product.IdUnit);
            ViewData["IdUser"] = new SelectList(_context.Users, "IdUser", "About", product.IdUser);
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
                .Include(p => p.IdBrandNavigation)
                .Include(p => p.IdTaxNavigation)
                .Include(p => p.IdUnitNavigation)
                .Include(p => p.IdUserNavigation)
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
