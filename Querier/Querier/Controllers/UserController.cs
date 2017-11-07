﻿using System;
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

            return View();
        }
        [HttpPost, ActionName("Create")]
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
        [Authorize]
        public IActionResult DeleteQuery(int queryNumber, int userId)
        {
            var username = User.Identity.Name.ToString();
            var user = DataManager.UserOptions.GetUser(username);
            var query = DataManager.QueryOptions.Load(user, queryNumber);

            DataManager.UserOptions.DeleteQuery(user, queryNumber);

            user = DataManager.UserOptions.GetUser(username);

            return View("LoadUser", user);
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
