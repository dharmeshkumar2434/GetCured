
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GetCuredProject.Models
{
    [Table("Patient")]
    public class PatientsModel
    {
        public int Id { get; set; }
        [DisplayName("Patient's Name")]
        [Required]
        public string PName { get; set; }
        [DisplayName("Phone Number")]
        public string PPhone { get; set; }
        [DisplayName("E-mail")]
        public string PEmail { get; set; }
        [DisplayName("Gender")]
        public string PGender { get; set; }
        [DisplayName("Patient's Image")]
        public string FileName { get; set; }
        [DisplayName("Patient's Age")]
        [Required]
        public string PAge { get; set; }
        [DisplayName("Address")]
        [Required]
        public string PAddress { get; set; }
        [DisplayName("Weight")]
        public string Weight { get; set; }

        public bool Vaccinated { get; set; }
    }
}