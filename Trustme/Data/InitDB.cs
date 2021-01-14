using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Trustme.Models;

namespace Trustme.Data
{

    public class InitDB
    {
        public static void InitDb(AppContext context)
        {
            context.Database.EnsureCreated();
            if (context.Key.Any() && context.User.Any() && context.Role.Any())
            {
                return;
            }
            if (!context.Role.Any())
            {
                var roles = new Role[]
                {
                    new Role { RoleName = "Admin"},
                    new Role { RoleName = "User"},
                    new Role { RoleName = "Pro"},
                    new Role { RoleName = "Free"},
                };
                foreach(var r in roles)
                {
                    context.Role.Add(r);
                }
                context.SaveChanges();
            }



            var users = new User[]
            {
                new User{UserId = 1, FirstName = "Mihai", SecondName="Popescu", Mail="mihaipopescu@gmail.com", Username="mihai12345", Password = "password" },
                new User{UserId = 2, FirstName = "Mihai", SecondName="Popescu", Mail="mihaipopescu@gmail.com", Username="mariru", Password = "marina" }
            };

            foreach(User u in users)
            {
                context.User.Add(u);
            }

            var keys = new Key[]
            {
                new Key{UserId = 1, PublicKey = " "}
            };

            foreach(Key k in keys)
            {
                context.Key.Add(k);
            }
            context.SaveChanges();
        }
    }
}
