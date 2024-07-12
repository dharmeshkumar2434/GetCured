using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GetCuredProject.Models
{
    [Table("Appointment")]
    public class AppointmentModel
    {
        public int Id { get; set; }
        [Required]
        public DateTime AppoitmentDate { get; set; }

        [ForeignKey("Patient")]
        public int PatientId { get; set; }

        public PatientsModel Patient { get; set; }

        [ForeignKey("Doctor")]
        public int DoctorId { get; set; }

        public DoctorModel Doctor { get; set; }
    }
}
