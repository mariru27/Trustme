using Trustme.Models;

namespace Trustme.ViewModels
{
    public class UserKeyModel
    {
        public Key Key { get; set; } = new Key();
        public User User { get; set; } = new User();
    }
}
