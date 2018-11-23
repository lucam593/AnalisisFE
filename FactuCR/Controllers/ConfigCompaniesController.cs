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

        //GET: ConfigCompanies
        public IActionResult Index()
        {
            var db_facturacionContext = _context.ConfigCompany.Include(c => c.IdTypeNavigation);
            return RedirectToAction(nameof(Details));
        }

        public async Task<IActionResult> Details()
        {
            var configCompany = await _context.ConfigCompany
                .Include(c => c.IdTypeNavigation)
                .FirstOrDefaultAsync(m => m.IdConfig == 1);
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
        public async Task<IActionResult> Edit()
        {
            var configCompany = await _context.ConfigCompany.FindAsync(1);
            if (configCompany == null)
            {
                return NotFound();
            }
            ViewData["IdType"] = new SelectList(_context.IdentificationType, "IdType", "Code", configCompany.IdType);
            ConfigCompaniesManagement conf = new ConfigCompaniesManagement();
            conf.configCompany = configCompany;

            List<Province> Provinces = new List<Province>();

            List<string> provinceList = _context.MasterAddress.Select(x => x.NombreProvincia).Distinct().ToList();
            Provinces.Add(new Province("Seleccione una provincia."));
            foreach (string province in provinceList)
            {
                Provinces.Add(new Province(province));
            }
            ViewData["provinceList"] = Provinces;
            return View(conf);
        }

        // POST: ConfigCompanies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ConfigCompaniesManagement model)
        {
            ViewData["IdType"] = new SelectList(_context.IdentificationType, "IdType", "Name", model.configCompany.IdType);


            ConfigCompany configCompannyInBD = _context.ConfigCompany.Find(model.configCompany.IdConfig);
            configCompannyInBD.IdType = model.configCompany.IdType;
            configCompannyInBD.FullName = model.configCompany.FullName;
            configCompannyInBD.Email = model.configCompany.Email;
            configCompannyInBD.CompannyName = model.configCompany.CompannyName;

            string providerIdBeforeChanges = model.configCompany.IdUser;

            switch (model.configCompany.IdType)
            {

                case 1:
                    configCompannyInBD.IdUser = model.IdentificationNumberCedFisica;
                    break;
                case 2:
                    configCompannyInBD.IdUser = model.IdentificationNumberCedJuridica;
                    break;
                case 3:
                    configCompannyInBD.IdUser = model.IdentificationNumberNITE;
                    break;
                default:
                    configCompannyInBD.IdUser = model.IdentificationNumberDIMEX;
                    break;
            }
            configCompannyInBD.Telephone = model.configCompany.Telephone;
            configCompannyInBD.Fax = model.configCompany.Fax;
            configCompannyInBD.Province = model.configCompany.Province;
            configCompannyInBD.Canton = model.configCompany.Canton;
            configCompannyInBD.District = model.configCompany.District;
            configCompannyInBD.OtherSigns = model.configCompany.OtherSigns;

            configCompannyInBD.UserTax = model.configCompany.UserTax;
            configCompannyInBD.PasswordTax = model.configCompany.PasswordTax;
            configCompannyInBD.Country = model.configCompany.Country;
            configCompannyInBD.Currency = model.configCompany.Currency;
            configCompannyInBD.CurrencyValue = model.configCompany.CurrencyValue;

            _context.Update(configCompannyInBD);
            _context.SaveChanges();
            return RedirectToAction(nameof(Details));
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

        //public JsonResult getNeighborhood(string Province, string Canton, string district)
        //{
        //    List<Neighborhood> Neighborhood = new List<Neighborhood>();

        //    List<string> neighborhoodList = _context.MasterAddress.Where(s => s.NombreProvincia == Province && s.NombreCanton == Canton && s.NombreDistrito == district).Select(x => x.NombreBarrio).Distinct().ToList();

        //    Neighborhood.Add(new Neighborhood("Seleccione un Barrio"));
        //    foreach (string c in neighborhoodList)
        //    {
        //        Neighborhood.Add(new Neighborhood(c));
        //    }

        //    return Json(new SelectList(Neighborhood, "NameNeighborhood", "NameNeighborhood"));
        //}
    }
}
