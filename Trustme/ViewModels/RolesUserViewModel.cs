using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trustme.Models;

namespace Trustme.ViewModels
{
    public class RolesUserViewModel
    {
        public User User { get; set; }
        public List<Role> Roles { get; set; }
    }
}
