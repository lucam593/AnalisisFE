using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FactuCR.Models;
using Newtonsoft.Json;
using System.Diagnostics;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Authorization;
using FactuCR.Models.Email;
using Microsoft.AspNetCore.Hosting;
using System.Text;

namespace FactuCR.Controllers
{
    //[Authorize]
    public class BillingController : Controller
    {
        private readonly db_facturacionContext _context;
        private readonly IHostingEnvironment _hostingEnvironment;

        public BillingController(db_facturacionContext context, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }

        // GET: Billing
        public async Task<IActionResult> Index()
        {
            var db_facturacionContext = _context.MasterInvoiceVoucher
                .Include(m => m.IdConditionNavigation)
                .Include(m => m.IdKeyNavigation)
                .Include(m => m.IdPaymentNavigation)
                .Include(m => m.IdKeyNavigation.IdConsecutiveNavigation)
                .Include(m => m.MasterReceiver);

            ViewData["Bills"] = _context.MasterInvoiceVoucher.ToList<MasterInvoiceVoucher>();

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
                .Include(m => m.IdKeyNavigation.IdConsecutiveNavigation)
                .Include(m => m.MasterReceiver)
                .Include(m => m.MasterDetail)
                .FirstOrDefaultAsync(m => m.IdKey == id);
            if (masterInvoiceVoucher == null)
            {
                return NotFound();
            }

            return View(masterInvoiceVoucher);
        }

        // GET: Billing/Create
        public IActionResult CreateBill()
        {
            ViewData["IdCondition"] = new SelectList(_context.MasterSaleCondition, "IdCondition", "Name");
            ViewData["IdKey"] = new SelectList(_context.MasterKey, "IdKey", "Country");
            ViewData["IdPayment"] = new SelectList(_context.MasterPayment, "IdPayment", "Name");
            ViewData["ClientsList"] = new SelectList(_context.Client, "IdClient", "Name");
            ViewData["ProductsList"] = new SelectList(_context.Product, "IdProduct", "NameProduct");

            List<Product> products = _context.Product.ToList();
            ViewBag.Products = JsonConvert.SerializeObject(products);

            List<Client> clients = _context.Client.ToList();
            ViewBag.Clients = JsonConvert.SerializeObject(clients);

            List<Tax> taxes = _context.Tax.ToList();
            ViewBag.Taxes = JsonConvert.SerializeObject(taxes);

            //return View(new BillManagement());
            return View();
        }

        // POST: Billing/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateBill([Bind("IdKey,IdPayment,IdCondition,Status,XmlEnviadoBase64,RespuestaMhbase64,Env")] BillManagement model)
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

        private bool MasterInvoiceVoucherExists(int id)
        {
            return _context.MasterInvoiceVoucher.Any(e => e.IdKey == id);
        }

