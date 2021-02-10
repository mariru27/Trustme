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
        public void DeleteKey(UserKeyModel _UserKeyModel);
        public void UpdateKey(UserKeyModel _UserKeyModel);
        public IEnumerable<Key> ListAllKeys(User _User);
        public UserKeyModel CreateDefaultUserKeyModel();
        public Key GetKey(int userId, int keyId);

        public Key GetKeyById(int keyId);

        public bool KeyExists(int idUser, int idKey);
    }
}
