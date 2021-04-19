using Microsoft.AspNetCore.Mvc.Rendering;
using Trustme.Models;

namespace Trustme.ViewModels
{
    public class RolesUserViewModel
    {
        public User User = new User();
        public SelectList Roles { get; set; }
    }
}
