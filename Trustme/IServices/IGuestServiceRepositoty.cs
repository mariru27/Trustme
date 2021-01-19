using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trustme.Models;

namespace Trustme.IServices
{
    interface IGuestServiceRepositoty
    {
        //register and login
        void LogIn(User user);
        void Register(User user);
    }
}
