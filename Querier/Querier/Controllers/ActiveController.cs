using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using DataManager;
using System.Linq;
using System;

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

                QuestionOptions.SetFirstActive(query);

                return View("LoadActiveQuestion", question);
            }

            return RedirectToAction(nameof(UserController.Index), "User");
        }

        [Authorize]
        public IActionResult DeleteAnswer(int queryNumber, int questionNumber, int answerNumber)
        {
            var username = User.Identity.Name.ToString();
            var user = UserOptions.GetUser(username);
            var query = QueryOptions.Load(user, queryNumber);
            var question = QuestionOptions.Load(query, questionNumber);

            QuestionOptions.DeleteAnswer(question, answerNumber);
            
            return RedirectToAction("LoadActiveQuery", new { queryID = queryNumber });
        }

        [Authorize]
        public IActionResult LoadNextQuestion(int queryNumber, int questionNumber)
        {
            var username = User.Identity.Name.ToString();
            var user = UserOptions.GetUser(username);
            var query = QueryOptions.Load(user, queryNumber);
            var question = QuestionOptions.Load(query, questionNumber);

            int index = 0;

            for (int i = 0; i < query.Questions.Count; ++i)
            {
                if (query.Questions[i].Number == question.Number)
                {
                    index = i;
                }
            }

            try
            {
                QuestionOptions.SetNextActive(question, query.Questions[index + 1].Number);
            }
            catch (ArgumentOutOfRangeException ex) { }

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