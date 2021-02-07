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
        public void AddRole(Role _Role);
        public void DeleteRole(Role _Role);
        public void UpdateRole(Role _Role);
        public IEnumerable<Role> ListAllRoles();
    }
}
