using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using DataManager;
using System.Linq;
using System.Collections.Generic;

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
            query = QueryOptions.Load(user, queryID); // reload to get code

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

        [Authorize]
        public IActionResult DisplayResults(int queryNumber, int questionNumber)
        {
            var username = User.Identity.Name.ToString();
            var user = UserOptions.GetUser(username);
            var query = QueryOptions.Load(user, 3);
            DataManager.Question question = new Question();

            foreach (DataManager.Question item in query.Questions)
            {
                if(item != null)
                {
                    question = item;
                }
            }
            if(question == null)
            {
                return RedirectToAction(nameof(UserController.Index), "User");
            }

            return View("DisplayResults", question);
        }
        public IActionResult QueryStart()
        {
            return View("QueryStartView");
        }

    }
}