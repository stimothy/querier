using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using DataManager;
using System.Linq;

namespace Querier.Controllers
{
    public class ActiveController : Controller
    {
        [Authorize]
        public IActionResult LoadQueryStartPage(int queryID)
        {
            var username = User.Identity.Name.ToString();
            var user = UserOptions.GetUser(username);
            var query = QueryOptions.Load(user, queryID);

            QueryOptions.Open(query);
            
            return View("QueryStartView", query);
        }

        [Authorize]
        public IActionResult LoadActiveQuery(int queryID)
        {
            var username = User.Identity.Name.ToString();
            var user = UserOptions.GetUser(username);
            var query = QueryOptions.Load(user, queryID);

            if (query.Questions.Count > 0)
            {
                var question = QuestionOptions.Load(query, query.Questions.First().Number);

                return View("LoadActiveQuestion", question);
            }

            return RedirectToAction(nameof(UserController.Index), "User");
        }

        [Authorize]
        public IActionResult LoadNextQuestion(int queryNumber, int questionNumber)
        {
            var username = User.Identity.Name.ToString();
            var user = UserOptions.GetUser(username);
            var query = QueryOptions.Load(user, queryNumber);
            var question = QuestionOptions.Load(query, questionNumber);

            QuestionOptions.SetNextActive(question);

            try
            {
                var nextQuestionNumber = query.Questions.First(x => x.Number > questionNumber).Number;
                var nextQuestion = QuestionOptions.Load(query, nextQuestionNumber);
                return View("LoadActiveQuestion", nextQuestion);
            }
            catch
            {
                QueryOptions.Close(query);
                return RedirectToAction(nameof(UserController.Index), "User");
            }
        }
    }
}