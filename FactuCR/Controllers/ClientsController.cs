﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FactuCR.Models;
using Microsoft.AspNetCore.Authorization;

namespace FactuCR.Controllers
{
 //   [Authorize]
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

            ViewData["TelephoneContact"] = await _context.TelephoneContact.Where(tc => tc.IdOwner.Equals(client.IdentificationNumber)).FirstAsync();

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
            else if (client.IdType == 2)
            {
                model.IdentificationNumberCedJuridica = client.IdentificationNumber;
            }
            else  if (client.IdType == 3)
            {
                model.IdentificationNumberNITE = client.IdentificationNumber;
            }
            else   
            {
                model.IdentificationNumberDIMEX = client.IdentificationNumber;
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

            Client clientInDB = _context.Client.Find(model.Client.IdClient);
            clientInDB.IdType = model.Client.IdType;
            clientInDB.IdentificationNumber = model.Client.IdentificationNumber;
            clientInDB.Name = model.Client.Name;
            clientInDB.LastName = model.Client.LastName;
            clientInDB.Email = model.Client.Email;
            clientInDB.Country = model.Client.Country;
            clientInDB.Status = model.Client.Status;
            clientInDB.AdmissionDate = model.Client.AdmissionDate;


            if (model.Client.IdType == 1)
            {
                clientInDB.IdentificationNumber = model.IdentificationNumberCedFisica;
            }
            else if (model.Client.IdType == 2)
            {
                clientInDB.IdentificationNumber = model.IdentificationNumberCedJuridica ;
            }
            else if (model.Client.IdType == 3)
            {
                clientInDB.IdentificationNumber = model.IdentificationNumberNITE;
            }
            else
            {
                clientInDB.IdentificationNumber = model.IdentificationNumberDIMEX;
            }

            TelephoneContact telephoneContactInDB = new TelephoneContact();
            telephoneContactInDB.IdOwner = clientInDB.IdentificationNumber;
            telephoneContactInDB.CountryCode = model.TelephoneContact.CountryCode;
            telephoneContactInDB.TelephoneNumber = model.TelephoneContact.TelephoneNumber;
            telephoneContactInDB.Type = model.TelephoneContact.Type;
            telephoneContactInDB.Description = model.TelephoneContact.Description;
            telephoneContactInDB.Extension = model.TelephoneContact.Extension;

            _context.Update(telephoneContactInDB);
            _context.Update(clientInDB);
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
            ClientManagement model = new ClientManagement();

            var client = await _context.Client
                .Include(c => c.IdTypeNavigation)
                .FirstOrDefaultAsync(m => m.IdClient == id);
            
            if (client == null)
            {
                return NotFound();
            }
            ViewData["IdentificationType"] = _context.IdentificationType.Find(client.IdType);

            model.Client = client;

            return View(model);
        }

        // POST: Clients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var client = await _context.Client.FindAsync(id);
            var telephone_contact = await _context.TelephoneContact.Where(tc => tc.IdOwner.Equals(client.IdentificationNumber)).FirstAsync();

            _context.Client.Remove(client);
            _context.TelephoneContact.Remove(telephone_contact);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
            
        }

        private bool ClientExists(int id)
        {
            return _context.Client.Any(e => e.IdClient == id);
        }
    }
}
