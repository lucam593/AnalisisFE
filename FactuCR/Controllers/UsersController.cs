using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FactuCR.Models;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;

namespace FactuCR.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        private readonly db_facturacionContext _context;

        public UsersController(db_facturacionContext context)
        {
            _context = context;
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            var values = new Dictionary<string, string>
                {
                   { "iam", User.Identity.Name },
                   { "sessionKey", User.Claims.First(c => c.Type == "SessionKey").Value}
                };
            ApiConnect api = new ApiConnect();
            JToken jObjet = api.PostApi(values, "users", "users_log_me_out");
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Login");
        }
        // GET: Users/Edit/5
        public async Task<IActionResult> Index()
        {
            uint? id = Convert.ToUInt32(User.Claims.First(c => c.Type == "idUser").Value);
            if (id == null)
            {
                return NotFound();
            }

            var users = await _context.Users.FindAsync(id);
            if (users == null)
            {
                return NotFound();
            }
            //Unix timestamp is seconds past epoch
            DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(users.LastAccess).ToLocalTime();

            ViewBag.Message = dtDateTime.ToString();
            return View(users);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index([Bind("IdUser,FullName,UserName,Email,LastAccess,Pwd,ConfirmPassword")] Users users)
        {
            uint? id = Convert.ToUInt32(User.Claims.First(c => c.Type == "idUser").Value);
            if (id != users.IdUser)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {

                    var values = new Dictionary<string, string>
                {
                   { "idUser", Convert.ToString(  users.IdUser) },
                   { "fullName", users.FullName },
                   { "pwd", users.Pwd },
                   { "iam", User.Identity.Name },
                   { "sessionKey", User.Claims.First(c => c.Type == "SessionKey").Value}
                };
                    ApiConnect api = new ApiConnect();
                    JToken jObjet = api.PostApi(values, "users", "users_update_profile");
                    var status = (string)jObjet["status"];

                }
                catch (Exception)
                {
                    if (!UsersExists(users.IdUser))
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
            return View(users);
        }

        private bool UsersExists(uint id)
    {
        return _context.Users.Any(e => e.IdUser == id);
    }
}
}
