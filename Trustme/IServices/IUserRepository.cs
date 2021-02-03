using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trustme.Models;

namespace Trustme.IServices
{
    interface IUserRepository
    {
        public void add(User _User);
        public void delete(User _User);
        public void update();
        public IEnumerable<User> listAllUsers();
    }
}
