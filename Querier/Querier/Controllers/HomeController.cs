using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Querier.Models;
using Microsoft.AspNetCore.Authorization;
using DataManager;

namespace Querier.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        [Authorize]
        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }
                
        public IActionResult GuestAccess()
        {
            User user = UserOptions.GetUser("TEST@TEST.COM");
            return View(user);
        }

        public IActionResult ManageQuery()
        {
            User user = UserOptions.GetUser("TEST@TEST.COM");
            Query query = QueryOptions.Load(user, 1);
            return View(query);
        }

        public IActionResult ManageQuestion()
        {
            User user = UserOptions.GetUser("TEST@TEST.COM");
            Query query = QueryOptions.Load(user, 1);
            Question question = QuestionOptions.Load(query, 1);
            return View(question);
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
