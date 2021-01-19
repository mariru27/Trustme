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
        User LogIn(User user);
        User Register(User user);
    }
}
