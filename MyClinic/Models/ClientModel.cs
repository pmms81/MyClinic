using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;

namespace MyClinic.Models
{
    public class ClientModel
    {
        [Key]
        [ForeignKey("ClientRole")]
        public int ID { get; set; }
        /*
         * ? -> Define a variable that can be null
         * ex: int? a = null;
         * Null Coalescing Operator:
         * // GetSomeValueMaybe() is a method returning an int? value
         * int? possible = GetSomeValueMaybe();
         * int definite = possible ?? 5; // Default to 5
         */
        public string? Name { get; set; } = "";
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = "";
        [DataType(DataType.Password)]
        public string Password { get; set; } = "";
        public ICollection<PrescriptionModel>? Prescriptions {get; set;} 
        //public ClientRoleModel? ClientRole { get; set; }
        //public ICollection<ClientRoleModel>? ClientRoles { get; set;}
    }
}
