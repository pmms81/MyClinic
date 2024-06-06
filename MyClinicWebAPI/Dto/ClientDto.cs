using System.ComponentModel.DataAnnotations;

namespace MyClinicWebAPI.Dto
{
    public class ClientDto
    {
        public int ID { get; set; }
        public string? Name { get; set; } = "";
        public string Email { get; set; } = "";
    }
}
