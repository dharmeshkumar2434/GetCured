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
using Microsoft.AspNetCore.Http;
using System.IO;

namespace GetCuredProject.Controllers
{
    [Authorize]
    public class DoctorController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _env;

        public DoctorController(ApplicationDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // GET: Doctor
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Doctor
                .Include(d => d.Hospital);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Doctor/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var doctorModel = await _context.Doctor
                .Include(d => d.Hospital)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (doctorModel == null)
            {
                return NotFound();
            }

            return View(doctorModel);
        }

        // GET: Doctor/Create
        public IActionResult Create()
        {
            ViewData["HospitalId"] = new SelectList(_context.HospitalOrClinic, "Id", "ClinicName");
            return View();
        }

        // POST: Doctor/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DoctorModel doctorModel,IFormFile file)
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
                doctorModel.FileName = fileName;
                _context.Add(doctorModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["HospitalId"] = new SelectList(_context.HospitalOrClinic, "Id", "ClinicName", doctorModel.HospitalId);
            return View(doctorModel);
        }

        // GET: Doctor/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var doctorModel = await _context.Doctor.FindAsync(id);
            if (doctorModel == null)
            {
                return NotFound();
            }
            ViewData["HospitalId"] = new SelectList(_context.HospitalOrClinic, "Id", "ClinicName", doctorModel.HospitalId);
            return View(doctorModel);
        }

        // POST: Doctor/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DName,DPhone,DEmail,DGender,DQualification,DSpecialisation,DExperience,DRating,DFee,HospitalId,FromTime,ToTime")] DoctorModel doctorModel, IFormFile file)
        {
            if (id != doctorModel.Id)
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
                    doctorModel.FileName = fileName;
                    _context.Update(doctorModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DoctorModelExists(doctorModel.Id))
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
            ViewData["HospitalId"] = new SelectList(_context.HospitalOrClinic, "Id", "ClinicName", doctorModel.HospitalId);
            return View(doctorModel);
        }

        // GET: Doctor/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var doctorModel = await _context.Doctor
                .Include(d => d.Hospital)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (doctorModel == null)
            {
                return NotFound();
            }

            return View(doctorModel);
        }

        // POST: Doctor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var doctorModel = await _context.Doctor.FindAsync(id);
            _context.Doctor.Remove(doctorModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DoctorModelExists(int id)
        {
            return _context.Doctor.Any(e => e.Id == id);
        }
    }
}
