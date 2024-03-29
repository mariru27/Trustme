﻿using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Trustme.Data;
using Trustme.IServices;
using Trustme.Models;
using Trustme.ViewModels;

namespace Trustme.Service
{
    public class KeyRepository : IKeyRepository
    {
        private TMDbContext _context;

        public KeyRepository(TMDbContext context)
        {
            _context = context;
        }
        public void AddKey(UserKeyModel _UserKeyModel)
        {
            //find user
            User user = _context.User.Where(a => a.Username == _UserKeyModel.User.Username)?.SingleOrDefault();

            Key userKey = _context.Key.Where(a => a.KeyId == user.UserId && a.CertificateName == _UserKeyModel.Key.CertificateName)?.SingleOrDefault();

            if (userKey == null)
            {
                // add key 
                _context.Key.Add(_UserKeyModel.Key);

                // create UserKey model and populate with _UserKeyModel values
                UserKey _UserKey = new UserKey();
                //_UserKey.Key = _UserKeyModel.Key;
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
            //create UserKey model
            //UserKey _UserKey = new UserKey();
            //_UserKey = this.GetUserKeyById(_UserKeyModel.Key.KeyId);

            //_context.UserKey.Remove(_UserKey);
            try
            {
                _context.SignedDocuments.RemoveRange(_context.SignedDocuments.Where(x => x.KeyId == _UserKeyModel.Key.KeyId));
                _context.UnsignedDocuments.RemoveRange(_context.UnsignedDocuments.Where(x => x.KeyId == _UserKeyModel.Key.KeyId));
                _context.Key.Remove(GetKeyById(_UserKeyModel.Key.KeyId));
                _context.SaveChanges();
            }
            catch (System.Exception e)
            {

            }
        }


        public List<Key> ListAllKeys(User _User)
        {
            List<Key> KeysList = _context.User.
                Join(_context.UserKey,
                user => user.UserId,
                userKey => userKey.UserId,
                (user, userKey) => new { user, userKey }
                ).Where(a => a.user.UserId == _User.UserId).Join(_context.Key,
                userKeyResult => userKeyResult.userKey.IdUserKey,
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
            return _context.UserKey.Where(uk => uk.UserId == idUser && uk.IdUserKey == idKey).Join(_context.Key,
                user => user.IdUserKey,
                key => key.KeyId,
                (user, key) => new Key(key)).Any();
        }

        public bool CertitifateNameExist(int idUser, string name)
        {
            var res = _context.UserKey.AsNoTracking().Where(uk => uk.UserId == idUser).Join(_context.Key,
                user => user.IdUserKey,
                key => key.KeyId,
                (user, key) => new Key(key)).ToList().Where(a => a.CertificateName == name).Any();
            return res;
        }


        public Key GetKey(int userId, int keyId)
        {
            Key key = _context.UserKey.AsNoTracking().Where(uk => uk.UserId == userId && uk.IdUserKey == keyId).Join(_context.Key,
                user => user.IdUserKey,
                key => key.KeyId,
                (user, key) => new Key(key)).SingleOrDefault();
            return key;


        }

        public Key GetKeyByCertificateName(string username, string name)
        {
            User user = _context.User.Where(u => u.Username == username).SingleOrDefault();

            if (user != null)
            {
                Key key = _context.UserKey.Where(uk => uk.UserId == user.UserId).Join(
                    _context.Key.Where(k => k.CertificateName == name),
                    uk => uk.IdUserKey,
                    k => k.KeyId,
                    (uk, k) => new Key(k)).SingleOrDefault();
                return key;
            }
            return null;
        }

        public UserKey GetUserKeyById(int idUserKey)
        {
            return _context.UserKey.Where(uk => uk.IdUserKey == idUserKey).SingleOrDefault();
        }

        public int GetNrCertificates(User _User)
        {
            IEnumerable<Key> KeysList = _context.User.
               Join(_context.UserKey,
               user => user.UserId,
               userKey => userKey.UserId,
               (user, userKey) => new { user, userKey }
               ).Where(a => a.user.UserId == _User.UserId).Join(_context.Key,
               userKeyResult => userKeyResult.userKey.IdUserKey,
               key => key.KeyId,
               (userKeyResult, key) => new Key(key)
               ).ToList();
            return KeysList.Count();
        }

        public bool CheckCertificateSameName(User user, string KeyName)
        {
            IEnumerable<Key> KeysList = _context.User.
               Join(_context.UserKey,
               user => user.UserId,
               userKey => userKey.UserId,
               (user, userKey) => new { user, userKey }
               ).Where(a => a.user.UserId == user.UserId).Join(_context.Key,
               userKeyResult => userKeyResult.userKey.IdUserKey,
               key => key.KeyId,
               (userKeyResult, key) => new Key(key)
               ).ToList().Where(k => k.CertificateName == KeyName);
            return KeysList.Any();
        }


    }
}
