using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trustme.Models;

namespace Trustme.IServices
{
    interface IKeyRepository
    {
        public void addKey(Key key);
        public void delete();
        public void update();
        public IEnumerable<Key> listAllKeys(User user);

    }
}
