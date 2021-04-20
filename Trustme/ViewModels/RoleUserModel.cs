using Trustme.Models;

namespace Trustme.ViewModels
{
    public class RoleUserModel
    {
        public User User { get; set; } = new User();
        public Role Role { get; set; } = new Role();
    }
}
