using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FactuCR.Models;
using Newtonsoft.Json.Linq;

namespace FactuCR.Controllers
{
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

        // GET: Users/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("FullName,UserName,Email,About,Country,Status,Pwd")] Users users)
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
                    //LoginController login = new LoginController();
                }
                catch (Exception e)
                {
                    //var status = (string)JObject["status"];
                    ModelState.AddModelError("", e.Message);
                }
            }
            return View(users);
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
