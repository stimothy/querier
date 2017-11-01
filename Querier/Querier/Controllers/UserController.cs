using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Querier.Models;
using Microsoft.AspNetCore.Identity;
using DataManager;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Principal;
using System.Security.Permissions;
using System.Dynamic;


// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Querier.Controllers
{

    public class UserController : Controller
    {
            

        //IList<ManageUserModel> QueryList = DataManager.UserData.GetQueries(userID);
        // GET: /<controller>/
        public IActionResult Index()
        {
            object cookie = HttpContext.Request.Cookies[".AspNetCore.Identity.Application"];
            string loginID = User.Identity.ToString();
            User user = DataManager.UserOptions.GetUser(loginID);
            dynamic model = new ExpandoObject();
            model.User = DataManager.UserOptions.GetUser(loginID);
            
            return View(user);
        }
        //[HttpGet]
        //public IActionResult Edit(int queryID)
        //{
        //    //finds query you want to edit
        //    var quiz = QueryList.Where(s => s.QueryID == queryID).FirstOrDefault();
        //    return View(quiz);
        //}
        //[HttpPost]
        //public IActionResult Edit(ManageUserModel quiz)
        //{
        //    //code to update the query name
        //    //Should be able to edit query name and show list of questions with options to edit
        //    var updateQuery = QueryList.FirstOrDefault(x => x.QueryID == quiz.QueryID);
        //    if (updateQuery != null)
        //    {
        //        updateQuery.QueryName = quiz.QueryName;
        //    }
        //    return RedirectToAction("Index");
        //}

        //[HttpPost]
        //public IActionResult Create()
        //{
            
        //    return RedirectToAction("Index");
        //}

    }
}