        [HttpPost]
        public JsonResult SaveAllBill([FromBody] JObject allBill)
        {

            dynamic dynamicAllBill = allBill;

            //------------CONSECUTIVE RECORD---------------- -
            MasterConsecutive masterConsecutive = new MasterConsecutive();

            string branchOffice = dynamicAllBill.branchOffice;
            string terminal = dynamicAllBill.terminal;
            int voucherType = dynamicAllBill.voucherType;
            string voucherTypeSymbology = "";

            if (voucherType == 1)
            {
                voucherTypeSymbology = "FE";
            }
            else
            {
                voucherTypeSymbology = "TE";
            }

            masterConsecutive.BranchOffice = branchOffice;
            masterConsecutive.Terminal = terminal;
            masterConsecutive.VoucherType = voucherTypeSymbology;

            int lastConsecutiveByBranchAndTerminal = _context.MasterConsecutive.Where(mc => mc.BranchOffice.Equals(branchOffice) & mc.Terminal.Equals(terminal)).Count();

            string consecutive = getNumber(lastConsecutiveByBranchAndTerminal, "consecutive");

            masterConsecutive.NumberConsecutive = consecutive;

            _context.MasterConsecutive.Add(masterConsecutive);
            _context.SaveChanges();

            // -----------FINISH CONSECUTIVE RECORD -------------

            //-----------MASTER KEY RECORD ---------------------

            MasterKey masterKey = new MasterKey();

            string country = dynamicAllBill.country;
            string dayCode = dynamicAllBill.day;

            if (dayCode.Length == 1)
            {
                dayCode = "0" + dayCode;
            }

            string monthCode = dynamicAllBill.month;

            if (monthCode.Length == 1)
            {
                monthCode = "0" + monthCode;
            }

            int year = dynamicAllBill.year;
            string yearCode = (year % 100).ToString();
            string transmitter = dynamicAllBill.idTransmitter;

            int idConsecutive = _context.MasterConsecutive.Last().IdConsecutive;

            int situation = dynamicAllBill.situation;

            int lastNumberKey = _context.MasterKey.Count();

            string number = getNumber(lastNumberKey, "masterKey");

            masterKey.Country = country;
            masterKey.Day = dayCode;
            masterKey.Month = monthCode;
            masterKey.Year = yearCode;
            masterKey.IdTrasmitter = transmitter;
            masterKey.IdConsecutive = idConsecutive;
            masterKey.IdSituation = situation;
            masterKey.SecurityCode = number;

            _context.MasterKey.Add(masterKey);
            _context.SaveChanges();

            // -----------FINISH MASTER KEY RECORD--------------

            //---------- - INVOICE VOUCHER RECORD ----------------
            MasterInvoiceVoucher masterInvoiceVoucher = new MasterInvoiceVoucher();

            int lastMasterKey = _context.MasterKey.Last().IdKey;
            int idPayment = dynamicAllBill.idPayment;
            int idCondition = dynamicAllBill.idCondition;

            masterInvoiceVoucher.IdKey = lastMasterKey;
            masterInvoiceVoucher.IdPayment = idPayment;
            masterInvoiceVoucher.IdCondition = idCondition;

            _context.MasterInvoiceVoucher.Add(masterInvoiceVoucher);
            _context.SaveChanges();

            // ----------FINISH INVOICE VOUCHER RECORD----------

            //------------ - CLIENT RECEIVER RECORD --------------

            string[] cliID = new string[2];
            string clientEmail = "";

            if (voucherType == 1)
            {
                int clientId = dynamicAllBill.clientId;

                Client client = _context.Client.Find(clientId);

                MasterReceiver mr = new MasterReceiver();
                mr.IdKey = lastMasterKey;
                mr.Fullname = client.Name + " " + client.LastName;
                mr.IdentificationNumber = client.IdentificationNumber;
                mr.IdentificationType = _context.IdentificationType.Find(client.IdType).Symbology;
                mr.Email = client.Email;
                clientEmail = client.Email;
                mr.Telephone = _context.TelephoneContact.Where(tc => tc.IdOwner.Equals(client.IdentificationNumber)).First().TelephoneNumber;
                mr.CountryCode = "506";

                _context.MasterReceiver.Add(mr);
                _context.SaveChanges();
                cliID[0] = mr.IdentificationType;
                cliID[1] = mr.IdentificationNumber;
            }

            //-----------FINISH CLIENT RECEIVER RECORD-------- -

           List < ProductDetail > productDetailsList = new List<ProductDetail>();

            foreach (dynamic dynamicbpd in dynamicAllBill.billProductDetails)
            {
                ProductDetail pd = new ProductDetail();
                pd.IdProduct = dynamicbpd.IdProduct;
                pd.CodeProduct = dynamicbpd.CodeProduct;
                pd.NameProduct = dynamicbpd.NameProduct;
                pd.UnitPrice = dynamicbpd.UnitPrice;
                pd.Quantity = dynamicbpd.Quantity;
                pd.TotalAmount = dynamicbpd.TotalAmount;
                pd.IdUnit = dynamicbpd.IdUnit;
                productDetailsList.Add(pd);
            }

            List<MasterDetail> mdList = SaveProductDetails(productDetailsList, lastMasterKey);
            Models.Facturar_Hacienda.Hacienda hacienda = new Models.Facturar_Hacienda.Hacienda(_context);
            Models.Facturar_Hacienda.FE fe = new Models.Facturar_Hacienda.FE(_context);
            //------------------Send to Ministerio Hacienda------------------
            hacienda.CreateKey(lastMasterKey, voucherTypeSymbology,"normal", consecutive, number, terminal, branchOffice);
            string[] vales = fe.CreateXML(mdList, lastMasterKey, voucherTypeSymbology);

            string[] token = hacienda.Token();

            hacienda.SendHacienda(token, vales[0], vales[1], voucherTypeSymbology, cliID);

            if (voucherType == 1)
            {
                SendSignedXMLToClient(clientEmail, vales[0]);
            }

            //sendMailToClient();

            return Json(new { state = 0, message = string.Empty });
        }

