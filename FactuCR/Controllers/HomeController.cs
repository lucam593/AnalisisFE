using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FactuCR.Models;
using Microsoft.AspNetCore.Authorization;

namespace FactuCR.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly db_facturacionContext _context;

        public HomeController(db_facturacionContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            ViewData["TotalClients"] = _context.Client.Count();
            ViewData["TotalProducts"] = _context.Product.Count();
            ViewData["TotalProviders"] = _context.Provider.Count();
            ViewData["TotalBills"] = _context.MasterInvoiceVoucher.Count();

            return View();
        }

        public IActionResult Manual()
        {
            return View();
        }
    }
}