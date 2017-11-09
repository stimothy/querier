using DataManager;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Querier.Controllers
{
    public class AnswerController : Controller
    {
        [Authorize]
        public IActionResult ManageAnswer()
        {
            var username = User.Identity.Name.ToString();
            var user = UserOptions.GetUser(username);
            Query query = QueryOptions.Load(user, 1);
            Question question = QuestionOptions.Load(query, 1);
            Answer answer = AnswerOptions.Load(question, 1);

            return View("LoadAnswer", answer);
        }

        [Authorize][HttpGet]
        public IActionResult LoadAnswer(int queryNumber, int questionNumber, int answerNumber)
        {
            var username = User.Identity.Name.ToString();
            var user = UserOptions.GetUser(username);
            Query query = QueryOptions.Load(user, queryNumber);
            Question question = QuestionOptions.Load(query, questionNumber);
            Answer answer = AnswerOptions.Load(question, answerNumber);

            return View("LoadAnswer", answer);
        }

        [Authorize]
        [HttpPost]
        public IActionResult SaveAnswer(Answer answer)
        {
            AnswerOptions.Save(answer);

            return this.RedirectToAction("LoadQuestion", "Question", new { queryNumber = answer.QueryNumber, questionNumber = answer.QuestionNumber });
        }
    }
}