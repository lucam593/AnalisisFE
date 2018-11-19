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
    public class FilesController : Controller
    {
        private readonly db_facturacionContext _context;

        public FilesController(db_facturacionContext context)
        {
            _context = context;
        }

        // GET: Files
        public async Task<IActionResult> Index()
        {
            return View(await _context.Files.ToListAsync());
        }

        // GET: Files/Details/5
        public async Task<IActionResult> Details(uint? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var files = await _context.Files
                .FirstOrDefaultAsync(m => m.IdFile == id);
            if (files == null)
            {
                return NotFound();
            }

            return View(files);
        }

        // GET: Files/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Files/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdFile,Md5,Name,Timestamp,Size,IdUser,DownloadCode,FileType,Type")] Files files)
        {
            if (ModelState.IsValid)
            {
                _context.Add(files);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(files);
        }

        // GET: Files/Edit/5
        public async Task<IActionResult> Edit(uint? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var files = await _context.Files.FindAsync(id);
            if (files == null)
            {
                return NotFound();
            }
            return View(files);
        }

        // POST: Files/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(uint id, [Bind("IdFile,Md5,Name,Timestamp,Size,IdUser,DownloadCode,FileType,Type")] Files files)
        {
            if (id != files.IdFile)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(files);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FilesExists(files.IdFile))
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
            return View(files);
        }

        // GET: Files/Delete/5
        public async Task<IActionResult> Delete(uint? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var files = await _context.Files
                .FirstOrDefaultAsync(m => m.IdFile == id);
            if (files == null)
            {
                return NotFound();
            }

            return View(files);
        }

        // POST: Files/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(uint id)
        {
            var files = await _context.Files.FindAsync(id);
            _context.Files.Remove(files);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FilesExists(uint id)
        {
            return _context.Files.Any(e => e.IdFile == id);
        }
    }
}
