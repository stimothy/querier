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
        
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }
        
        [HttpPost]
        public IActionResult JoinQuery(string qID)
        {
            return RedirectToAction("JoinQuery", "Client", new { code = qID });
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
