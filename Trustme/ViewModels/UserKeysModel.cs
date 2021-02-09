using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trustme.Models;

namespace Trustme.ViewModels
{
    public class UserKeysModel
    {
        public IEnumerable<Key> Keys;
        public User User;
    }
}
