using Microsoft.AspNetCore.Http;
using System.Linq;
using Trustme.Controllers;
using Trustme.IServices;
using Trustme.Models;

namespace Trustme.Data
{

    public class InitDB
    {
        private readonly IHttpRequestFunctions _HttpRequestFunctions;
        private AuthenticateController _AuthenticateController;
        public InitDB(IHttpRequestFunctions httpRequestFunctions, AuthenticateController authenticateController)
        {
            _HttpRequestFunctions = httpRequestFunctions;
            _AuthenticateController = authenticateController;
        }

        public void DeleteCookie(AppContext context)
        {
            if (context.User.Any() == false)
            {
                _AuthenticateController.LogoutAsync();

            }
        }
        public static void InitDb(AppContext context)
        {

            
            context.Database.EnsureCreated();


            if (context.Key.Any() && context.User.Any() && context.Role.Any())
            {
                return;
            }
            if (context.Role.Any() == false)
            {

                var roles = new Role[]
                {
                    new Role { RoleName = "Admin"},
                    new Role { RoleName = "User"},
                    new Role { RoleName = "Pro"},
                    new Role { RoleName = "Free"},
                };
                foreach (var r in roles)
                {
                    context.Role.Add(r);
                }
                context.SaveChanges();
            }

            //var users = new User[]
            //{
            //    new User{UserKeyId = 1, FirstName = "Mihai", SecondName="Popescu", Mail="mihaipopescu@gmail.com", Username="mihai12345", Password = "password", ConfirmPassword = "password", UserRole = roles[0] },
            //    new User{UserKeyId = 2, FirstName = "Mihai", SecondName="Popescu", Mail="mihaipopescu@gmail.com", Username="mariru", Password = "marina", ConfirmPassword = "passwor", UserRole = roles[0]  }
            //};

            //users[0].UserRole = roles[0];
            //users[1].UserRole = roles[0];
            //roles[0].Users.Add(users[0]);
            //roles[0].Users.Add(users[1]);

            //var keys = new Key[] 
            //{
            //     new Key{ KeyId = 1, UserKeyId = 1, PublicKey = " ", CertificateName = "testeCertificate1", Description = "test description", KeySize = 1024, User = users[0]},     
            //     new Key{ KeyId = 2, UserKeyId = 2,PublicKey = " ", CertificateName = "testeCertificate2", Description = "test description3", KeySize = 1024, User = users[1]},     
            //};

            //foreach (User u in users)
            //{
            //    context.User.Add(u);
            //}
            //context.SaveChanges();

            //foreach (Key k in keys)
            //{
            //    context.Key.Add(k);
            //}

            // context.SaveChanges();
        }
    }
}
