using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FactuCR.Models;
using Newtonsoft.Json;

namespace FactuCR.Controllers
{
    public class BillingController : Controller
    {
        private readonly db_facturacionContext _context;

        private List<Product> billProducts = new List<Product>();

        public BillingController(db_facturacionContext context)
        {
            _context = context;
        }

        // GET: Billing
        public async Task<IActionResult> Index()
        {
            var db_facturacionContext = _context.MasterInvoiceVoucher.Include(m => m.IdConditionNavigation).Include(m => m.IdKeyNavigation).Include(m => m.IdPaymentNavigation);
            return View(await db_facturacionContext.ToListAsync());
        }

        // GET: Billing/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var masterInvoiceVoucher = await _context.MasterInvoiceVoucher
                .Include(m => m.IdConditionNavigation)
                .Include(m => m.IdKeyNavigation)
                .Include(m => m.IdPaymentNavigation)
                .FirstOrDefaultAsync(m => m.IdKey == id);
            if (masterInvoiceVoucher == null)
            {
                return NotFound();
            }

            return View(masterInvoiceVoucher);
        }

        // GET: Billing/Create
        public IActionResult Create()
        {
            ViewData["IdCondition"] = new SelectList(_context.MasterSaleCondition, "IdCondition", "Name");
            ViewData["IdKey"] = new SelectList(_context.MasterKey, "IdKey", "Country");
            ViewData["IdPayment"] = new SelectList(_context.MasterPayment, "IdPayment", "Name");
            ViewData["ClientsList"] = new SelectList(_context.Client, "IdClient", "Name");
            ViewData["ProductsList"] = new SelectList(_context.Product, "IdProduct", "NameProduct");

            List<Product> products = _context.Product.ToList();
            ViewBag.Products = JsonConvert.SerializeObject(products);

            return View(new BillManagement());
        }

        // POST: Billing/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdKey,IdPayment,IdCondition,Status,XmlEnviadoBase64,RespuestaMhbase64,Env")] BillManagement model)
        {
            if (ModelState.IsValid)
            {
                _context.Add(model);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCondition"] = new SelectList(_context.MasterSaleCondition, "IdCondition", "Code", model.MasterInvoiceVoucher.IdCondition);
            ViewData["IdKey"] = new SelectList(_context.MasterKey, "IdKey", "Country", model.MasterInvoiceVoucher.IdKey);
            ViewData["IdPayment"] = new SelectList(_context.MasterPayment, "IdPayment", "Code", model.MasterInvoiceVoucher.IdPayment);
            ViewData["ClientsList"] = new SelectList(_context.Client, "IdClient", "Name");
            ViewData["ProductsList"] = new SelectList(_context.Product, "IdProduct", "NameProduct");                  
            
            return View(model);
        }

        [HttpPost]
        public IActionResult AddProduct(BillManagement model)
        {
            ViewData["IdCondition"] = new SelectList(_context.MasterSaleCondition, "IdCondition", "Name");
            ViewData["IdKey"] = new SelectList(_context.MasterKey, "IdKey", "Country");
            ViewData["IdPayment"] = new SelectList(_context.MasterPayment, "IdPayment", "Name");
            ViewData["ClientsList"] = new SelectList(_context.Client, "IdClient", "Name");
            ViewData["ProductsList"] = new SelectList(_context.Product, "IdProduct", "NameProduct");

            List<Product> products = _context.Product.ToList();
            ViewBag.Products = JsonConvert.SerializeObject(products);

            Product findProduct = _context.Product.Find(model.IdProduct);

            if (findProduct != null)
            {
                model.SelectedProducts.Add(findProduct);
            }

            return View("Create", model);
        }

        // GET: Billing/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var masterInvoiceVoucher = await _context.MasterInvoiceVoucher.FindAsync(id);
            if (masterInvoiceVoucher == null)
            {
                return NotFound();
            }
            ViewData["IdCondition"] = new SelectList(_context.MasterSaleCondition, "IdCondition", "Code", masterInvoiceVoucher.IdCondition);
            ViewData["IdKey"] = new SelectList(_context.MasterKey, "IdKey", "Country", masterInvoiceVoucher.IdKey);
            ViewData["IdPayment"] = new SelectList(_context.MasterPayment, "IdPayment", "Code", masterInvoiceVoucher.IdPayment);
            return View(masterInvoiceVoucher);
        }

        // POST: Billing/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdKey,IdPayment,IdCondition,Status,XmlEnviadoBase64,RespuestaMhbase64,Env")] MasterInvoiceVoucher masterInvoiceVoucher)
        {
            if (id != masterInvoiceVoucher.IdKey)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(masterInvoiceVoucher);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MasterInvoiceVoucherExists(masterInvoiceVoucher.IdKey))
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
            ViewData["IdCondition"] = new SelectList(_context.MasterSaleCondition, "IdCondition", "Code", masterInvoiceVoucher.IdCondition);
            ViewData["IdKey"] = new SelectList(_context.MasterKey, "IdKey", "Country", masterInvoiceVoucher.IdKey);
            ViewData["IdPayment"] = new SelectList(_context.MasterPayment, "IdPayment", "Code", masterInvoiceVoucher.IdPayment);
            return View(masterInvoiceVoucher);
        }

        // GET: Billing/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var masterInvoiceVoucher = await _context.MasterInvoiceVoucher
                .Include(m => m.IdConditionNavigation)
                .Include(m => m.IdKeyNavigation)
                .Include(m => m.IdPaymentNavigation)
                .FirstOrDefaultAsync(m => m.IdKey == id);
            if (masterInvoiceVoucher == null)
            {
                return NotFound();
            }

            return View(masterInvoiceVoucher);
        }

        // POST: Billing/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var masterInvoiceVoucher = await _context.MasterInvoiceVoucher.FindAsync(id);
            _context.MasterInvoiceVoucher.Remove(masterInvoiceVoucher);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MasterInvoiceVoucherExists(int id)
        {
            return _context.MasterInvoiceVoucher.Any(e => e.IdKey == id);
        }

        /*
        [HttpPost]
        public IActionResult Create(int selectedIdProduct)
        {            
            billProducts.Add(_context.Product.Find(selectedIdProduct));
            ViewData["SelectedProducts"] = billProducts;

            Console.WriteLine("PRODUCTS AMOUNT: " + billProducts.Count);

            return View();
        }*/
    }
}
