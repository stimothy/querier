using DataManager;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Querier.Controllers
{
    public class QuestionController : Controller
    {
        [Authorize]
        public IActionResult ManageQuestion()
        {
            var username = User.Identity.Name.ToString();
            var user = UserOptions.GetUser(username);
            Query query = QueryOptions.Load(user, 1);
            Question question = QuestionOptions.Load(query, 1);
            return View("LoadQuestion", question);
        }

        [Authorize][HttpGet]
        public IActionResult LoadQuestion(int queryNumber, int questionNumber)
        {
            var username = User.Identity.Name.ToString();
            var user = UserOptions.GetUser(username);
            Query query = QueryOptions.Load(user, queryNumber);
            Question question = QuestionOptions.Load(query, questionNumber);
            return View("LoadQuestion", question);
        }

        [Authorize][HttpPost]
        public IActionResult SaveQuestion(Question question)
        {
            QuestionOptions.Save(question);

            return this.RedirectToAction("ManageQuery", "Query", new { queryID = question.QueryNumber });
        }

        [Authorize]
        public IActionResult DeleteAnswer(int queryNumber, int questionNumber, int answerNumber)
        {
            var username = User.Identity.Name.ToString();
            var user = UserOptions.GetUser(username);
            var query = QueryOptions.Load(user, queryNumber);
            var question = QuestionOptions.Load(query, questionNumber);

            QuestionOptions.DeleteAnswer(question, answerNumber);

            return View("LoadQuestion", question);
        }

        [Authorize]
        public IActionResult InsertAnswer(int queryNumber, int questionNumber)
        {
            var username = User.Identity.Name.ToString();
            var user = UserOptions.GetUser(username);
            var query = QueryOptions.Load(user, queryNumber);
            var question = QuestionOptions.Load(query, questionNumber);

            QuestionOptions.AddAnswer(question);

            //question.Answers.Max(x => x.Number);

            //return View("LoadQuestion", question);
            return this.RedirectToAction("LoadAnswer", "Answer", new { queryNumber = question.QueryNumber, questionNumber = question.Number, answerNumber = question.Answers.Max(x => x.Number) });
        }
    }
}