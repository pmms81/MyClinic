using System.ComponentModel.DataAnnotations;

namespace MyClinicWebAPI.Dto
{
    public class ClientDto
    {
        [Required]
        //[Range(1,10000)]
        public int ID { get; set; }
        [Required]
        [MinLength(4,ErrorMessage = "Name must be 5 characters")]
        [MaxLength(50, ErrorMessage = "Name cannot be over 50 characters")]
        public string? Name { get; set; } = "";
        [Required]
        public string Email { get; set; } = "";
    }
}
