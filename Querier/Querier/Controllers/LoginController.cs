using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Querier.Models.Login;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Querier.Controllers
{
    public class LoginController : Controller
    {
        private UserManager<User> userManager;
        private SignInManager<User> signInManager;

        public LoginController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserRegistration reg)
        {
            if (ModelState.IsValid)
            {
                User user = new User { UserName = reg.username };
                IdentityResult result = await userManager.CreateAsync(user, reg.password);

                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(user, false);
                    return RedirectToAction("Index", "Home");
                }
                
                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserLogin login)
        {
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(login.username, login.password, false, false);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                }
            }

            return View();
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }
    }
}
