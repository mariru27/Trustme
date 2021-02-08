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

        public void DeleteKey(UserKeyModel _UserKeyModel)
        {
            //create UserKey model and populate with _UserKeyModel values
            UserKey _UserKey = new UserKey();
            _UserKey.Key = _UserKeyModel.Key;
            _UserKey.KeyId = _UserKeyModel.Key.KeyId;
            _UserKey.User = _UserKeyModel.User;
            _UserKey.UserId = _UserKeyModel.User.UserId;
            
            //remove _UserKey
            _context.UserKey.Remove(_UserKey);
            //remove Key
            _context.Key.Remove(_UserKey.Key);

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
            List<Key> KeysList = (List<Key>)_context.User.
                Join(_context.UserKey,
                user => user.UserId,
                userKey => userKey.UserId,
                (user, userKey) => new { user, userKey }
                ).Join(_context.Key,
                userKeyResult => userKeyResult.userKey.KeyId,
                key => key.KeyId,
                (userKeyResult, key) => new { key }
                );

            return KeysList;
        }
        public void UpdateKey(UserKeyModel _UserKeyModel)
        {
            //create UserKey model and populate with _UserKeyModel values
            UserKey _UserKey = new UserKey();
            _UserKey.Key = _UserKeyModel.Key;
            _UserKey.KeyId = _UserKeyModel.Key.KeyId;
            _UserKey.User = _UserKeyModel.User;
            _UserKey.UserId = _UserKeyModel.User.UserId;

            _context.UserKey.Update(_UserKey);
            _context.User.Update(_UserKeyModel.User);
            _context.Key.Update(_UserKeyModel.Key);

            //save
            _context.SaveChanges();
        }

        public Key GetKeyById(int keyId)
        {
            return _context.Key.Where(a => a.KeyId == keyId)?.SingleOrDefault();
        }
    }
}
