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

        public IActionResult JoinQuery(string code, Question question)
        {
            if (question != null)
            {
                question = QuestionOptions.GetActive(question.Number, code, question.IsAnswered);
                if (question == null)
                {
                    question = new Question(code);
                }
            }
            else
            {
                question = new Question(code);
            }

            if (QueryOptions.ValidCode(code)){
                return View("ClientView", question);
            }
            else
            {
                return View("QueryClosed", question);
            }
            
        }

        [HttpPost]
        public IActionResult SelectAnswer(int queryNumber, int questionNumber, int number, string code)
        {
            var username = User.Identity.Name.ToString();
            var user = UserOptions.GetUser(username);
            var query = QueryOptions.Load(user, queryNumber);
            var question = QuestionOptions.Load(query, questionNumber);
            var answer = AnswerOptions.Load(question, number);

            AnswerOptions.Select(answer);
            question.IsAnswered = true;

            return RedirectToAction("JoinQuery", "Client", new { code, question }); 
        }
    }
}
