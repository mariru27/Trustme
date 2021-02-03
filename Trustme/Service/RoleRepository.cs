using System;
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
        private RoleContext _context;
        RoleRepository(RoleContext context)
        {
            _context = context;
        }
        public void addRole(Role _Role)
        {
            _context.Role.Add(_Role);
            _context.SaveChanges();
        }

        public void delete()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> listAllUsers(Role role)
        {
            throw new NotImplementedException();
        }

        public void update()
        {
            throw new NotImplementedException();
        }


    }
}
