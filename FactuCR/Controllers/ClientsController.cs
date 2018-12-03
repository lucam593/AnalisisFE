using System;
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
    [Authorize]
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

            var client = await _context.Client.FindAsync(id);
            var telephoneContact = _context.TelephoneContact.Where(tc => tc.IdOwner.Equals(client.IdentificationNumber)).First();
            var address = _context.Address.Where(x => x.IdUser == (uint)client.IdClient).First();

            ClientManagement model = new ClientManagement();
            model.Client = client;
            model.TelephoneContact = telephoneContact;
            model.address = address;
            if (client.IdType == 1)
            {
                model.IdentificationNumberCedFisica = client.IdentificationNumber;
            }
            else if (client.IdType == 2)
            {
                model.IdentificationNumberCedJuridica = client.IdentificationNumber;
            }
            else if (client.IdType == 3)
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

            MasterAddress masterA = _context.MasterAddress.Find(address.idCodificacion);
            model.Province = masterA.NombreProvincia;
            model.Canton = masterA.NombreCanton;
            model.District = masterA.NombreDistrito;
            model.neighborhood = masterA.NombreBarrio;
            ViewData["TelephoneContact"] = await _context.TelephoneContact.Where(tc => tc.IdOwner.Equals(client.IdentificationNumber)).FirstAsync();

            return View(model);
        }



        // GET: Clients/Create
        public IActionResult Create()
        {
            ViewData["IdType"] = new SelectList(_context.IdentificationType, "IdType", "Name");
            List<Country> countriesList = _context.Country.ToList();
            ViewData["CountriesList"] = countriesList;

            List<Province> Provinces = new List<Province>();

            List<string> provinceList = _context.MasterAddress.Select(x => x.NombreProvincia).Distinct().ToList();
            Provinces.Add(new Province("Seleccione una provincia."));
            foreach (string province in provinceList)
            {
                Provinces.Add(new Province(province));
            }
            ViewData["provinceList"] = Provinces;

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
            client.Status = 1;
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

            MasterAddress master = _context.MasterAddress.Where(x => x.NombreProvincia == model.Province && x.NombreCanton == model.Canton && x.NombreDistrito == model.District && x.NombreBarrio == model.neighborhood).First();
            client = _context.Client.Last();

            Address address = model.address;
            address.IdUser = (uint)client.IdClient;
            address.idCodificacion = master.IdCodificacion;
            address.OtherSigns = model.address.OtherSigns;

            _context.Address.Add(address);
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
            var address = _context.Address.Where(x => x.IdUser == (uint)client.IdClient).First();

            ClientManagement model = new ClientManagement();
            model.Client = client;
            model.TelephoneContact = telephoneContact;
            model.address = address;
            if (client.IdType == 1)
            {
                model.IdentificationNumberCedFisica = client.IdentificationNumber;
            }
            else if (client.IdType == 2)
            {
                model.IdentificationNumberCedJuridica = client.IdentificationNumber;
            }
            else if (client.IdType == 3)
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

            List<Province> Provinces = new List<Province>();

            List<string> provinceList = _context.MasterAddress.Select(x => x.NombreProvincia).Distinct().ToList();
            Provinces.Add(new Province("Seleccione una provincia."));
            foreach (string province in provinceList)
            {
                Provinces.Add(new Province(province));
            }
            ViewData["provinceList"] = Provinces;
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
                clientInDB.IdentificationNumber = model.IdentificationNumberCedJuridica;
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

            if (model.Province != null && model.Canton != null && model.District != null && model.neighborhood != null)
            {

                MasterAddress master = _context.MasterAddress.Where(x => x.NombreProvincia == model.Province && x.NombreCanton == model.Canton && x.NombreDistrito == model.District && x.NombreBarrio == model.neighborhood).First();
                Address addressOriginal = _context.Address.Where(x => x.IdUser == (uint)clientInDB.IdClient).First();
                if (master.IdCodificacion != addressOriginal.idCodificacion)
                {
                    addressOriginal.idCodificacion = master.IdCodificacion;
                    addressOriginal.OtherSigns = model.address.OtherSigns;
                    _context.Update(addressOriginal);
                }
            }

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
            var client = await _context.Client.FindAsync(id);
            var telephoneContact = _context.TelephoneContact.Where(tc => tc.IdOwner.Equals(client.IdentificationNumber)).First();
            var address = _context.Address.Where(x => x.IdUser == (uint)client.IdClient).First();

            ClientManagement model = new ClientManagement();
            model.Client = client;
            model.TelephoneContact = telephoneContact;
            model.address = address;

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
            var address = _context.Address.Where(x => x.IdUser == (uint)client.IdClient).First();

            _context.Client.Remove(client);
            _context.TelephoneContact.Remove(telephone_contact);
            _context.Address.Remove(address);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));

        }

        private bool ClientExists(int id)
        {
            return _context.Client.Any(e => e.IdClient == id);
        }

        public JsonResult getCanton(string Province)
        {
            List<Canton> canton = new List<Canton>();

            List<string> CantonList = _context.MasterAddress.Where(p => p.NombreProvincia == Province).Select(p => p.NombreCanton).Distinct().ToList();
            canton.Add(new Canton("Seleccione un Canton."));
            foreach (string c in CantonList)
            {
                canton.Add(new Canton(c));
            }
            return Json(new SelectList(canton, "NameCanton", "NameCanton"));
        }

        public JsonResult getDistrict(string Province, string Canton)
        {
            List<District> district = new List<District>();

            List<string> districtList = _context.MasterAddress.Where(s => s.NombreProvincia == Province && s.NombreCanton == Canton).Select(x => x.NombreDistrito).Distinct().ToList();

            district.Add(new District("Seleccione un distrito."));
            foreach (string c in districtList)
            {
                district.Add(new District(c));
            }

            return Json(new SelectList(district, "NameDistrict", "NameDistrict"));
        }


        public JsonResult getNeighborhood(string Province, string Canton, string District)
        {
            List<Neighborhood> Neighborhood = new List<Neighborhood>();

            List<string> neighborhoodList = _context.MasterAddress.Where(s => s.NombreProvincia == Province && s.NombreCanton == Canton && s.NombreDistrito == District).Select(x => x.NombreBarrio).Distinct().ToList();

            Neighborhood.Add(new Neighborhood("Seleccione un Barrio"));
            foreach (string c in neighborhoodList)
            {
                Neighborhood.Add(new Neighborhood(c));
            }

            return Json(new SelectList(Neighborhood, "NameNeighborhood", "NameNeighborhood"));
        }
    }
}
