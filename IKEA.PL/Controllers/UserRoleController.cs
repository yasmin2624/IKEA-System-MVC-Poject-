using IKEA.DAL.Models.Shared;
using IKEA.PL.ViewsModels.UserRoleViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IKEA.PL.Controllers
{
    public class UserRoleController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserRoleController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        #region AssignRole (GET)
        public IActionResult AssignRole()
        {
            ViewBag.Users = _userManager.Users.ToList();
            ViewBag.Roles = _roleManager.Roles.ToList();
            return View();
        }
        #endregion

        #region AssignRole (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AssignRole(string userId, string roleId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var role = await _roleManager.FindByIdAsync(roleId);

            if (user == null || role == null)
            {
                TempData["Error"] = "Invalid user or role!";
                return RedirectToAction(nameof(AssignRole));
            }

            var result = await _userManager.AddToRoleAsync(user, role.Name);

            if (result.Succeeded)
                TempData["Success"] = $"Role '{role.Name}' assigned to user '{user.UserName}' successfully!";
            else
                TempData["Error"] = string.Join(", ", result.Errors.Select(e => e.Description));

            return RedirectToAction(nameof(AssignRole));
        }
        #endregion

        #region Index 
        public IActionResult Index()
        {
            var users = _userManager.Users.ToList();
            var roles = _roleManager.Roles.ToList();

            var model = users.Select(u => new UserRoleViewModel
            {
                UserId = u.Id,
                UserName = u.UserName,
                Roles = roles.Select(r => new RoleCheckBox
                {
                    RoleName = r.Name,
                    IsSelected = _userManager.IsInRoleAsync(u, r.Name).Result
                }).ToList()
            }).ToList();

            return View(model);
        }
        #endregion

        #region UpdateUserRoles (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateUserRoles(string userId, List<string> selectedRoles)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return NotFound();

            var currentRoles = await _userManager.GetRolesAsync(user);
            await _userManager.RemoveFromRolesAsync(user, currentRoles);

            if (selectedRoles != null && selectedRoles.Any())
                await _userManager.AddToRolesAsync(user, selectedRoles);

            TempData["Success"] = $"Roles for {user.UserName} have been updated.";
            return RedirectToAction(nameof(Index));
        }
        #endregion
    }
}
