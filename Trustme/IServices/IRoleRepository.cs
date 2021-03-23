using System.Collections.Generic;
using Trustme.Models;

namespace Trustme.IServices
{
    public interface IRoleRepository
    {
        public void AddRole(Role _Role);
        public void DeleteRole(Role _Role);
        public void UpdateRole(Role _Role);
        public IEnumerable<Role> ListAllRoles();

        public Role GetRoleById(int id);

        public Role GetUserRole(User User);
    }
}
