using DataManager;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Querier.Controllers
{
    public class QueryController : Controller
    {
        [Authorize]
        public IActionResult ManageQuery(int queryID)
        {
            var username = User.Identity.Name.ToString();
            var user = UserOptions.GetUser(username);
            var query = QueryOptions.Load(user, queryID);

            return View("LoadQuery", query);
        }

        [Authorize]
        public IActionResult LoadQuery(Query query)
        {
            return View(query);
        }

        [Authorize][HttpPost]
        public IActionResult SaveQuery(Query query)
        {
            QueryOptions.Save(query);

            return RedirectToAction("Index", "User");
        }

        [Authorize]
        public IActionResult DeleteQuestion(int queryID, int questionNumber)
        {
            var username = User.Identity.Name.ToString();
            var user = UserOptions.GetUser(username);
            var query = QueryOptions.Load(user, queryID);

            QueryOptions.DeleteQuestion(query, questionNumber);

            return View("LoadQuery", query);
        }

        [Authorize]
        public IActionResult InsertQuestion(int queryID)
        {
            var username = User.Identity.Name.ToString();
            var user = UserOptions.GetUser(username);
            var query = QueryOptions.Load(user, queryID);

            QueryOptions.AddQuestion(query);

            var questionNumber = query.Questions.Max(x => x.Number);
            var question = QuestionOptions.Load(query, questionNumber);

            return RedirectToAction("LoadQuestion", "Question", question);
        }
        
    }
}