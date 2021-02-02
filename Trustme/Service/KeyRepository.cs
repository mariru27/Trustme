using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trustme.Models;
using Trustme.IServices;
using Trustme.Data;

namespace Trustme.Service
{
    public class KeyRepository : IKeyRepository
    {
        private UserKeyContext _context;
        KeyRepository(UserKeyContext context)
        {
            _context = context;
        }
        public void addKey(Key key)
        {
            _context.Key.Add(key);
            _context.SaveChanges();
        }

        public void delete()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Key> listAllKeys(User user)
        {
            throw new NotImplementedException();
        }

        public void update()
        {
            throw new NotImplementedException();
        }
    }
}
