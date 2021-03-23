using System.Collections.Generic;
using Trustme.Models;

namespace Trustme.ViewModels
{
    public class UserKeysRoleModel
    {
        public IEnumerable<Key> Keys;
        public User User;
        public Role Role;
    }
}
