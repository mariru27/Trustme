using System.ComponentModel.DataAnnotations;

namespace Trustme.Models
{
    public class Role
    {
        public Role() { }
        public Role(Role role) 
        {
            this.IdRole = role.IdRole;
            this.RoleName = role.RoleName;
        }
        [Key]
        public int IdRole { get; set; }
        public string RoleName { get; set; }
    }
}
