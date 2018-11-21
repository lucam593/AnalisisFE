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
    public class ClientsController : Controller
    {
        private readonly db_facturacionContext _context;

        public ClientsController(db_facturacionContext context)
        {
            _context = context;
        }

        // GET: Clients
        public async Task<IActionResult> Index()
        {
            var db_facturacionContext = _context.Client.Include(c => c.IdTypeNavigation);
            return View(await db_facturacionContext.ToListAsync());
        }


        // GET: Clients/Details/5
        public async Task<IActionResult> Details(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var client = await _context.Client
                .Include(c => c.IdTypeNavigation)
                .FirstOrDefaultAsync(m => m.IdClient == id);

            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }



        // GET: Clients/Create
        public IActionResult Create()
        {
            ViewData["IdType"] = new SelectList(_context.IdentificationType, "IdType", "Name");
            List<Country> countriesList = _context.Country.ToList();
            ViewData["CountriesList"] = countriesList;

            return View(new ClientManagement());
        }



        [HttpPost]  
        public IActionResult Create(ClientManagement model)
        {
            ViewData["IdType"] = new SelectList(_context.IdentificationType, "IdType", "Name");
            List<Country> countriesList = _context.Country.ToList();
            ViewData["CountriesList"] = countriesList;

            Client client = new Client();
            client.IdType = model.Client.IdType;
            client.IdentificationNumber = model.Client.IdentificationNumber;
            client.Name = model.Client.Name;
            client.LastName = model.Client.LastName;
            client.Email = model.Client.Email;
            client.Country = model.Client.Country;
            client.Status = model.Client.Status;
            client.AdmissionDate = model.Client.AdmissionDate;

            if (model.Client.IdType == 1)
            {
                client.IdentificationNumber = model.IdentificationNumberCedFisica;
            }
            if (model.Client.IdType == 2)
            {
                client.IdentificationNumber = model.IdentificationNumberCedJuridica;
            }
            if (model.Client.IdType == 3)
            {
                client.IdentificationNumber = model.IdentificationNumberNITE;
            }
            if (model.Client.IdType == 4)
            {
                client.IdentificationNumber = model.IdentificationNumberDIMEX;
            }
            TelephoneContact telephoneContact = new TelephoneContact();
            telephoneContact.IdOwner = client.IdentificationNumber;
            telephoneContact.CountryCode = model.TelephoneContact.CountryCode;
            telephoneContact.TelephoneNumber = model.TelephoneContact.TelephoneNumber;
            telephoneContact.Type = model.TelephoneContact.Type;
            telephoneContact.Description = model.TelephoneContact.Description;
            telephoneContact.Extension = model.TelephoneContact.Extension;

            _context.TelephoneContact.Add(telephoneContact);
            _context.Client.Add(client);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        // GET: Clients/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _context.Client.FindAsync(id);
            var telephoneContact = _context.TelephoneContact.Where(tc => tc.IdOwner.Equals(client.IdentificationNumber)).First();

            ClientManagement model = new ClientManagement();
            model.Client = client;
            model.TelephoneContact = telephoneContact;

            if (client.IdType == 1)
            {
            model.IdentificationNumberCedFisica  = client.IdentificationNumber;
            }
            else if (model.Client.IdType == 2)
            {
                model.IdentificationNumberCedJuridica = client.IdentificationNumber ;
            }
          else  if (model.Client.IdType == 3)
            {
                model.IdentificationNumberNITE = client.IdentificationNumber  ;
            }
        else   
            {
                model.IdentificationNumberDIMEX = client.IdentificationNumber  ;
            }


            if (client == null)
            {
                return NotFound();
            }
            ViewData["IdType"] = new SelectList(_context.IdentificationType, "IdType", "Name", client.IdType);
            List<Country> countriesList = _context.Country.ToList();
            ViewData["CountriesList"] = countriesList;

            return View(model);
        }

        // POST: Clients/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, ClientManagement model)
        {
            ViewData["IdType"] = new SelectList(_context.IdentificationType, "IdType", "Name", model.Client.IdType);
            List<Country> countriesList = _context.Country.ToList();
            ViewData["CountriesList"] = countriesList;

            Client client = _context.Client.Find(model.Client.IdClient);
            client.IdType = model.Client.IdType;
            client.IdentificationNumber = model.Client.IdentificationNumber;
            client.Name = model.Client.Name;
            client.LastName = model.Client.LastName;
            client.Email = model.Client.Email;
            client.Country = model.Client.Country;
            client.Status = model.Client.Status;
            client.AdmissionDate = model.Client.AdmissionDate;


            if (client.IdType == 1)
            {
              client.IdentificationNumber = model.IdentificationNumberCedFisica;
            }
            else if (model.Client.IdType == 2)
            {
               client.IdentificationNumber = model.IdentificationNumberCedJuridica ;
            }
            else if (model.Client.IdType == 3)
            {
                client.IdentificationNumber = model.IdentificationNumberNITE;
            }
            else
            {
                client.IdentificationNumber = model.IdentificationNumberDIMEX;
            
            }
            TelephoneContact telephoneContact = new TelephoneContact();
            telephoneContact.IdOwner = client.IdentificationNumber;
            telephoneContact.CountryCode = model.TelephoneContact.CountryCode;
            telephoneContact.TelephoneNumber = model.TelephoneContact.TelephoneNumber;
            telephoneContact.Type = model.TelephoneContact.Type;
            telephoneContact.Description = model.TelephoneContact.Description;
            telephoneContact.Extension = model.TelephoneContact.Extension;

            _context.Update(telephoneContact);
            _context.Update(client);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
        
        

        // GET: Clients/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var client = await _context.Client
                .Include(c => c.IdTypeNavigation)
                .FirstOrDefaultAsync(m => m.IdClient == id);

            ClientManagement model = new ClientManagement();
            model.Client = client;

            if (model == null)
            {
                return NotFound();
            }
            return View(model);
        }

        // POST: Clients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var client = await _context.Client.FindAsync(id);
            _context.Client.Remove(client);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            
        }

        private bool ClientExists(int id)
        {
            return _context.Client.Any(e => e.IdClient == id);
        }
    }
}
