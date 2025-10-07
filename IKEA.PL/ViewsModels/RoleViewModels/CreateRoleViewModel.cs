

using System.ComponentModel.DataAnnotations;

namespace IKEA.PL.ViewsModels.RoleViewModels
{
    public class CreateRoleViewModel
    {
        [Required(ErrorMessage = "Role Name is required")]
        public string Name { get; set; }
    }
}
