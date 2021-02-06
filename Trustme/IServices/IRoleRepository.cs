using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trustme.Models;
using Trustme.ViewModels;

namespace Trustme.IServices
{
    public interface IRoleRepository
    {
        public void addRole(Role _Role);
        public void deleteRole(Role _Role);
        public void updateRole(Role _Role);
        public IEnumerable<Role> listAllRoles();
    }
}
