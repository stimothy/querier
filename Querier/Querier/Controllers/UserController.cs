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
using Microsoft.AspNetCore.Authorization;
using System.Net;


// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Querier.Controllers
{

    public class UserController : Controller
    {
        public object FormsAuthentication { get; private set; }


        //IList<ManageUserModel> QueryList = DataManager.UserData.GetQueries(userID);
        // GET: /<controller>/
        [Authorize]
        public IActionResult Index()
        {
            string loginID = User.Identity.Name.ToString();
            User user = DataManager.UserOptions.GetUser(loginID);

            return View(user);
        }
        public IActionResult Create()
        {
            //string loginID = User.Identity.Name.ToString();
            //User user = DataManager.UserOptions.GetUser(loginID);
            //User newquery = DataManager.UserOptions.AddQuery(user);

            return View();
        }
        [HttpPost]
        public IActionResult Create(Query query)
        {
            string loginID = User.Identity.Name.ToString();
            User user = DataManager.UserOptions.GetUser(loginID);
            User newquery = DataManager.UserOptions.AddQuery(user);
            if (ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View(query);
            }
        }
        public IActionResult Delete(int id)
        {
            string loginID = User.Identity.Name.ToString();
            User user = DataManager.UserOptions.GetUser(loginID);
            Query query = DataManager.QueryData.Get(user.UserID, id);
            return View(query);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            string loginID = User.Identity.Name.ToString();
            User user = DataManager.UserOptions.GetUser(loginID);
            Query query = DataManager.QueryData.Get(user.UserID, id);
            DataManager.UserOptions.DeleteQuery(user, id);
            return RedirectToAction("Index");
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
