using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GetCuredProject.Models
{
    [Table("HospitalOrClinic")]
    public class HospitalOrClinicModel
    {
        public int Id { get; set; }
        [Required]
        public string ClinicName { get; set; }
        [Required]
        public string ClinicAddress { get; set; }
        public string CabinNo { get; set; }
        [DisplayName("Image")]
        public string FileName { get; set; }

    }
}
