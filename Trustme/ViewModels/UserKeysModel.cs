using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trustme.Models;

namespace Trustme.ViewModels
{
    public class UserKeysModel
    {
        public List<Key> Keys = new List<Key>();
        public User User;
    }
}
