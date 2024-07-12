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

namespace GetCuredProject.Controllers
{
    [Authorize]
    public class AppointmentController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AppointmentController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Appointment
        public async Task<IActionResult> Index(bool? booked)
        {
            ViewBag.NewBooked = booked;
            var applicationDbContext = _context.Appointment
                .Include(a => a.Doctor)
                .Include(a => a.Patient);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Appointment/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointmentModel = await _context.Appointment
                .Include(a => a.Doctor)
                .Include(a => a.Patient)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (appointmentModel == null)
            {
                return NotFound();
            }

            return View(appointmentModel);
        }

        // GET: Appointment/Create
        public IActionResult Create()
        {
            ViewData["DoctorId"] = new SelectList(_context.Doctor, "Id", "DoctorFullInfo");
            ViewData["PatientId"] = new SelectList(_context.Patients, "Id", "PName");
            return View();
        }

        // POST: Appointment/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see ht
        //
        // tp://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AppointmentModel appointmentModel)
        {
            if (ModelState.IsValid)
            {
                if (_context.Appointment.Any(a => a.AppoitmentDate == appointmentModel.AppoitmentDate && a.PatientId == appointmentModel.PatientId && a.DoctorId == appointmentModel.DoctorId))
                {
                    ViewData["DoctorId"] = new SelectList(_context.Doctor, "Id", "DoctorFullInfo", appointmentModel.DoctorId);
                    ViewData["PatientId"] = new SelectList(_context.Patients, "Id", "PName", appointmentModel.PatientId);
                    ModelState.AddModelError("", "Same appointment is already present.");
                    return View(appointmentModel);
                }
                DoctorModel doctor = _context.Doctor.Find(appointmentModel.DoctorId);

                TimeSpan fromTime = TimeSpan.Parse(doctor.FromTime);
                TimeSpan toTime = TimeSpan.Parse(doctor.ToTime);
                TimeSpan appointmentTime = TimeSpan.Parse(appointmentModel.AppoitmentDate.ToString("HH:mm"));
                if (appointmentTime >= fromTime && appointmentTime <= toTime)
                {
                    _context.Add(appointmentModel);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index), new { booked = true });
                }
                else
                {
                    ModelState.AddModelError(String.Empty, "Invalid time");

                    ViewData["DoctorId"] = new SelectList(_context.Doctor, "Id", "DoctorFullInfo", appointmentModel.DoctorId);
                    ViewData["PatientId"] = new SelectList(_context.Patients, "Id", "PName", appointmentModel.PatientId);
                    return View(appointmentModel);
                }

            }
            ViewData["DoctorId"] = new SelectList(_context.Doctor, "Id", "DoctorFullInfo", appointmentModel.DoctorId);
            ViewData["PatientId"] = new SelectList(_context.Patients, "Id", "PName", appointmentModel.PatientId);
            return View(appointmentModel);
        }

        // GET: Appointment/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointmentModel = await _context.Appointment.FindAsync(id);
            if (appointmentModel == null)
            {
                return NotFound();
            }
            ViewData["DoctorId"] = new SelectList(_context.Doctor, "Id", "DName", appointmentModel.DoctorId);
            ViewData["PatientId"] = new SelectList(_context.Patients, "Id", "PName", appointmentModel.PatientId);
            return View(appointmentModel);
        }

        // POST: Appointment/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,AppoitmentDate,PatientId,DoctorId")] AppointmentModel appointmentModel)
        {
            if (id != appointmentModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (_context.Appointment.Any(a => a.AppoitmentDate == appointmentModel.AppoitmentDate && a.PatientId == appointmentModel.PatientId && a.DoctorId == appointmentModel.DoctorId))
                    {
                        ViewData["DoctorId"] = new SelectList(_context.Doctor, "Id", "DoctorFullInfo", appointmentModel.DoctorId);
                        ViewData["PatientId"] = new SelectList(_context.Patients, "Id", "PName", appointmentModel.PatientId);
                        ModelState.AddModelError("", "Same appointment is already present.");
                        return View(appointmentModel);
                    }
                    _context.Update(appointmentModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AppointmentModelExists(appointmentModel.Id))
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
            ViewData["DoctorId"] = new SelectList(_context.Doctor, "Id", "DName", appointmentModel.DoctorId);
            ViewData["PatientId"] = new SelectList(_context.Patients, "Id", "PName", appointmentModel.PatientId);
            return View(appointmentModel);
        }

        // GET: Appointment/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointmentModel = await _context.Appointment
                .Include(a => a.Doctor)
                .Include(a => a.Patient)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (appointmentModel == null)
            {
                return NotFound();
            }

            return View(appointmentModel);
        }

        // POST: Appointment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var appointmentModel = await _context.Appointment.FindAsync(id);
            _context.Appointment.Remove(appointmentModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AppointmentModelExists(int id)
        {
            return _context.Appointment.Any(e => e.Id == id);
        }
    }
}
