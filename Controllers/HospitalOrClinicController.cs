using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GetCuredProject.Data;
using GetCuredProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace GetCuredProject.Controllers
{
    [Authorize]
    public class HospitalOrClinicController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _env;

        public HospitalOrClinicController(ApplicationDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // GET: HospitalOrClinic
        public async Task<IActionResult> Index()
        {
            return View(await _context.HospitalOrClinic.ToListAsync());
        }

        // GET: HospitalOrClinic/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hospitalOrClinicModel = await _context.HospitalOrClinic
                .FirstOrDefaultAsync(m => m.Id == id);
            if (hospitalOrClinicModel == null)
            {
                return NotFound();
            }

            return View(hospitalOrClinicModel);
        }

        // GET: HospitalOrClinic/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: HospitalOrClinic/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ClinicName,ClinicAddress,CabinNo")] HospitalOrClinicModel hospitalOrClinicModel,IFormFile file)
        {
            if (ModelState.IsValid)
            {
                string fileName = string.Empty;
                if (file != null && file.Length > 0)
                {
                    fileName = file.FileName;
                    var path = Path.Combine(_env.WebRootPath, "uploads", file.FileName);
                    file.CopyTo(new FileStream(path, FileMode.Create));
                }
                hospitalOrClinicModel.FileName = fileName;
                _context.Add(hospitalOrClinicModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(hospitalOrClinicModel);
        }

        // GET: HospitalOrClinic/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hospitalOrClinicModel = await _context.HospitalOrClinic.FindAsync(id);
            if (hospitalOrClinicModel == null)
            {
                return NotFound();
            }
            return View(hospitalOrClinicModel);
        }

        // POST: HospitalOrClinic/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ClinicName,ClinicAddress,CabinNo")] HospitalOrClinicModel hospitalOrClinicModel, IFormFile file) 
        {
            if (id != hospitalOrClinicModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    string fileName = string.Empty;
                    if (file != null && file.Length > 0)
                    {
                        fileName = file.FileName;
                        var path = Path.Combine(_env.WebRootPath, "uploads", file.FileName);
                        file.CopyTo(new FileStream(path, FileMode.Create));
                    }
                    hospitalOrClinicModel.FileName = fileName;
                    _context.Update(hospitalOrClinicModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HospitalOrClinicModelExists(hospitalOrClinicModel.Id))
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
            return View(hospitalOrClinicModel);
        }

        // GET: HospitalOrClinic/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hospitalOrClinicModel = await _context.HospitalOrClinic
                .FirstOrDefaultAsync(m => m.Id == id);
            if (hospitalOrClinicModel == null)
            {
                return NotFound();
            }

            return View(hospitalOrClinicModel);
        }

        // POST: HospitalOrClinic/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var hospitalOrClinicModel = await _context.HospitalOrClinic.FindAsync(id);
            _context.HospitalOrClinic.Remove(hospitalOrClinicModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HospitalOrClinicModelExists(int id)
        {
            return _context.HospitalOrClinic.Any(e => e.Id == id);
        }
    }
}
