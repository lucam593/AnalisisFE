using System;
using System.Collections.Generic;
using System.Security.Claims;
using FactuCR.Models;
using FactuCR.Models.Login;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Authentication;
using System.Threading;
using MailKit.Net.Smtp;
using MailKit;
using MimeKit;
using System.Net.Mail;
using Microsoft.Extensions.DependencyInjection;

namespace FactuCR.Controllers
{
    public class LoginController : Controller
    {
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                return Redirect("/Billing");
            }
            else
            {
                return View();

            }
        }
        // POST: Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual async System.Threading.Tasks.Task<ActionResult> Index([Bind("UserName,Pwd")] Login login)
        {
            try
            {
               
                if (ModelState.IsValid)
                {
                    var temp = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss%K");

                    var values = new Dictionary<string, string>
                {
                   { "userName", login.UserName },
                   { "pwd", login.Pwd }
                };
                    ApiConnect api = new ApiConnect();
                    JToken jObjet = api.PostApi(values, "users", "users_log_me_in");
                    var sessionKey = (string)jObjet["sessionKey"];
                    var user = (string)jObjet["userName"];
                    var idUser = (string)jObjet["idUser"];

                    await genClaimsAsync(user, sessionKey, idUser);
                   
                    
                    return RedirectToAction("Index", "Home");
                }




            }
            catch (Exception e)
            {

                TempData["UserLoginFailed"] = "Error al entrar.Por favor ingresa datos correctos";
                return View();
            }
            return View();
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async System.Threading.Tasks.Task<IActionResult> Create([Bind("FullName,UserName,Email,About,Country,Status,Pwd")] Users users)
        {
            if (ModelState.IsValid)
            {

                try
                {
                    var values = new Dictionary<string, string>
                {
                   { "fullName", users.FullName },
                   { "userName", users.UserName },
                   { "email", users.Email },
                   { "pwd", users.Pwd }
                };
                    ApiConnect api = new ApiConnect();
                    JToken jObjet = api.PostApi(values, "users", "users_register");
                    var sessionKey = (string)jObjet["sessionKey"];
                    var user = (string)jObjet["userName"];
                    var idUser = (string)jObjet["idUser"];
                    await genClaimsAsync(user, sessionKey, idUser);
                    ModelState.Clear();
                    TempData["Success"] = "Registro Exitoso";
                    return RedirectToAction("Create", "Files");
                }
                catch (Exception e)
                {
                    TempData["Fail"] = "Este Usuario ya existe. Registro fallido.";
                    return View();
                }
            }
            return View(users);
        }

        private async System.Threading.Tasks.Task<ClaimsPrincipal> genClaimsAsync(string UserName, string sessionKey, string id)
        {
            var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, UserName),
                        new Claim("SessionKey", sessionKey),
                        new Claim("idUser", id)
                    };
            ClaimsIdentity userIdentity = new ClaimsIdentity(claims, "login");
            ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);
            Thread.CurrentPrincipal = principal;
            await HttpContext.SignInAsync(
                principal, 
                properties: new AuthenticationProperties
            {
                ExpiresUtc = DateTime.UtcNow.AddMinutes(5)
            });
            return principal;
        }


        private async System.Threading.Tasks.Task enviarmailpruebaAsync()
        {
            using (var smtpClient = HttpContext.RequestServices.GetRequiredService<System.Net.Mail.SmtpClient>())
            {
                await smtpClient.SendMailAsync(new MailMessage(
                       "luis.castro@ucrso.info",
                       "sandy.ovg@gmail.com",
                       "Test message body",
                       "sirva pendejada"
                       ));

            }

                //var mail = new MimeMessage();
                //mail.From.Add(new MailboxAddress("FactuCR", "luis.castro@ucrso.info"));
                //mail.To.Add(new MailboxAddress("Mr Sandy", "sandy.ovg@gmail.com"));

                //mail.Subject = "PROBANDO CORREO DESDE C#";
                //mail.Body =  new TextPart("plain")
                //{
                //    Text = "nananana esta mierda sirve?"
                //};

                //using (var client = new SmtpClient())
                //{
                //    client.Connect("smtp-pulse.com", 465, true);
                //    client.Authenticate("lucam5993@gmail.com", "YP2g3Ab7jgYt");
                //    client.Send(mail);
                //    client.Disconnect(true);
                //}


                //srive??

            }
        }



}