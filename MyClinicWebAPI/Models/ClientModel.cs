using System.ComponentModel.DataAnnotations;

namespace MyClinicWebAPI.Models
{
    public class ClientModel
    {
        [Key]
        public int ID { get; set; }
        public string? Name { get; set; } = "";
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = "";
        [DataType(DataType.Password)]
        public string Password { get; set; } = "";

    }
}

