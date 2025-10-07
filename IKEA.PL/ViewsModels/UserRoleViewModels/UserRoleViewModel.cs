using System.Collections.Generic;


namespace IKEA.PL.ViewsModels.UserRoleViewModels
{
    public class UserRoleViewModel
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public List<RoleCheckBox> Roles { get; set; } = new List<RoleCheckBox>();
    }
}
