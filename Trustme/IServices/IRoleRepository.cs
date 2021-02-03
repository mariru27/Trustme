using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trustme.Models;
using Trustme.ViewModels;

namespace Trustme.IServices
{
    interface IRoleRepository
    {
        public void addRole(Role _Role);
        public void delete(Role _Role);
        public void update(Role _Role);
        public IEnumerable<Role> listAllRoles();
    }
}
