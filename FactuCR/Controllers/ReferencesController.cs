using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FactuCR.Models;
using FactuCR.Models.Buzon;

namespace FactuCR.Controllers
{
    public class ReferencesController : Controller
    {
        private readonly db_facturacionContext _context;

        public ReferencesController(db_facturacionContext context)
        {
            _context = context;
        }

        // GET: References
        public async Task<IActionResult> Index()
        {
            var db_facturacionContext = _context.MasterInvoiceReference.Include(m => m.IdKeyNavigation);
            return View(await db_facturacionContext.ToListAsync());
        }

        // GET: References/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var masterInvoiceReference = await _context.MasterInvoiceReference
                .Include(m => m.IdKeyNavigation)
                .FirstOrDefaultAsync(m => m.IdKey == id);
            if (masterInvoiceReference == null)
            {
                return NotFound();
            }

            return View(masterInvoiceReference);
        }

        // GET: References/Create
        public IActionResult Create()
        {
            ViewData["CodeReference"] = new SelectList(_context.VoucherType, "Code", "Name");
            ViewData["situation"] = new SelectList(_context.SituationDocument, "Code", "Name");
            ViewData["BranchOffice"] = new SelectList(_context.BranchOffice, "OfficeNumber", "Name");
            List<MasterInvoiceVoucher> masterInvoices = _context.MasterInvoiceVoucher
               .Include(m => m.IdConditionNavigation)
               .Include(m => m.IdKeyNavigation)
               .Include(m => m.IdPaymentNavigation)
               .Include(m => m.IdKeyNavigation.IdConsecutiveNavigation)
               .Include(m => m.MasterReceiver).ToList();
            ReferenceManagement referenceManagement = new ReferenceManagement();
            referenceManagement.invoiceVouchers = masterInvoices;

            return View(referenceManagement);
        }

        // POST: References/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdKey,DocumentNumber,ReferenceCode,Detail,RespuestaMhbase64,XmlenviadoBase64")] MasterInvoiceReference masterInvoiceReference)
        {
            if (ModelState.IsValid)
            {
                _context.Add(masterInvoiceReference);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdKey"] = new SelectList(_context.MasterKey, "IdKey", "Country", masterInvoiceReference.IdKey);
            return View(masterInvoiceReference);
        }

        // GET: References/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var masterInvoiceReference = await _context.MasterInvoiceReference.FindAsync(id);
            if (masterInvoiceReference == null)
            {
                return NotFound();
            }
            ViewData["IdKey"] = new SelectList(_context.MasterKey, "IdKey", "Country", masterInvoiceReference.IdKey);
            return View(masterInvoiceReference);
        }

        // POST: References/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdKey,DocumentNumber,ReferenceCode,Detail,RespuestaMhbase64,XmlenviadoBase64")] MasterInvoiceReference masterInvoiceReference)
        {
            if (id != masterInvoiceReference.IdKey)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(masterInvoiceReference);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MasterInvoiceReferenceExists(masterInvoiceReference.IdKey))
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
            ViewData["IdKey"] = new SelectList(_context.MasterKey, "IdKey", "Country", masterInvoiceReference.IdKey);
            return View(masterInvoiceReference);
        }

        // GET: References/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var masterInvoiceReference = await _context.MasterInvoiceReference
                .Include(m => m.IdKeyNavigation)
                .FirstOrDefaultAsync(m => m.IdKey == id);
            if (masterInvoiceReference == null)
            {
                return NotFound();
            }

            return View(masterInvoiceReference);
        }

        // POST: References/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var masterInvoiceReference = await _context.MasterInvoiceReference.FindAsync(id);
            _context.MasterInvoiceReference.Remove(masterInvoiceReference);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MasterInvoiceReferenceExists(int id)
        {
            return _context.MasterInvoiceReference.Any(e => e.IdKey == id);
        }

        public JsonResult getTerminal(string branchOffice)
        {

            List<Terminal> TerminalList = _context.Terminal.Where(s => s.IdOfficeNavigation.OfficeNumber == branchOffice).Distinct().ToList();

            return Json(new SelectList(TerminalList, "TerminalNumber", "Name"));
        }
    }
}
