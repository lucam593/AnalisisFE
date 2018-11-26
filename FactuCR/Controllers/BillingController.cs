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

namespace FactuCR.Controllers
{
    public class BillingController : Controller
    {
        private readonly db_facturacionContext _context;

        public BillingController(db_facturacionContext context)
        {
            _context = context;
        }

        // GET: Billing
        public async Task<IActionResult> Index()
        {
            var db_facturacionContext = _context.MasterInvoiceVoucher.Include(m => m.IdConditionNavigation).Include(m => m.IdKeyNavigation).Include(m => m.IdPaymentNavigation);
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

            //return View(new BillManagement());
            return View();
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

        private bool MasterInvoiceVoucherExists(int id)
        {
            return _context.MasterInvoiceVoucher.Any(e => e.IdKey == id);
        }

        [HttpPost]
        public JsonResult SaveAllBill([FromBody] JObject allBill)
        {
            dynamic dynamicAllBill = allBill;

            //------------ CONSECUTIVE RECORD -----------------
            MasterConsecutive masterConsecutive = new MasterConsecutive();

            string branchOffice = dynamicAllBill.branchOffice;
            string terminal = dynamicAllBill.terminal;
            int voucherType = dynamicAllBill.voucherType;
            string voucherTypeSymbology = "";

            if (voucherType == 1)
            {
                voucherTypeSymbology = "FE";
            }
            else {
                voucherTypeSymbology = "TE";
            }

            string consecutive = dynamicAllBill.consecutive;

            masterConsecutive.BranchOffice = branchOffice;
            masterConsecutive.Terminal = terminal;
            masterConsecutive.VoucherType = voucherTypeSymbology;

            int lastConsecutiveByBranchAndTerminal = _context.MasterConsecutive.Where(mc => mc.BranchOffice.Equals(branchOffice) & mc.Terminal.Equals(terminal)).Count();

            masterConsecutive.NumberConsecutive = getNumber(lastConsecutiveByBranchAndTerminal, "consecutive");

            _context.MasterConsecutive.Add(masterConsecutive);
            _context.SaveChanges();

            //----------- FINISH CONSECUTIVE RECORD -------------

            //----------- MASTER KEY RECORD ---------------------

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

            //----------- FINISH MASTER KEY RECORD --------------

            //----------- INVOICE VOUCHER RECORD ----------------
            MasterInvoiceVoucher masterInvoiceVoucher = new MasterInvoiceVoucher();

            int lastMasterKey = _context.MasterKey.Last().IdKey;
            int idPayment = dynamicAllBill.idPayment;
            int idCondition = dynamicAllBill.idCondition;

            masterInvoiceVoucher.IdKey = lastMasterKey;
            masterInvoiceVoucher.IdPayment = idPayment;
            masterInvoiceVoucher.IdCondition = idCondition;

            _context.MasterInvoiceVoucher.Add(masterInvoiceVoucher);
            _context.SaveChanges();

            //------------- FINISH INVOICE VOUCHER RECORD -------

            //------------- CLIENT RECEIVER RECORD --------------

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
                mr.Telephone = _context.TelephoneContact.Where(tc => tc.IdOwner.Equals(client.IdentificationNumber)).First().TelephoneNumber;
                mr.CountryCode = "506";

                _context.MasterReceiver.Add(mr);
                _context.SaveChanges();
            }

            //------------- FINISH CLIENT RECEIVER RECORD -------

            int IdClient = dynamicAllBill.clientId;

            List<ProductDetail> productDetailsList = new List<ProductDetail>();

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

            SaveProductDetails(productDetailsList, lastMasterKey);

            CreateKey(lastMasterKey, voucherTypeSymbology, "fisico", "112070714", "normal", consecutive, number, terminal, branchOffice);

            return Json(new { state = 0, message = string.Empty });
        }

        public void SaveProductDetails(List<ProductDetail> billDetails, int key)
        {
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
            }
        }

        public string getNumber(int lastNumber, string type)
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

        private void FE()
        {

            //var values = new Dictionary<string, string>
            //    {
            //       { "grant_type", "password" },
            //       { "client_id", "api-stag" },
            //       { "username", "cpf-01-1207-0714@stag.comprobanteselectronicos.go.cr" }, //usuario y contraseña se deben obtener de la BD
            //       { "password", ">|o+-JNb_G(1#9$xs}b]" }
            //    };
            //ApiConnect api = new ApiConnect();
            //JToken jObjet = api.PostApi(values, "token", "gettoken");
            //var sessionKey = (string)jObjet["sessionKey"];
            //var user = (string)jObjet["userName"];
            //var idUser = (string)jObjet["idUser"];

        }

        private void CreateKey(int idKey, string tipoDocumento, string tipoCedula, string cedula, string situacion, string consec, string codSeg, string ter, string sucr)
        {
            var values = new Dictionary<string, string>
                {
                   { "tipoCedula", tipoCedula },
                   { "cedula", cedula },
                   { "situacion", situacion },
                   { "codigoPais", "506" },
                   { "consecutivo", consec },
                   { "codigoSeguridad", codSeg },
                   { "tipoDocumento", tipoDocumento },
                   { "terminal", ter },
                   { "sucursal", sucr }
                };
            ApiConnect api = new ApiConnect();
            JToken jObjet = api.PostApi(values, "clave", "clave");
            var key = (string)jObjet["clave"];
            var consecutive = (string)jObjet["consecutivo"];

            MasterInvoiceVoucher miv = _context.MasterInvoiceVoucher.Find(idKey);

            miv.ApiKey = key;
            miv.ApiConsecutive = consecutive;

            _context.SaveChanges();
        }

        private void Token()
        {

            var values = new Dictionary<string, string>
                {
                   { "grant_type", "password" },
                   { "client_id", "api-stag" },
                   { "username", "cpf-01-1207-0714@stag.comprobanteselectronicos.go.cr" }, //usuario y contraseña se deben obtener de la BD
                   { "password", ">|o+-JNb_G(1#9$xs}b]" }
                };

            ApiConnect api = new ApiConnect();
            JToken jObjet = api.PostApi(values, "token", "gettoken");

            var access_token = (string)jObjet["access_token"];
            var refresh_token = (string)jObjet["refresh_token"];
            var expires_in = (string)jObjet["expires_in"];
        }
    }
}
