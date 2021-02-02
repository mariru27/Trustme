using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trustme.Models;
using Trustme.IServices;
using Trustme.Data;
using Trustme.ViewModels;

namespace Trustme.Service
{
    public class KeyRepository : IKeyRepository
    {
        private UserKeyContext _context;
        KeyRepository(UserKeyContext context)
        {
            _context = context;
        }
        public void addKey(UserKeyModel _UserKeyModel)
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

        public void delete(UserKeyModel _UserKeyModel)
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
