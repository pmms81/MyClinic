using MyClinicWebAPI.DataLayer;
using System.ComponentModel.DataAnnotations;

namespace MyClinicWebAPI.Models
{
    public class PrescriptionModel
    {
        [Key]
        public int IDPrescription {  get; set; }
        public string Description { get; set; } = "";

    }
}
