using Microsoft.AspNetCore.Mvc.Rendering;

namespace Trustme.ViewModels
{
    public class EditUserRolesModel
    {
        public EditUserModel User { get; set; } = new EditUserModel();
        public SelectList Roles { get; set; }
    }
}
