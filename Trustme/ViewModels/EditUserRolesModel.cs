using Microsoft.AspNetCore.Mvc.Rendering;

namespace Trustme.ViewModels
{
    public class EditUserRolesModel
    {
        public EditUserModel User { get; set; }
        public SelectList Roles { get; set; }
    }
}
