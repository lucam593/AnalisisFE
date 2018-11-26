using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FactuCR.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.IO;

namespace FactuCR.Controllers
{
    [Authorize]
    public class FilesController : Controller
    {
        private readonly db_facturacionContext _context;

        public FilesController(db_facturacionContext context)
        {
            _context = context;
        }

        // GET: Files
        public async Task<IActionResult> Index()
        {
            return View(await _context.Files.ToListAsync());
        }

        // GET: Files/Details/5
        public async Task<IActionResult> Details(uint? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var files = await _context.Files
                .FirstOrDefaultAsync(m => m.IdFile == id);
            if (files == null)
            {
                return NotFound();
            }

            return View(files);
        }

        // GET: Files/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Files/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([FromForm]IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
            
            }

            return Ok();

                //try
                //{
                //    if (file != null && file.Length > 0)
                //    {
                //        using (var client = new HttpClient())
                //        {
                //            try
                //            {
                //                client.BaseAddress = new Uri("http://localhost/apiCRLibre/www/api.php");

                //                byte[] data;
                //                using (var br = new BinaryReader(file.OpenReadStream()))
                //                    data = br.ReadBytes((int)file.OpenReadStream().Length);

                //                ByteArrayContent bytes = new ByteArrayContent(data);


                //                MultipartFormDataContent multiContent = new MultipartFormDataContent();

                //                multiContent.Add(bytes, "file", file.FileName);
                //                //multiContent.Add(,,);

                //                var result = client.PostAsync("api/Values", multiContent).Result;


                //                return StatusCode((int)result.StatusCode); //201 Created the request has been fulfilled, resulting in the creation of a new resource.

                //            }

        }

        // GET: Files/Edit/5
        public async Task<IActionResult> Edit(uint? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var files = await _context.Files.FindAsync(id);
            if (files == null)
            {
                return NotFound();
            }
            return View(files);
        }

        // POST: Files/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(uint id, [Bind("IdFile,Md5,Name,Timestamp,Size,IdUser,DownloadCode,FileType,Type")] Files files)
        {
            if (id != files.IdFile)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(files);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FilesExists(files.IdFile))
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
            return View(files);
        }

        // GET: Files/Delete/5
        public async Task<IActionResult> Delete(uint? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var files = await _context.Files
                .FirstOrDefaultAsync(m => m.IdFile == id);
            if (files == null)
            {
                return NotFound();
            }

            return View(files);
        }

        // POST: Files/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(uint id)
        {
            var files = await _context.Files.FindAsync(id);
            _context.Files.Remove(files);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FilesExists(uint id)
        {
            return _context.Files.Any(e => e.IdFile == id);
        }
    }
}
