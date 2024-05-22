using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyClinic.Models
{
    public class ClientRoleModel
    {
        [Key]
        public int IDClientRole {  get; set; }
        [ForeignKey("Client")]
        public int IDClient { get; set; }
        [ForeignKey("Role")]
        public int IDRole { get; set; }

        //public RoleModel Role { get; set; } 
        
        //public ICollection<RoleModel>? Roles {get; set;}
    }
}
