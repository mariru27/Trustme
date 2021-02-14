using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trustme.Models;
using Trustme.IServices;
using Trustme.Data;
using Trustme.ViewModels;
using System.Net.Http;
using Microsoft.Extensions.Caching.Memory;

namespace Trustme.Service
{
    public class KeyRepository : IKeyRepository
    {
        private AppContext _context;

        public KeyRepository(AppContext context)
        {
            _context = context;
        }
        public void AddKey(UserKeyModel _UserKeyModel)
        {
            //find user
            User user = _context.User.Where(a => a.Username == _UserKeyModel.User.Username)?.SingleOrDefault();

            Key userKey = _context.Key.Where(a => a.UserKeyId == user.UserId && a.CertificateName == _UserKeyModel.Key.CertificateName)?.SingleOrDefault();

            if(userKey == null)
            {
                // add key 
                _context.Key.Add(_UserKeyModel.Key);

                // create UserKey model and populate with _UserKeyModel values
                UserKey _UserKey = new UserKey();
                _UserKey.Key = _UserKeyModel.Key;
                _UserKey.KeyId = _UserKeyModel.Key.KeyId;
                _UserKey.User = _UserKeyModel.User;
                _UserKey.UserId = _UserKeyModel.User.UserId;

                // add UserKey
                _context.UserKey.Add(_UserKey);
            
                //save
                _context.SaveChanges();
            }
        }

        public void DeleteKey(UserKeyModel _UserKeyModel)
        {
            //create UserKey model and populate with _UserKeyModel values
            UserKey _UserKey = new UserKey();
            _UserKey.Key = _UserKeyModel.Key;
            _UserKey.KeyId = _UserKeyModel.Key.KeyId;
            _UserKey.User = _UserKeyModel.User;
            _UserKey.UserId = _UserKeyModel.User.UserId;
            
            //remove Key
            _context.Key.Remove(_UserKeyModel.Key);
            //remove _UserKey
            _context.UserKey.Remove(_UserKey);

            //save
            _context.SaveChanges();
        }
        public UserKeyModel CreateDefaultUserKeyModel()
        {
            User user = new User();
            user.FirstName = "First name";
            user.SecondName = "Second name";
            user.Username = "testusername";
            user.Mail = "mail@gmail.com";
            user.Password = "password";
            user.ConfirmPassword = "password";

            Key key = new Key();
            key.CertificateName = "certificateName";
            key.Description = "description";
            key.KeySize = 100;
            key.PublicKey = "wdwq";

            UserKeyModel userKeyModel = new UserKeyModel();
            userKeyModel.Key = key;
            userKeyModel.User = user;

            return userKeyModel;
        }

        public IEnumerable<Key> ListAllKeys(User _User)
        {
            IEnumerable<Key> KeysList = _context.User.
                Join(_context.UserKey,
                user => user.UserId,
                userKey => userKey.UserId,
                (user, userKey) => new { user, userKey }
                ).Where(a => a.user.UserId == _User.UserId).Join(_context.Key,
                userKeyResult => userKeyResult.userKey.KeyId,
                key => key.KeyId,
                (userKeyResult, key) => new Key(key)
                ).ToList();
            return KeysList;
        }
        public void UpdateKey(UserKeyModel _UserKeyModel)
        {

            _context.User.Update(_UserKeyModel.User);
            _context.Key.Update(_UserKeyModel.Key);

            //save
            _context.SaveChanges();
        }

        public Key GetKeyById(int keyId)
        {
            return _context.Key.Where(a => a.KeyId == keyId)?.SingleOrDefault();
        }

        public bool KeyExists(int idUser, int idKey)
        {
            return _context.UserKey.Where(uk => uk.UserId == idUser && uk.KeyId == idKey).Join(_context.Key,
                user => user.IdUserKey,
                key => key.KeyId,
                (user, key) => new Key(key)).Any();
        }

        public Key GetKey(int userId, int keyId)
        {
            return _context.UserKey.Where(uk => uk.UserId == userId && uk.KeyId == keyId).Join(_context.Key,
                user => user.IdUserKey,
                key => key.KeyId,
                (user, key) => new Key(key)).SingleOrDefault();
        }
    }
}
