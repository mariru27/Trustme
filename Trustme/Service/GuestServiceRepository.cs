using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trustme.IServices;
using Trustme.Models;
using Trustme.Data;

namespace Trustme.Service
{
    public class GuestServiceRepository : IGuestServiceRepositoty
    {
        UserRoleContext _userRoleContext;
        public GuestServiceRepository(UserRoleContext userRoleContext)
        {
            _userRoleContext = userRoleContext;
        }
        public void LogIn(User user)
        {
            throw new NotImplementedException();
        }

        public void Register(User user)
        {
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
            user.ConfirmPassword = BCrypt.Net.BCrypt.HashPassword(user.ConfirmPassword);
            _userRoleContext.User.Add(user);
            _userRoleContext.SaveChanges();
        }
    }
}
