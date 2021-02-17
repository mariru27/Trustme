using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trustme.IServices;
using Trustme.Models;
using Trustme.ViewModels;
using Trustme.Data;

namespace Trustme.Service
{
    public class RoleRepository : IRoleRepository
    {
        private AppContext _context;
        public RoleRepository(AppContext context)
        {
            _context = context;
        }
        public void AddRole(Role _Role)
        {
            _context.Role.Add(_Role);
            _context.SaveChanges();
        }

        public void DeleteRole(Role _Role)
        {
            _context.Role.Remove(_Role);
            _context.SaveChanges();
        }

        public Role GetRoleById(int roleId)
        {
            Role role = _context.Role.Where(a => a.IdRole == roleId).SingleOrDefault();
            return role;
        }

        public Role GetUserRole(User User)
        {
            return _context.Role.Where(role => role.IdRole == User.RoleId)?.SingleOrDefault();
        }

        public IEnumerable<Role> ListAllRoles()
        {
            return _context.Role.ToList();
        }

        public void UpdateRole(Role _Role)
        {
            _context.Role.Update(_Role);
        }



    }
}
