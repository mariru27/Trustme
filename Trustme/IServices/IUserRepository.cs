using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trustme.Models;
using Trustme.ViewModels;

namespace Trustme.IServices
{
    public interface IUserRepository
    {
        public void AddUser(User _User);
        public void DeleteUser(User _User);
        public void UpdateUser(User _User);
        public IEnumerable<User> ListAllUsers();
        public IEnumerable<User> ListAllUsers(Role _Role);

        public User GetUserbyUsername(string username);
        public User GetUserbyMail(string mail);

        public User GetUserById(int idUser);

    }
}
