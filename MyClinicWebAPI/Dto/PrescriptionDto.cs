using System.ComponentModel.DataAnnotations;

namespace MyClinicWebAPI.Dto
{
    public class PrescriptionDto
    {
        [Required]
        public int IDPrescription { get; set; }
        [Required]
        [MaxLength(500, ErrorMessage = "Description cannot be over 500 characters")]
        public string Description { get; set; } = "";
    }
}