        public List<MasterDetail> SaveProductDetails(List<ProductDetail> billDetails, int key)
        {
            List<MasterDetail> mdList = new List<MasterDetail>();
            MasterInvoiceVoucher miv = _context.MasterInvoiceVoucher.Find(key);

            foreach (ProductDetail pd in billDetails)
            {
                MasterDetail masterDetail = new MasterDetail();
                masterDetail.IdKey = key;
                masterDetail.Code = pd.CodeProduct;
                masterDetail.NameProduct = pd.NameProduct;
                masterDetail.MeasuredUnitSymbology = _context.MeasuredUnit.Find(pd.IdUnit).Symbology;
                masterDetail.Quantity = pd.Quantity;
                masterDetail.UnitPrice = pd.UnitPrice;
                masterDetail.TotalAmount = pd.TotalAmount;

                double discount = 0;

                masterDetail.DiscountAmount = discount;
                masterDetail.Subtotal = pd.TotalAmount - discount;

                double totalLineAmount = pd.TotalAmount - discount;

                masterDetail.TotalLineAmount = totalLineAmount;

                _context.MasterDetail.Add(masterDetail);
                _context.SaveChanges();

                mdList.Add(masterDetail);
            }

            return mdList;
        }

        private void SendSignedXMLToClient(string clientEmail, string signedXML)
        {
            byte[] decodedXML = Convert.FromBase64String(signedXML);

            Email email = new Email(clientEmail, decodedXML, "XMLFirmado.xml");

            if (email.sendEmail())
            {
                Debug.WriteLine("---------------------------EMAIL SEND");
            }
            else
            {
                Debug.WriteLine("--------------------------- EMAIL SEND FAILED: " + email.error);
            }
        }

        [HttpPost]
        public JsonResult SendPDFToClient([FromBody] JObject billPDF)
        {
            dynamic dynamicBillPDF = billPDF;

            string clientEmail = dynamicBillPDF.clientEmail;
            string base64PDF = dynamicBillPDF.base64PDF;
            int size = base64PDF.Length - 1;

            Debug.WriteLine(base64PDF);
            Debug.WriteLine(size);

            string onlyBase64 = base64PDF.Substring(28);

            Debug.WriteLine(onlyBase64);

            byte[] decodedPDF = Convert.FromBase64String(onlyBase64);

            Email email = new Email(clientEmail, decodedPDF, "Factura.pdf");

            if (email.sendEmail())
            {
                Debug.WriteLine("---------------------------EMAIL SEND");
            }
            else
            {
                Debug.WriteLine("--------------------------- EMAIL SEND FAILED: " + email.error);
            }

            return Json(new { state = 0, message = string.Empty });
        }

        private string getNumber(int lastNumber, string type)
        {
            string strNumber = (lastNumber + 1).ToString();
            string strNewNumber = "";

            int numberKeyLength = strNumber.Length;

            if (type.Equals("masterKey"))
            {
                switch (numberKeyLength)
                {
                    case 1:
                        strNewNumber = "0000000" + strNumber;
                        break;
                    case 2:
                        strNewNumber = "000000" + strNumber;
                        break;
                    case 3:
                        strNewNumber = "00000" + strNumber;
                        break;
                    case 4:
                        strNewNumber = "0000" + strNumber;
                        break;
                    case 5:
                        strNewNumber = "000" + strNumber;
                        break;
                    case 6:
                        strNewNumber = "00" + strNumber;
                        break;
                    case 7:
                        strNewNumber = "0" + strNumber;
                        break;
                    default:
                        strNewNumber = strNumber;
                        break;
                }
            }
            else
            {
                switch (numberKeyLength)
                {
                    case 1:
                        strNewNumber = "000000000" + strNumber;
                        break;
                    case 2:
                        strNewNumber = "00000000" + strNumber;
                        break;
                    case 3:
                        strNewNumber = "0000000" + strNumber;
                        break;
                    case 4:
                        strNewNumber = "000000" + strNumber;
                        break;
                    case 5:
                        strNewNumber = "00000" + strNumber;
                        break;
                    case 6:
                        strNewNumber = "0000" + strNumber;
                        break;
                    case 7:
                        strNewNumber = "000" + strNumber;
                        break;
                    case 8:
                        strNewNumber = "00" + strNumber;
                        break;
                    case 9:
                        strNewNumber = "0" + strNumber;
                        break;
                    default:
                        strNewNumber = strNumber;
                        break;
                }
            }

            return strNewNumber;
        }

    }
}
