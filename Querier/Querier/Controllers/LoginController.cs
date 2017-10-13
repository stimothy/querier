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

        //For when the user requests the html for the register view.
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        //For when the user submits his request on the register view.
        [HttpPost]
        public async Task<IActionResult> Register(UserRegistration reg)
        {
            if (ModelState.IsValid)
            {
                User user = new User { UserName = reg.username };

                //Create an acount on the database.
                IdentityResult result = await userManager.CreateAsync(user, reg.password);

                //If it succeeds, log them in and redirect to home page.
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

        //For when the user requests the html for the Login view.
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        //For when the user submits his request on the Login view.
        [HttpPost]
        public async Task<IActionResult> Login(UserLogin login)
        {
            if (ModelState.IsValid)
            {
                //Try to log the user in if successful redirect to home page.
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
