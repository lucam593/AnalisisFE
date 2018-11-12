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
    public class ClientTypesController : Controller
    {
        private readonly db_facturacionContext _context;

        public ClientTypesController(db_facturacionContext context)
        {
            _context = context;
        }

        // GET: ClientTypes
        public async Task<IActionResult> Index()
        {
            var db_facturacionContext = _context.ClientType.Include(c => c.UsersIdUserNavigation);
            return View(await db_facturacionContext.ToListAsync());
        }

        // GET: ClientTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clientType = await _context.ClientType
                .Include(c => c.UsersIdUserNavigation)
                .FirstOrDefaultAsync(m => m.IdClientType == id);
            if (clientType == null)
            {
                return NotFound();
            }

            return View(clientType);
        }

        // GET: ClientTypes/Create
        public IActionResult Create()
        {
            ViewData["UsersIdUser"] = new SelectList(_context.Users, "IdUser", "About");
            return View();
        }

        // POST: ClientTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdClientType,Code,Description,UsersIdUser")] ClientType clientType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(clientType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UsersIdUser"] = new SelectList(_context.Users, "IdUser", "About", clientType.UsersIdUser);
            return View(clientType);
        }

        // GET: ClientTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clientType = await _context.ClientType.FindAsync(id);
            if (clientType == null)
            {
                return NotFound();
            }
            ViewData["UsersIdUser"] = new SelectList(_context.Users, "IdUser", "About", clientType.UsersIdUser);
            return View(clientType);
        }

        // POST: ClientTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdClientType,Code,Description,UsersIdUser")] ClientType clientType)
        {
            if (id != clientType.IdClientType)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(clientType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClientTypeExists(clientType.IdClientType))
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
            ViewData["UsersIdUser"] = new SelectList(_context.Users, "IdUser", "About", clientType.UsersIdUser);
            return View(clientType);
        }

        // GET: ClientTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clientType = await _context.ClientType
                .Include(c => c.UsersIdUserNavigation)
                .FirstOrDefaultAsync(m => m.IdClientType == id);
            if (clientType == null)
            {
                return NotFound();
            }

            return View(clientType);
        }

        // POST: ClientTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var clientType = await _context.ClientType.FindAsync(id);
            _context.ClientType.Remove(clientType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClientTypeExists(int id)
        {
            return _context.ClientType.Any(e => e.IdClientType == id);
        }
    }
}
