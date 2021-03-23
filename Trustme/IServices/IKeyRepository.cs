using System.Collections.Generic;
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

        public Key GetKeyByCertificateName(string username, string name);

        public bool KeyExists(int idUser, int idKey);

        public UserKey GetUserKeyById(int idUserKey);

        public int GetNrCertificates(User _User);
        public bool CheckCertificateSameName(User user, string KeyName);

    }
}
