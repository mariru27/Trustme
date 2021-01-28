﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trustme.Models;

namespace Trustme.IServices
{
    interface IUserRepository
    {
        public void add();
        public void delete();
        public void update();
        public IEnumerable<User> listAllUsers();
    }
}
