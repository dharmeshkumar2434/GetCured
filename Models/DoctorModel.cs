using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GetCuredProject.Models
{
    [Table("Doctor")]
    public class DoctorModel
    {
        [Required]
        public int Id { get; set; }
        [DisplayName("Doctor's Name")]
        public string DName { get; set; }
        [DisplayName("Phone Number")]
        public string DPhone { get; set; }
        [DisplayName("E-mail")]
        [EmailAddress]
        public string DEmail { get; set; }
        [DisplayName("Gender")]
        public string DGender { get; set; }
        [DisplayName("Qualification")]
        [Required]
        public string DQualification { get; set; }
        [DisplayName("Specialization")]
        [Required]
        public string DSpecialisation { get; set; }
        [DisplayName("Experience")]
        public string DExperience { get; set; }
        [DisplayName("Rating")]
        public string DRating { get; set; }
        [DisplayName("Doctor's Image")]
        public string FileName { get; set; }
        [DisplayName("Consultation charge")]
        public double DFee { get; set; }

        [ForeignKey("Hospital")]
        public int HospitalId { get; set; }
        public HospitalOrClinicModel Hospital { get; set; }


        /// <summary>
        /// Format will be HH:MM
        /// </summary>
        public string FromTime { get; set; }

        /// <summary>
        /// Format will be HH:MM
        /// </summary>
        public string ToTime { get; set; }

        [NotMapped]
        public string DoctorFullInfo
        {
            get
            {
                return $"{DName} {FromTime} to {ToTime}";
            }
        }
    }
}
