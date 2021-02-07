using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trustme.Models;
using Trustme.ViewModels;

namespace Trustme.IServices
{
    public interface IKeyRepository
    {
        public void AddKey(UserKeyModel _UserKeyModel);
        public void Delete(UserKeyModel _UserKeyModel);
        public void Update(UserKeyModel _UserKeyModel);
        public IEnumerable<Key> ListAllKeys(User _User);
        public UserKeyModel CreateDefaultUserKeyModel();


    }
}
