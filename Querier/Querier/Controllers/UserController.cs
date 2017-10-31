using System;
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


// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Querier.Controllers
{

    public class QueryController : Controller
    {
        //IList<ManageQueryModel> QueryList = new List<ManageQueryModel>
        //{
        //        new ManageQueryModel() {QueryName = "test1", QueryID = 1 },
        //        new ManageQueryModel() {QueryName = "test2", QueryID = 2}
        //};


        string Login = "login";

        string userID = UserData.GetUserID(Login);

        IList<ManageQueryModel> QueryList = DataManager.UserData.GetQueries(userID);
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Edit(int queryID)
        {
            //finds query you want to edit
            var quiz = QueryList.Where(s => s.QueryID == queryID).FirstOrDefault();
            return View(quiz);
        }
        [HttpPost]
        public IActionResult Edit(ManageQueryModel quiz)
        {
            //code to update the query name
            //Should be able to edit query name and show list of questions with options to edit
            var updateQuery = QueryList.FirstOrDefault(x => x.QueryID == quiz.QueryID);
            if (updateQuery != null)
            {
                updateQuery.QueryName = quiz.QueryName;
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Create()
        {
            
            return RedirectToAction("Index");
        }

    }
}
