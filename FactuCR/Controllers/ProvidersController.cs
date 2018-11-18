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
    public class ProvidersController : Controller
    {
        private readonly db_facturacionContext _context;

        public ProvidersController(db_facturacionContext context)
        {
            _context = context;
        }

        // GET: Providers
        public async Task<IActionResult> Index()
        {
            var db_facturacionContext = _context.Provider.Include(p => p.IdTypeNavigation);
            return View(await db_facturacionContext.ToListAsync());
        }

        // GET: Providers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var provider = await _context.Provider
                .Include(p => p.IdTypeNavigation)
                .FirstOrDefaultAsync(m => m.IdProvider == id);
            if (provider == null)
            {
                return NotFound();
            }

            return View(provider);
        }

        // GET: Providers/Create
        public IActionResult Create()
        {
            ViewData["IdType"] = new SelectList(_context.IdentificationType, "IdType", "Name");
            List<Country> countriesList = _context.Country.ToList();
            ViewData["CountriesList"] = countriesList;

            return View(new ProviderManagement());
        }

        [HttpPost]
        public IActionResult Create(ProviderManagement model)
        {
            ViewData["IdType"] = new SelectList(_context.IdentificationType, "IdType", "Name");
            List<Country> countriesList = _context.Country.ToList();
            ViewData["CountriesList"] = countriesList;

            Provider provider = new Provider();
            provider.IdType = model.Provider.IdType;
            provider.Name = model.Provider.Name;
            provider.Email = model.Provider.Email;
            provider.Description = model.Provider.Description;

            if (model.Provider.IdType == 1)
            {
                provider.IdentificationNumber = model.IdentificationNumberCedFisica;
            }
            if (model.Provider.IdType == 2)
            {
                provider.IdentificationNumber = model.IdentificationNumberCedJuridica;
            }
            if (model.Provider.IdType == 3)
            {
                provider.IdentificationNumber = model.IdentificationNumberNITE;
            }
            if (model.Provider.IdType == 4)
            {
                provider.IdentificationNumber = model.IdentificationNumberNITE;
            }
            
            TelephoneContact telephoneContact = new TelephoneContact();
            telephoneContact.IdOwner = provider.IdentificationNumber;
            telephoneContact.CountryCode = model.TelephoneContact.CountryCode;
            telephoneContact.TelephoneNumber = model.TelephoneContact.TelephoneNumber;
            telephoneContact.Type = model.TelephoneContact.Type;
            telephoneContact.Description = model.TelephoneContact.Description;
            telephoneContact.Extension = model.TelephoneContact.Extension;

            _context.TelephoneContact.Add(telephoneContact);
            _context.Provider.Add(provider);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        /* POST: Providers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdProvider,IdType,IdentificationNumber,Name,Email,Description")] Provider provider)
        {
            if (ModelState.IsValid)
            {
                _context.Add(provider);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdType"] = new SelectList(_context.IdentificationType, "IdType", "Name", provider.IdType);
            return View(provider);
        }*/

        // GET: Providers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var provider = await _context.Provider.FindAsync(id);
            if (provider == null)
            {
                return NotFound();
            }
            ViewData["IdType"] = new SelectList(_context.IdentificationType, "IdType", "Name", provider.IdType);
            return View(provider);
        }

        // POST: Providers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdProvider,IdType,IdentificationNumber,Name,Email,Description")] Provider provider)
        {
            if (id != provider.IdProvider)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(provider);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProviderExists(provider.IdProvider))
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
            ViewData["IdType"] = new SelectList(_context.IdentificationType, "IdType", "Name", provider.IdType);
            return View(provider);
        }

        // GET: Providers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var provider = await _context.Provider
                .Include(p => p.IdTypeNavigation)
                .FirstOrDefaultAsync(m => m.IdProvider == id);
            if (provider == null)
            {
                return NotFound();
            }

            return View(provider);
        }

        // POST: Providers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var provider = await _context.Provider.FindAsync(id);
            _context.Provider.Remove(provider);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProviderExists(int id)
        {
            return _context.Provider.Any(e => e.IdProvider == id);
        }
    }
}
