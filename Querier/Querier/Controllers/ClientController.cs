using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DataManager;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Querier.Controllers
{
    public class ClientController : Controller
    {
        // GET: /<controller>/
        [Authorize]
        public IActionResult ClientView()
        {
            var username = User.Identity.Name.ToString();
            var user = UserOptions.GetUser(username);
            var test = user.Queries[0];
            var queryNumber = test.Number;
            Query query = QueryOptions.Load(user, queryNumber);
            var t = query.Questions[0];
            var questionNumber = t.Number;
            Question question = QuestionOptions.Load(query, questionNumber);
            return View("ClientView", question);
        }
    }
}
