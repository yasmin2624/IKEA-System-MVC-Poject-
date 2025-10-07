using IKEA.DAL.Models.Shared;
using IKEA.PL.ViewsModels.RoleViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IKEA.PL.Controllers
{
    public class RoleController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        


        public RoleController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            _roleManager = roleManager;
        }

        #region Index
        // GET: Role/Index
        public IActionResult Index()
        {
            var roles = _roleManager.Roles
                .Select(r => new RoleViewModel
                {
                    Id = r.Id,
                    Name = r.Name
                })
                .ToList();

            return View(roles);
        }
        #endregion

        #region Details
        // GET: Role/Details/{id}
        public IActionResult Details(string id)
        {
            if (string.IsNullOrEmpty(id))
                return NotFound();

            var role = _roleManager.Roles.FirstOrDefault(r => r.Id == id);

            if (role == null)
                return NotFound();

            var model = new RoleViewModel
            {
                Id = role.Id,
                Name = role.Name
            };

            return View(model);
        }
        #endregion


        #region Create
        // GET: Users/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (!await _roleManager.RoleExistsAsync(model.Name))
                {
                    var result = await _roleManager.CreateAsync(new IdentityRole(model.Name));
                    if (result.Succeeded)
                        return RedirectToAction(nameof(Index));

                    foreach (var error in result.Errors)
                        ModelState.AddModelError("", error.Description);
                }
                else
                {
                    ModelState.AddModelError("", "This Role already exists");
                }
            }

            return View(model);
        }


        #endregion

        #region Edit
        // GET: Role/Edit/{id}
        public IActionResult Edit(string id)
        {
            if (string.IsNullOrEmpty(id))
                return NotFound();

            var role = _roleManager.Roles.FirstOrDefault(r => r.Id == id);

            if (role == null)
                return NotFound();

            var model = new RoleViewModel
            {
                Id = role.Id,
                Name = role.Name
            };

            return View(model);
        }

        // POST: Role/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(RoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var role = _roleManager.Roles.FirstOrDefault(r => r.Id == model.Id);

                if (role == null)
                    return NotFound();

                role.Name = model.Name;
                var result = _roleManager.UpdateAsync(role).Result;

                if (result.Succeeded)
                    return RedirectToAction(nameof(Index));

                foreach (var error in result.Errors)
                    ModelState.AddModelError("", error.Description);
            }

            return View(model);
        }
        #endregion

        #region Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
                return NotFound();

            var role = _roleManager.Roles.FirstOrDefault(r => r.Id == id);

            if (role == null)
                return NotFound();

            var result = _roleManager.DeleteAsync(role).Result;

            if (result.Succeeded)
                return RedirectToAction(nameof(Index));

            foreach (var error in result.Errors)
                ModelState.AddModelError("", error.Description);

            return RedirectToAction(nameof(Index));
        }
        #endregion


    }
}
