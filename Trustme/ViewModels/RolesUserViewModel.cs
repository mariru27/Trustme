using Microsoft.AspNetCore.Mvc.Rendering;
using Trustme.Models;

namespace Trustme.ViewModels
{
    public class RolesUserViewModel
    {
        public User User { get; set; } = new User();
        public SelectList Roles { get; set; }
    }
}
