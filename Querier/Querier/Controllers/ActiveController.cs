using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using DataManager;
using System.Linq;
using System.Collections.Generic;
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
            query.Questions = query.Questions.OrderBy(q => q.Order).ToList();

            QueryOptions.Open(query);
            query = QueryOptions.Load(user, queryID); // reload to get code
            query.Questions = query.Questions.OrderBy(q => q.Order).ToList();

            QueryOptions.ResetScores(query);

            return View("QueryStartView", query);
        }

        [Authorize]
        public IActionResult LoadActiveQuery(int queryID)
        {
            var username = User.Identity.Name.ToString();
            var user = UserOptions.GetUser(username);
            var query = QueryOptions.Load(user, queryID);
            query.Questions = query.Questions.OrderBy(q => q.Order).ToList();

            if (query.Questions.Count > 0)
            {
                var question = QuestionOptions.Load(query, query.Questions.First().Number);
                question.Answers = question.Answers.OrderBy(a => a.Order).ToList();

                QuestionOptions.SetFirstActive(query);

                question.Code = query.Code;

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
            query.Questions = query.Questions.OrderBy(q => q.Order).ToList();
            var question = QuestionOptions.Load(query, questionNumber);
            question.Answers = question.Answers.OrderBy(a => a.Order).ToList();

            QuestionOptions.DeleteAnswer(question, answerNumber);
            
            return RedirectToAction("LoadActiveQuery", new { queryID = queryNumber });
        }

        [Authorize]
        public IActionResult AddAnswer(int queryNumber, int number, string answerName)
        {
            var username = User.Identity.Name.ToString();
            var user = UserOptions.GetUser(username);
            var query = QueryOptions.Load(user, queryNumber);
            query.Questions = query.Questions.OrderBy(q => q.Order).ToList();
            var question = QuestionOptions.Load(query, number);
            question.Answers = question.Answers.OrderBy(a => a.Order).ToList();

            QuestionOptions.AddAnswer(question, 0, answerName);

            return RedirectToAction("LoadActiveQuery", new { queryID = queryNumber });
        }

        [Authorize]
        public IActionResult LoadNextQuestion(int queryNumber, int questionNumber, bool fromDelete = false)
        {
            var username = User.Identity.Name.ToString();
            var user = UserOptions.GetUser(username);
            var query = QueryOptions.Load(user, queryNumber);
            query.Questions = query.Questions.OrderBy(q => q.Order).ToList();
            var question = QuestionOptions.Load(query, questionNumber);
            question.Answers = question.Answers.OrderBy(a => a.Order).ToList();

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
                if (fromDelete)
                    QuestionOptions.SetNextActive(question, query.Questions[index].Number);
                else
                    QuestionOptions.SetNextActive(question, query.Questions[index + 1].Number);
            }
            catch (ArgumentOutOfRangeException ex) { }

            try
            {
                //var nextQuestionNumber = query.Questions.First(x => x.Number > questionNumber).Number;
                //var nextQuestion = QuestionOptions.Load(query, nextQuestionNumber);
                var nextQuestion = QuestionOptions.GetActive(query.Questions[index + 1].Number, query.Code, false);
                question.Answers = question.Answers.OrderBy(a => a.Order).ToList();
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
            var query = QueryOptions.Load(user, queryNumber);
            query.Questions = query.Questions.OrderBy(q => q.Order).ToList();

            if (query != null)
            {
                var question = QuestionOptions.Load(query, questionNumber);
                question.Answers = question.Answers.OrderBy(a => a.Order).ToList();

                /*var username = User.Identity.Name.ToString();
                var user = UserOptions.GetUser(username);
                var query = QueryOptions.Load(user, 3);
                DataManager.Question question = new Question();
                */

                /*foreach (DataManager.Question item in query.Questions)
                {
                    if(item != null)
                    {
                        question = item;
                    }
                }*/
                if (question == null)
                {
                    return RedirectToAction(nameof(UserController.Index), "User");
                }

                return View("DisplayResults", question);
            }
            //var q = QuestionOptions.Load(query, question.Number);
            //question.Answers = question.Answers.OrderBy(a => a.Order).ToList();

            return RedirectToAction(nameof(UserController.Index), "User");
        }

        public IActionResult QueryStart()
        {
            return View("QueryStartView");
        }

        [HttpPost]
        public IActionResult InsertQuestion(int queryNumber, int Order, string NewQuestionName)
        {
            var username = User.Identity.Name.ToString();
            var user = UserOptions.GetUser(username);
            var query = QueryOptions.Load(user, queryNumber);
            query.Questions = query.Questions.OrderBy(q => q.Order).ToList();

            QueryOptions.AddQuestion(query,NewQuestionName,Order+1);

            var questionNumber = query.Questions.Max(x => x.Number);
            var question = QuestionOptions.Load(query, questionNumber);
            question.Answers = question.Answers.OrderBy(a => a.Order).ToList();

            return View("LoadActiveQuestion", question);
        }

        [Authorize]
        public IActionResult CloseQuery(int queryID)
        {
            var username = User.Identity.Name.ToString();
            var user = UserOptions.GetUser(username);
            var query = QueryOptions.Load(user, queryID);
            query.Questions = query.Questions.OrderBy(q => q.Order).ToList();

            QueryOptions.Close(query);

            return RedirectToAction(nameof(UserController.Index), "User");
        }
    }
}