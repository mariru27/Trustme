﻿using Microsoft.AspNetCore.Mvc.Rendering;

namespace Trustme.ViewModels
{
    public class RolesUserViewModel
    {
        public User User { get; set; }
        public SelectList Roles { get; set; }
    }
}
