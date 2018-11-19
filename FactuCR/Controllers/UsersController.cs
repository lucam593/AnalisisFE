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

        // GET: Users
        public async Task<IActionResult> Index()
        {
         
            return View(await _context.Users.ToListAsync());

        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(uint? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var users = await _context.Users
                .FirstOrDefaultAsync(m => m.IdUser == id);
            if (users == null)
            {
                return NotFound();
            }

            return View(users);
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
                   { "sessionKey", User.Claims.First(c => c.Type == "SessionKey").ToString()}
                };
            ApiConnect api = new ApiConnect();
            JToken jObjet = api.PostApi( values,"users", "users_log_me_out");
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Login");
        }
        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(uint? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var users = await _context.Users.FindAsync(id);
            if (users == null)
            {
                return NotFound();
            }
            return View(users);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(uint id, [Bind("IdUser,FullName,UserName,Email,About,Country,Status,Timestamp,LastAccess,Pwd,Avatar,Settings")] Users users)
        {
            if (id != users.IdUser)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(users);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
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
