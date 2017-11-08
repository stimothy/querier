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
    }
}