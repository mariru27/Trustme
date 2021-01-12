﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Trustme.Models
{
    public class Role
    {
        [Key]
        public string RoleName { get; set; }
        public string IdUser { get; set; }
        public List<User> Users { get; set; } = new List<User>();
    }
}