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
    public class ConfigCompaniesController : Controller
    {
        private readonly db_facturacionContext _context;

        public ConfigCompaniesController(db_facturacionContext context)
        {
            _context = context;
        }




        // GET: ConfigCompanies/Create
        public IActionResult Create()
        {
            int count = _context.ConfigCompany.Count();
            if (count >= 1)
            {
                return RedirectToAction("Index", "ConfigCompanies");

            }
            else { 

                ViewData["IdType"] = new SelectList(_context.IdentificationType, "IdType", "Name");
            ConfigCompaniesManagement conf = new ConfigCompaniesManagement();

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
        }
    

    // POST: ConfigCompanies/Create
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(ConfigCompaniesManagement model)
    {
        ViewData["IdType"] = new SelectList(_context.IdentificationType, "IdType", "Name");
        ConfigCompany configCompannyInBD = new ConfigCompany();
        configCompannyInBD.IdType = model.configCompany.IdType;
        configCompannyInBD.FullName = model.configCompany.FullName;
        configCompannyInBD.Email = model.configCompany.Email;
        configCompannyInBD.CompannyName = model.configCompany.CompannyName;
        ViewData["IdType"] = new SelectList(_context.IdentificationType, "IdType", "Name", model.configCompany.IdType);


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
            configCompannyInBD.neighborhood = model.configCompany.neighborhood;
            configCompannyInBD.OtherSigns = model.configCompany.OtherSigns;

        configCompannyInBD.UserTax = model.configCompany.UserTax;
        configCompannyInBD.PasswordTax = model.configCompany.PasswordTax;
        configCompannyInBD.Country = model.configCompany.Country;
        configCompannyInBD.Currency = model.configCompany.Currency;
        configCompannyInBD.CurrencyValue = model.configCompany.CurrencyValue;
        configCompannyInBD.neighborhood = "01";
        configCompannyInBD.pin = "1234";

        List<ConfigCompany> list = _context.ConfigCompany.Where(x => x.Status == "Activa").ToList();

        if (list == null)
        {
            configCompannyInBD.Status = "Activa";
        }
        else
        {
            foreach (ConfigCompany conf in list)
            {
                conf.Status = "Inactiva";
                _context.Update(conf);
            }
            configCompannyInBD.Status = "Activa";
        }



        _context.Add(configCompannyInBD);
        _context.SaveChanges();
        return RedirectToAction(nameof(Index)); //revisar
    }

    // GET: ConfigCompanies/Edit/5
    public async Task<IActionResult> Index()
    {
        try
        {
            int count = _context.ConfigCompany.Count();
            if (count == 0)
            {
                return NotFound();

            }
            else
            {

                int id = 1;
                var configCompany = await _context.ConfigCompany.FindAsync(id);
                if (configCompany == null)
                {
                    return NotFound();
                }

                ViewData["IdType"] = new SelectList(_context.IdentificationType, "IdType", "Name", configCompany.IdType);
                ConfigCompaniesManagement conf = new ConfigCompaniesManagement();
                conf.configCompany = configCompany;

                List<Province> Provinces = new List<Province>();

                List<string> provinceList = _context.MasterAddress.Select(x => x.NombreProvincia).Distinct().ToList();
                Provinces.Add(new Province("Seleccione una provincia."));
                foreach (string province in provinceList)
                {
                    Provinces.Add(new Province(province));
                }
                ViewData["idUser"] = Convert.ToString(conf.configCompany.IdUser).Replace("-", "");
                ViewData["conf"] = conf;
                ViewData["provinceList"] = Provinces;
                return View(conf);
            }
        }
        catch (Exception)
        {

            throw; //vista error
        }
    }

    // POST: ConfigCompanies/Edit/5
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Index(int id, ConfigCompaniesManagement model)
    {



        ConfigCompany configCompannyInBD = _context.ConfigCompany.Find(model.configCompany.IdConfig);
        if (!configCompannyInBD.IdType.Equals(model.configCompany.IdType))
        {
            configCompannyInBD.IdType = model.configCompany.IdType;
        }

        if (!configCompannyInBD.FullName.Equals(model.configCompany.FullName) | model.configCompany != null)
        {
            configCompannyInBD.FullName = model.configCompany.FullName;
        }

        if (!configCompannyInBD.Email.Equals(model.configCompany.Email) | model.configCompany != null)
        {
            configCompannyInBD.Email = model.configCompany.Email;
        }

        if (!configCompannyInBD.CompannyName.Equals(model.configCompany.CompannyName) | model.configCompany.CompannyName != null)
        {
            configCompannyInBD.CompannyName = model.configCompany.CompannyName;
        }

        ViewData["IdType"] = new SelectList(_context.IdentificationType, "IdType", "Name", model.configCompany.IdType);


        if (model.IdentificationNumberCedFisica != null | model.IdentificationNumberCedJuridica != null |
            model.IdentificationNumberNITE != null | model.IdentificationNumberDIMEX != null
            )
        {
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
        }
        if (!configCompannyInBD.Telephone.Equals(model.configCompany.Telephone) | model.configCompany.Telephone != null)
        {
            configCompannyInBD.Telephone = model.configCompany.Telephone;
        }

        if (!configCompannyInBD.Fax.Equals(model.configCompany.Fax) | model.configCompany.Fax != null)
        {
            configCompannyInBD.Fax = model.configCompany.Fax;
        }
        if (model.configCompany.Province != null && model.configCompany.Canton != null && model.configCompany.District != null && model.configCompany.neighborhood != null)
        {
            string province = model.configCompany.Province;
            string canton = model.configCompany.Canton;
            string district = model.configCompany.District;
            string neighborhood = model.configCompany.neighborhood;

            MasterAddress master = _context.MasterAddress.Where(x => x.NombreProvincia == province && x.NombreCanton == canton && x.NombreDistrito == district && x.NombreBarrio == neighborhood).First();
            configCompannyInBD.Province = master.IdProvincia;
            configCompannyInBD.Canton = master.IdCanton;
            configCompannyInBD.District = master.IdDistrito;
            configCompannyInBD.neighborhood = master.IdBarrio;
        }

        if (!configCompannyInBD.OtherSigns.Equals(model.configCompany.OtherSigns) | model.configCompany.OtherSigns != null)
        {
            configCompannyInBD.OtherSigns = model.configCompany.OtherSigns;
        }

        if (!configCompannyInBD.UserTax.Equals(model.configCompany.UserTax) | model.configCompany.UserTax != null)
        {
            configCompannyInBD.UserTax = model.configCompany.UserTax;
        }


        if (!configCompannyInBD.PasswordTax.Equals(model.configCompany.PasswordTax) | model.configCompany.PasswordTax != null)
        {
            configCompannyInBD.PasswordTax = model.configCompany.PasswordTax;
        }


        if (!configCompannyInBD.Country.Equals(model.configCompany.Country) | model.configCompany.Country != null)
        {
            configCompannyInBD.Country = model.configCompany.Country;
        }


        if (!configCompannyInBD.Currency.Equals(model.configCompany.Currency) | model.configCompany.Currency != null)
        {
            configCompannyInBD.Currency = model.configCompany.Currency;
        }

        if (!configCompannyInBD.CurrencyValue.Equals(model.configCompany.CurrencyValue) | model.configCompany.CurrencyValue != null)
        {
            configCompannyInBD.CurrencyValue = model.configCompany.CurrencyValue;
        }

        if (!configCompannyInBD.Status.Equals(model.configCompany.Status))
        {
            if (model.configCompany.Status.Equals("Activa"))
            {
                List<ConfigCompany> list = _context.ConfigCompany.Where(x => x.Status == "Activa").ToList();
                foreach (ConfigCompany conf in list)
                {
                    conf.Status = "Inactiva";
                    _context.Update(conf);
                }
            }
            configCompannyInBD.Status = model.configCompany.Status;
        }

        if (!configCompannyInBD.pin.Equals(model.configCompany.pin) | model.configCompany.pin != null)
        {
            configCompannyInBD.pin = model.configCompany.pin;
        }

        _context.Update(configCompannyInBD);
        _context.SaveChanges();
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
