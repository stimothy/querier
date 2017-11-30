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

            try
            {
                var nextQuestionNumber = query.Questions.First(x => x.Number > questionNumber).Number;
                var nextQuestion = QuestionOptions.Load(query, nextQuestionNumber);
                return View("LoadActiveQuestion", nextQuestion);
            }
            catch
            {
                return RedirectToAction(nameof(UserController.Index), "User");
            }
        }

        [Authorize]
        public IActionResult DisplayResults(int queryNumber, int questionNumber, List<string> resultList)
        {
            var username = User.Identity.Name.ToString();
            var user = UserOptions.GetUser(username);
            var query = QueryOptions.Load(user, 0);

            var question = query.Questions[0];

            resultList = new List<string>();
            resultList.Add(question.Number.ToString());

            foreach (var item in question.Answers)
            {
                resultList.Add(item.ToString());
            }

            return View("ResultsPage", resultList);
        }
        public IActionResult QueryStart()
        {
            return View("QueryStartView");
        }
    }
}