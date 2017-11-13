using DataManager;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Querier.Controllers
{
    public class UserController : Controller
    {
        public object FormsAuthentication { get; private set; }

        [Authorize]
        public IActionResult Index(int userID)
        {
            string loginID = User.Identity.Name.ToString();
            User user = UserOptions.GetUser(loginID);

            return View(user);
        }

        [Authorize]
        public IActionResult Create(int userID)
        {
            var username = User.Identity.Name.ToString();
            var user = UserOptions.GetUser(username);

            //var queryID = query.Number;
            //return RedirectToAction("ManageQuery","Query", queryID);

            UserOptions.AddQuery(user);

            var queryid = user.Queries.Max(x => x.Number);
            var query = QueryOptions.Load(user, queryid);
            
            return RedirectToAction("LoadQuery", "Query", query);

        }

        [Authorize]
        public IActionResult DeleteQuery(int queryNumber, int userId)
        {
            var username = User.Identity.Name.ToString();
            var user = UserOptions.GetUser(username);
            var query = QueryOptions.Load(user, queryNumber);

            UserOptions.DeleteQuery(user, queryNumber);

            user = UserOptions.GetUser(username);

            return View("Index", user);
        }
    }
}