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
        public void addRole(Role _Role)
        {
            _context.Role.Add(_Role);
            _context.SaveChanges();
        }

        public void deleteRole(Role _Role)
        {
            _context.Role.Remove(_Role);
            _context.SaveChanges();
        }

        public IEnumerable<Role> listAllRoles()
        {
            return _context.Role.ToList();
        }

        public void updateRole(Role _Role)
        {
            _context.Role.Update(_Role);
        }


    }
}
