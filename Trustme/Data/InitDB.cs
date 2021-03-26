using System.Linq;
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
            if (context.Role.Any() == false)
            {


                var roles = new Role[]
                {
                    new Role { RoleName = "Admin"},
                    new Role { RoleName = "Pro"},
                    new Role { RoleName = "Free"},
                };
                foreach (var r in roles)
                {
                    context.Role.Add(r);
                }
                context.SaveChanges();
            }

        }
    }
}
