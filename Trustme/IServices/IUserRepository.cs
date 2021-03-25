using System.Collections.Generic;
using Trustme.Models;

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
        public void EditUser(User _User);

        public User GetUserById(int idUser);
        public bool AnyUser();


    }
}
