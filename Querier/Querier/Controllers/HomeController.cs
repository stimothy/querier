using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Querier.Models;
using Microsoft.AspNetCore.Authorization;

/*HomeController is the default controler that the program goes to.
 Every controller should have an accompanying views folder with the same
 name as the *Controller that you named this file, except it should only be
 called * without the Controller part.*/
namespace Querier.Controllers
{
    public class HomeController : Controller
    {
        /*Index is the default function that is called unless otherwise specified*/
        //[Authorize]           I couldn't get this to work, so I did an if statement instead.
        public IActionResult Index()
        {
            //Check to see if the user is logged in.
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Login");
            }
            //View will call the cshtml file that is found under Views/Home folder, index is the default file it will call.
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
