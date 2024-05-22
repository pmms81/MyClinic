using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyClinic.Models
{
    public class RoleModel
    {
        [Key]
        [ForeignKey("ClientRole")]
        public int IDRole { get; set; }
        public string Role {  get; set; }
        public ClientRoleModel ClientRole { get; set; }
    }
}
