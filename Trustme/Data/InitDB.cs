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
            if(context.Key.Any())
            {
                return;
            }

            var users = new User[]
            {
                new User{UserId = 1, firstName = "Mihai", secondName="Popescu", mail="mihaipopescu@gmail.com", username="mihai12345", password = "password" }
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
