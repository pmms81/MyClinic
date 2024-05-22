using System.ComponentModel.DataAnnotations;

namespace MyClinic.Models
{
    public class PrescriptionModel
    {
        [Key]
        public int IDPrescription { get; set; }
        public string? Description { get; set; }
    }
}
