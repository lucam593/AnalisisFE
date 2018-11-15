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
    public class ProductFamiliesController : Controller
    {
        private readonly db_facturacionContext _context;

        public ProductFamiliesController(db_facturacionContext context)
        {
            _context = context;
        }

        // GET: ProductFamilies
        public async Task<IActionResult> Index()
        {
            var db_facturacionContext = _context.ProductFamily.Include(p => p.IdUserNavigation);
            return View(await db_facturacionContext.ToListAsync());
        }

        // GET: ProductFamilies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productFamily = await _context.ProductFamily
                .Include(p => p.IdUserNavigation)
                .FirstOrDefaultAsync(m => m.IdFamily == id);
            if (productFamily == null)
            {
                return NotFound();
            }

            return View(productFamily);
        }

        // GET: ProductFamilies/Create
        public IActionResult Create()
        {
            ViewData["IdUser"] = new SelectList(_context.Users, "IdUser", "About");
            return View();
        }

        // POST: ProductFamilies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdFamily,IdUser,Description,FamilyType")] ProductFamily productFamily)
        {
            if (ModelState.IsValid)
            {
                _context.Add(productFamily);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdUser"] = new SelectList(_context.Users, "IdUser", "About", productFamily.IdUser);
            return View(productFamily);
        }

        // GET: ProductFamilies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productFamily = await _context.ProductFamily.FindAsync(id);
            if (productFamily == null)
            {
                return NotFound();
            }
            ViewData["IdUser"] = new SelectList(_context.Users, "IdUser", "About", productFamily.IdUser);
            return View(productFamily);
        }

        // POST: ProductFamilies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdFamily,IdUser,Description,FamilyType")] ProductFamily productFamily)
        {
            if (id != productFamily.IdFamily)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productFamily);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductFamilyExists(productFamily.IdFamily))
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
            ViewData["IdUser"] = new SelectList(_context.Users, "IdUser", "About", productFamily.IdUser);
            return View(productFamily);
        }

        // GET: ProductFamilies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productFamily = await _context.ProductFamily
                .Include(p => p.IdUserNavigation)
                .FirstOrDefaultAsync(m => m.IdFamily == id);
            if (productFamily == null)
            {
                return NotFound();
            }

            return View(productFamily);
        }

        // POST: ProductFamilies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var productFamily = await _context.ProductFamily.FindAsync(id);
            _context.ProductFamily.Remove(productFamily);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductFamilyExists(int id)
        {
            return _context.ProductFamily.Any(e => e.IdFamily == id);
        }
    }
}
