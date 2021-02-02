using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trustme.Models;
using Trustme.ViewModels;

namespace Trustme.IServices
{
    interface IKeyRepository
    {
        public void addKey(UserKeyModel _UserKeyModel);
        public void delete(UserKeyModel _UserKeyModel);
        public void update(UserKeyModel _UserKeyModel);
        public IEnumerable<Key> listAllKeys(User user);

    }
}
