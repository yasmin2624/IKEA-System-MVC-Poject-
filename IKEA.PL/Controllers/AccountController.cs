using IKEA.DAL.Models.Shared;
using IKEA.PL.Helper;
using IKEA.PL.ViewsModels.AccountViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IKEA.PL.Controllers
{
    public class AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager) : Controller
    {
        private readonly UserManager<ApplicationUser> userManager = userManager;
        private readonly SignInManager<ApplicationUser> signInManager = signInManager;
        #region Register
        [HttpGet]
        public IActionResult Register() => View();

        [HttpPost]
        public IActionResult Register(RegisterViewModel registerView)
        {
            if (!ModelState.IsValid) return View(registerView);
            var User = new ApplicationUser()
            {
                FirstlName = registerView.FirstName,
                LastName = registerView.LastName,
                UserName = registerView.UserName,
                Email = registerView.Email,

            };
            var Result = userManager.CreateAsync(User, registerView.Password).Result;
            if (Result.Succeeded)
            {
                return RedirectToAction("Login");
            }
            else
            {
                foreach (var error in Result.Errors)
                {
                    ModelState.AddModelError(String.Empty, error.Description);
                }
                return View(registerView);
            }

        }

        #endregion
        #region Login
        [HttpGet]
        public IActionResult Login() => View();
        [HttpPost]
        public IActionResult Login(LoginViewModel loginView)
        {
            if (!ModelState.IsValid) return View(loginView);
            var user = userManager.FindByEmailAsync(loginView.Email).Result;
            if (user is not null)
            {
                var result = signInManager.PasswordSignInAsync(user, loginView.Password, loginView.RememberMe, false).Result;
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(HomeController.Index), "Home");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid Login Attempt:(");

                }

            }
            return View(loginView);
        }
        #endregion
        #region SignOut
        [HttpGet]
        public IActionResult SignOut()
        {
            signInManager.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }
        #endregion
        #region ForgetPassword
        [HttpGet]
        public IActionResult ForgetPassword() => View();


        #endregion
        #region SendResetPasswordLink
        [HttpPost]
        public IActionResult SendResetPasswordLink(ForgetPasswordViewModel forgetPassword)
        {
            if (ModelState.IsValid)
            {
                var User = userManager.FindByEmailAsync(forgetPassword.Email).Result;
                if (User is not null)
                {
                    //Send Email 
                    var Token = userManager.GeneratePasswordResetTokenAsync(User).Result;
                    var ResetPasswordLink = Url.Action("ResetPassword", "Account", new { email = forgetPassword.Email, Token }, Request.Scheme);
                    var email = new Email()
                    {
                        To = forgetPassword.Email,
                        Subject = "Here You Can Reset Your Password ",
                        Body = ResetPasswordLink
                    };
                    EmailSettings.SendEmail(email);
                    return RedirectToAction("CheckYourInbox");


                }
            }
            ModelState.AddModelError(string.Empty, "Invalid Operation");
            return View(nameof(ForgetPassword), forgetPassword);
        }

        #endregion

        #region Check Your Inbox
        [HttpGet]
        public IActionResult CheckYourInbox() => View();

        #endregion

        #region ReserPassword 
        [HttpGet]
        public IActionResult ResetPassword(string email, string Token)
        {
            TempData["email"] = email;
            TempData["Token"] = Token;
            return View();

        }
        [HttpPost]
        public IActionResult ResetPassword(ResetPasswordViewModel resetPassword)
        {
            if (!ModelState.IsValid) return View(resetPassword);
            string email = TempData["email"] as string ?? string.Empty;
            string Token = TempData["Token"] as string ?? string.Empty;
            var User = userManager.FindByEmailAsync(email).Result;
            if (User is not null)
            {
                var result = userManager.ResetPasswordAsync(User, Token, resetPassword.Password).Result;
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(Login));
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
               
            }

            return View(resetPassword);
        }
            #endregion

        
    }
}
