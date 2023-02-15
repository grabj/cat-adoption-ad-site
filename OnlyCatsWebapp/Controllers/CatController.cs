using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnlyCatsWebapp.Areas.Identity.Data;
using OnlyCatsWebapp.Core;
using OnlyCatsWebapp.Migrations;
using OnlyCatsWebapp.Models;
using static OnlyCatsWebapp.Core.Consts;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using System.Diagnostics.Metrics;
using Microsoft.AspNetCore.Hosting;
using System.Collections.Immutable;
using System.IO;
using System.Xml.Linq;
using System.Security.Claims;
using OnlyCatsWebapp.Core.ViewModels;

namespace OnlyCatsWebapp.Controllers
{
    public class CatController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly UserManager<ApplicationUser> _userManager;

        public CatController(ApplicationDbContext context, IWebHostEnvironment environment, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            webHostEnvironment = environment;
            _userManager = userManager;
        }

        // GET: Cat
        [Authorize(Roles = Roles.User)]
        public async Task<IActionResult> Index()
        {
              return View(await _context.Cats.ToListAsync());
        }

        // GET: Cat/Details/5
        [AllowAnonymous]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Cats == null)
            {
                return NotFound();
            }

            var cat = await _context.Cats
                .FirstOrDefaultAsync(m => m.CatId == id);
            if (cat == null)
            {
                return NotFound();
            }

            return View(cat);
        }

        // GET: Cat/Create
        [Authorize(Roles = Roles.User)]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = Roles.User)]
        public async Task<IActionResult> Create(CatViewModel model, IFormFile photo)
        {
            if (photo == null || photo.Length == 0)
            {
                return Content("File not selected");
            }
            string date = DateTime.Now.ToString("yyyy’-‘MM’-‘dd’-’HH’-’mm’-’ss");
            var path = Path.Combine((Path.Combine(webHostEnvironment.WebRootPath, "images")),  date + photo.FileName);

            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                await photo.CopyToAsync(stream);
                stream.Close();
            }

            model.Cat.ImageName = date + photo.FileName;
            model.Cat.DateAdded = DateTime.Today;

            //if (model != null)
            //{
                var cat = new Cat
                {
                    Name = model.Cat.Name,
                    Age = model.Cat.Age,
                    DateAdded = model.Cat.DateAdded,
                    ImageName = model.Cat.ImageName,
                    ImageLocation = path,
                    Description = model.Cat.Description,
                    UserId = model.Cat.UserId
                };
                _context.Add(cat);
                await _context.SaveChangesAsync();
           // }
            return RedirectToAction(nameof(Index));

        }

        // GET: Cat/Edit/5
        [Authorize(Roles = Consts.Roles.User)]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Cats == null)
            {
                return NotFound();
            }

            var cat = await _context.Cats.FindAsync(id);

            if (cat == null)
            {
                return NotFound();
            }

            var viewModel = new CatViewModel
            {
                Cat = cat
            };
            return View(viewModel);


            //return View(cat);
        }

        // POST: Cat/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = Consts.Roles.User)]
        public async Task<IActionResult> Edit(int id, CatViewModel model, IFormFile photo)
        {
            var cat = await _context.Cats.FindAsync(id);
            if (id !=cat.CatId)
            {
                return NotFound();
            }

            if (photo == null || photo.Length == 0)
            {
                return Content("File not selected");
            }
            string date = DateTime.Now.ToString("yyyy’-‘MM’-‘dd’-’HH’-’mm’-’ss");
            var path = Path.Combine((Path.Combine(webHostEnvironment.WebRootPath, "images")), date + photo.FileName);
            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                await photo.CopyToAsync(stream);
                stream.Close();
            }

            if (model !=null)
            {
                try
                {
                    cat.Name = model.Cat.Name;
                    cat.Age = model.Cat.Age;
                    cat.ImageName = model.Cat.ImageName;
                    cat.ImageLocation = path;
                    cat.Description = model.Cat.Description;

                    _context.Update(cat);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CatExists(model.Cat.CatId))
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
            return View(model);
        }

        // GET: Cat/Delete/5
        [Authorize(Roles = Consts.Roles.User)]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Cats == null)
            {
                return NotFound();
            }

            var cat = await _context.Cats
                .FirstOrDefaultAsync(m => m.CatId == id);
            if (cat == null)
            {
                return NotFound();
            }

            return View(cat);
        }

        // POST: Cat/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = Consts.Roles.User)]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Cats == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Cats'  is null.");
            }
            var cat = await _context.Cats.FindAsync(id);
            if (cat != null)
            {
                _context.Cats.Remove(cat);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CatExists(int id)
        {
          return _context.Cats.Any(e => e.CatId == id);
        }
    }
}
