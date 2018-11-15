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
    public class InventoriesController : Controller
    {
        private readonly db_facturacionContext _context;

        public InventoriesController(db_facturacionContext context)
        {
            _context = context;
        }

        // GET: Inventories
        public async Task<IActionResult> Index()
        {
            var db_facturacionContext = _context.Inventory.Include(i => i.IdProductNavigation).Include(i => i.IdUserNavigation);
            return View(await db_facturacionContext.ToListAsync());
        }

        // GET: Inventories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inventory = await _context.Inventory
                .Include(i => i.IdProductNavigation)
                .Include(i => i.IdUserNavigation)
                .FirstOrDefaultAsync(m => m.IdInventary == id);
            if (inventory == null)
            {
                return NotFound();
            }

            return View(inventory);
        }

        // GET: Inventories/Create
        public IActionResult Create()
        {
            ViewData["IdProduct"] = new SelectList(_context.Product, "IdProduct", "KindCode");
            ViewData["IdUser"] = new SelectList(_context.Users, "IdUser", "About");
            return View();
        }

        // POST: Inventories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdInventary,IdUser,IdProduct,MovementType,Cuantity,Date")] Inventory inventory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(inventory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdProduct"] = new SelectList(_context.Product, "IdProduct", "KindCode", inventory.IdProduct);
            ViewData["IdUser"] = new SelectList(_context.Users, "IdUser", "About", inventory.IdUser);
            return View(inventory);
        }

        // GET: Inventories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inventory = await _context.Inventory.FindAsync(id);
            if (inventory == null)
            {
                return NotFound();
            }
            ViewData["IdProduct"] = new SelectList(_context.Product, "IdProduct", "KindCode", inventory.IdProduct);
            ViewData["IdUser"] = new SelectList(_context.Users, "IdUser", "About", inventory.IdUser);
            return View(inventory);
        }

        // POST: Inventories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdInventary,IdUser,IdProduct,MovementType,Cuantity,Date")] Inventory inventory)
        {
            if (id != inventory.IdInventary)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(inventory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InventoryExists(inventory.IdInventary))
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
            ViewData["IdProduct"] = new SelectList(_context.Product, "IdProduct", "KindCode", inventory.IdProduct);
            ViewData["IdUser"] = new SelectList(_context.Users, "IdUser", "About", inventory.IdUser);
            return View(inventory);
        }

        // GET: Inventories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inventory = await _context.Inventory
                .Include(i => i.IdProductNavigation)
                .Include(i => i.IdUserNavigation)
                .FirstOrDefaultAsync(m => m.IdInventary == id);
            if (inventory == null)
            {
                return NotFound();
            }

            return View(inventory);
        }

        // POST: Inventories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var inventory = await _context.Inventory.FindAsync(id);
            _context.Inventory.Remove(inventory);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InventoryExists(int id)
        {
            return _context.Inventory.Any(e => e.IdInventary == id);
        }
    }
}
