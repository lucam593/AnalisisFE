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
    public class TerminalsController : Controller
    {
        private readonly db_facturacionContext _context;

        public TerminalsController(db_facturacionContext context)
        {
            _context = context;
        }

        // GET: Terminals
        public async Task<IActionResult> Index()
        {
            var db_facturacionContext = _context.Terminal.Include(t => t.IdOfficeNavigation);
            return View(await db_facturacionContext.ToListAsync());
        }

        // GET: Terminals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var terminal = await _context.Terminal
                .Include(t => t.IdOfficeNavigation)
                .FirstOrDefaultAsync(m => m.IdTerminal == id);
            if (terminal == null)
            {
                return NotFound();
            }

            return View(terminal);
        }

        // GET: Terminals/Create
        public IActionResult Create()
        {
            ViewData["IdOffice"] = new SelectList(_context.BranchOffice, "IdOffice", "Name");
            return View();
        }

        // POST: Terminals/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdTerminal,Name,TerminalNumber,IdOffice")] Terminal terminal)
        {
            if (ModelState.IsValid)
            {

                int number = Int32.Parse(terminal.TerminalNumber.Replace('_', ' ').Trim());
                string TerminalNumber = "";

                if (number <= 9 && number > 0)
                {
                    TerminalNumber = "0000" + number;
                }
                else
                {
                    if (number >= 10 && number <= 99)
                    {
                        TerminalNumber = "000" + number;
                    }
                    else
                    {
                        if (number >= 100 && number <= 999)
                        {
                            TerminalNumber = "00" + number;
                        }
                        else
                        {
                            if (number >= 1000 && number <= 9999)
                            {
                                TerminalNumber = "0" + number;
                            }
                            else
                            {
                                TerminalNumber = number.ToString();
                            }
                        }
                    }
                }
                terminal.TerminalNumber = TerminalNumber;
                List<Terminal> office = _context.Terminal.Where(x => x.TerminalNumber == TerminalNumber).ToList();
                if (office.Count == 0)
                {
                    _context.Add(terminal);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            ViewData["IdOffice"] = new SelectList(_context.BranchOffice, "IdOffice", "Name", terminal.IdOffice);
            return View(terminal);
        }

        // GET: Terminals/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var terminal = await _context.Terminal.FindAsync(id);
            if (terminal == null)
            {
                return NotFound();
            }
            ViewData["IdOffice"] = new SelectList(_context.BranchOffice, "IdOffice", "Name", terminal.IdOffice);
            return View(terminal);
        }

        // POST: Terminals/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdTerminal,Name,TerminalNumber,IdOffice")] Terminal terminal)
        {
            if (id != terminal.IdTerminal)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    int number = Int32.Parse(terminal.TerminalNumber.Replace('_', ' ').Trim());
                    string TerminalNumber = "";

                    if (number <= 9 && number > 0)
                    {
                        TerminalNumber = "0000" + number;
                    }
                    else
                    {
                        if (number >= 10 && number <= 99)
                        {
                            TerminalNumber = "000" + number;
                        }
                        else
                        {
                            if (number >= 100 && number <= 999)
                            {
                                TerminalNumber = "00" + number;
                            }
                            else
                            {
                                if (number >= 1000 && number <= 9999)
                                {
                                    TerminalNumber = "0" + number;
                                }
                                else
                                {
                                    TerminalNumber = number.ToString();
                                }
                            }
                        }
                    }
                    terminal.TerminalNumber = TerminalNumber;
                    List<Terminal> office = _context.Terminal.Where(x => x.TerminalNumber == TerminalNumber).ToList();
                    if (office.Count == 0)
                    {
                        _context.Update(terminal);
                        _context.SaveChanges();
                        return RedirectToAction(nameof(Index));
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TerminalExists(terminal.IdTerminal))
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
            ViewData["IdOffice"] = new SelectList(_context.BranchOffice, "IdOffice", "Name", terminal.IdOffice);
            return View(terminal);
        }

        // GET: Terminals/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var terminal = await _context.Terminal
                .Include(t => t.IdOfficeNavigation)
                .FirstOrDefaultAsync(m => m.IdTerminal == id);
            if (terminal == null)
            {
                return NotFound();
            }

            return View(terminal);
        }

        // POST: Terminals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var terminal = await _context.Terminal.FindAsync(id);
            _context.Terminal.Remove(terminal);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TerminalExists(int id)
        {
            return _context.Terminal.Any(e => e.IdTerminal == id);
        }
    }
}
