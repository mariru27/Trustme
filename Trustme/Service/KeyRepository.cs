﻿using System;
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

        public IEnumerable<Key> listAllKeys(User _User)
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
        public void update(UserKeyModel _UserKeyModel)
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
    }
}
