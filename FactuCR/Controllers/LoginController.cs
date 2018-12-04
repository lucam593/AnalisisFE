﻿using System;
using System.Collections.Generic;
using System.Security.Claims;
using FactuCR.Models;
using FactuCR.Models.Login;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Authentication;
using System.Threading;
using Microsoft.Extensions.DependencyInjection;

namespace FactuCR.Controllers
{
   
    public class LoginController : Controller
    {
        private readonly db_facturacionContext _context;

        public LoginController(db_facturacionContext context)
        {
            _context = context;
        }
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

                TempData["UserLoginFailed"] = "La conexión falló. Verifica que ingresaste los datos correctos.";
                return View();
            }
            return View();
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            return View();
        }
        /*La creacion de Usuario
         * POST CREATE
         * ENVIA LA SOLICITUD AL API
         * SI EL MODELO ES CORRECTO Y EL API TIENE ESTADO SATISFACTORIO
         * CREA UN NUEVO USUARIO, CONTRASEÑA ENCRIPTADA
         * EL NUEVO USUARIO ESTARA LOGUEADO
         * VALIDA QUE NO SEA LA PRIMERA VEZ
         * REDIRECCIONA A HOME*/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async System.Threading.Tasks.Task<IActionResult> Create([Bind("FullName,UserName,Email,,Pwd,ConfirmPassword")] Users users)
        {
            if (ModelState.IsValid)
            {

                try
                {
                    int count = 0;
                    if (_context.Users.Find(1) == null)
                    {
                        count++;
                    }
                    
                    
                    var values = new Dictionary<string, string>
                {
                   { "fullName", users.FullName },
                   { "userName", users.UserName },
                   { "email", users.Email },
                   { "pwd", users.Pwd }
                };
                    ApiConnect api = new ApiConnect();
                    JToken jObjet = api.PostApi(values, "users", "users_register");
                    var status = (string)jObjet["status"];
                    TempData["Fail"] = status;
                    var sessionKey = (string)jObjet["sessionKey"];
                    var user = (string)jObjet["userName"];
                    var idUser = (string)jObjet["idUser"];
                    await genClaimsAsync(user, sessionKey, idUser);
                    ModelState.Clear();
                    TempData["Success"] = "Registro Exitoso";

                    if (count == 0)
                    {
                        return RedirectToAction("Create", "ConfigCompanies");
                    }
                    return RedirectToAction("Index", "Home");
                }
                catch (Exception e)
                {
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

        }



}