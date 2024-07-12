using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using GetCuredProject.Models;

namespace GetCuredProject.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<AppointmentModel> Appointment { get; set; }
        public DbSet<DoctorModel> Doctor { get; set; }
        public DbSet<HospitalOrClinicModel> HospitalOrClinic { get; set; }
        public DbSet<PatientsModel> Patients { get; set; }
    }
}
