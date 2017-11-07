using DataManager;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Querier.Controllers
{
    public class QueryController : Controller
    {
        [Authorize]
        public IActionResult ManageQuery()
        {
            var username = User.Identity.Name.ToString();
            var user = UserOptions.GetUser(username);
            Query query = QueryOptions.Load(user, 1);
            return View("LoadQuery", query);
        }

        [Authorize]
        public IActionResult LoadQuery(Query query)
        {
            return View(query);
        }

        [Authorize]
        public IActionResult DeleteQuestion(int queryID, int questionNumber)
        {
            var username = User.Identity.Name.ToString();
            var user = UserOptions.GetUser(username);
            var query = QueryOptions.Load(user, queryID);

            QueryOptions.DeleteQuestion(query, questionNumber);

            query = QueryOptions.Load(user, queryID);

            return View("LoadQuery", query);
        }

        [Authorize]
        public IActionResult InsertQuestion(int queryID)
        {
            var username = User.Identity.Name.ToString();
            var user = UserOptions.GetUser(username);
            var query = QueryOptions.Load(user, queryID);

            QueryOptions.AddQuestion(query);

            query = QueryOptions.Load(user, queryID);

            return View("LoadQuery", query);
        }
    }
}