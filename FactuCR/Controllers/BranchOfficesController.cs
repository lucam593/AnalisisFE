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
    public class BranchOfficesController : Controller
    {
        private readonly db_facturacionContext _context;

        public BranchOfficesController(db_facturacionContext context)
        {
            _context = context;
        }

        // GET: BranchOffices
        public async Task<IActionResult> Index()
        {
            return View(await _context.BranchOffice.ToListAsync());
        }

        // GET: BranchOffices/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var branchOffice = await _context.BranchOffice
                .FirstOrDefaultAsync(m => m.IdOffice == id);
            if (branchOffice == null)
            {
                return NotFound();
            }

            return View(branchOffice);
        }

        // GET: BranchOffices/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BranchOffices/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdOffice,Name,OfficeNumber")] BranchOffice branchOffice)
        {
            if (ModelState.IsValid)
            {
                int number = Int32.Parse(branchOffice.OfficeNumber.Replace('_', ' ').Trim());
                string officeNumber = "";

                if (number <= 9 && number > 0)
                {
                    officeNumber = "00" + number;
                }
                else
                {
                    if (number >= 10 && number <= 99)
                    {
                        officeNumber = "0" + number;
                    }
                    else
                    {
                        officeNumber = number.ToString();
                    }

                }
                branchOffice.OfficeNumber = officeNumber;
                List<BranchOffice> office = _context.BranchOffice.Where(x => x.OfficeNumber == officeNumber).ToList();
                if (office.Count == 0)
                {
                    _context.Add(branchOffice);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(branchOffice);
        }

        // GET: BranchOffices/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var branchOffice = await _context.BranchOffice.FindAsync(id);
            if (branchOffice == null)
            {
                return NotFound();
            }
            return View(branchOffice);
        }

        // POST: BranchOffices/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdOffice,Name,OfficeNumber")] BranchOffice branchOffice)
        {
            if (id != branchOffice.IdOffice)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    int number = Int32.Parse(branchOffice.OfficeNumber.Replace('_', ' ').Trim());
                    string officeNumber = "";

                    if (number <= 9 && number > 0)
                    {
                        officeNumber = "00" + number;
                    }
                    else
                    {
                        if (number >= 10 && number <= 99)
                        {
                            officeNumber = "0" + number;
                        }
                        else
                        {
                            officeNumber = number.ToString();
                        }

                    }
                    branchOffice.OfficeNumber = officeNumber;
                    List<BranchOffice> office = _context.BranchOffice.Where(x => x.OfficeNumber == officeNumber).ToList();
                    if (office.Count == 0)
                    {
                        _context.Update(branchOffice);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BranchOfficeExists(branchOffice.IdOffice))
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
            return View(branchOffice);
        }

        // GET: BranchOffices/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var branchOffice = await _context.BranchOffice
                .FirstOrDefaultAsync(m => m.IdOffice == id);
            if (branchOffice == null)
            {
                return NotFound();
            }

            return View(branchOffice);
        }

        // POST: BranchOffices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var branchOffice = await _context.BranchOffice.FindAsync(id);
            _context.BranchOffice.Remove(branchOffice);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BranchOfficeExists(int id)
        {
            return _context.BranchOffice.Any(e => e.IdOffice == id);
        }
    }
}
