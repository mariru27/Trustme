using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trustme.Models;

namespace Trustme.IServices
{
    interface IKeyRepository
    {
        public void add();
        public void delete();
        public void update();
        public Enumerable listAllKeys(User user);

    }
}
