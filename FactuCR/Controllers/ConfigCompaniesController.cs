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
    public class ConfigCompaniesController : Controller
    {
        private readonly db_facturacionContext _context;

        public ConfigCompaniesController(db_facturacionContext context)
        {
            _context = context;
        }

        // GET: ConfigCompanies
        public async Task<IActionResult> Index()
        {
            var db_facturacionContext = _context.ConfigCompany.Include(c => c.IdTypeNavigation);
            return View(await db_facturacionContext.ToListAsync());
        }

        // GET: ConfigCompanies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var configCompany = await _context.ConfigCompany
                .Include(c => c.IdTypeNavigation)
                .FirstOrDefaultAsync(m => m.IdConfig == id);
            if (configCompany == null)
            {
                return NotFound();
            }

            return View(configCompany);
        }

        // GET: ConfigCompanies/Create
        public IActionResult Create()
        {
            ViewData["IdType"] = new SelectList(_context.IdentificationType, "IdType", "Code");
            return View();
        }

        // POST: ConfigCompanies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdConfig,FullName,IdType,IdUser,CompannyName,Email,Telephone,Fax,Province,Canton,District,OtherSigns,Country,UserTax,PasswordTax,Currency,CurrencyValue")] ConfigCompany configCompany)
        {
            if (ModelState.IsValid)
            {
                _context.Add(configCompany);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdType"] = new SelectList(_context.IdentificationType, "IdType", "Code", configCompany.IdType);
            return View(configCompany);
        }

        // GET: ConfigCompanies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var configCompany = await _context.ConfigCompany.FindAsync(id);
            if (configCompany == null)
            {
                return NotFound();
            }
            ViewData["IdType"] = new SelectList(_context.IdentificationType, "IdType", "Code", configCompany.IdType);
            ConfigCompaniesManagement conf = new ConfigCompaniesManagement();
            conf.configCompany = configCompany;
            return View(conf);
        }

        // POST: ConfigCompanies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdConfig,FullName,IdType,IdUser,CompannyName,Email,Telephone,Fax,Province,Canton,District,OtherSigns,Country,UserTax,PasswordTax,Currency,CurrencyValue")] ConfigCompany configCompany)
        {
            if (id != configCompany.IdConfig)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(configCompany);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConfigCompanyExists(configCompany.IdConfig))
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
            ViewData["IdType"] = new SelectList(_context.IdentificationType, "IdType", "Code", configCompany.IdType);
            ConfigCompaniesManagement conf = new ConfigCompaniesManagement();
            conf.configCompany = configCompany;
            return View(conf);
        }

        // GET: ConfigCompanies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var configCompany = await _context.ConfigCompany
                .Include(c => c.IdTypeNavigation)
                .FirstOrDefaultAsync(m => m.IdConfig == id);
            if (configCompany == null)
            {
                return NotFound();
            }

            return View(configCompany);
        }

        // POST: ConfigCompanies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var configCompany = await _context.ConfigCompany.FindAsync(id);
            _context.ConfigCompany.Remove(configCompany);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ConfigCompanyExists(int id)
        {
            return _context.ConfigCompany.Any(e => e.IdConfig == id);
        }
    }
}
