using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using MongoDB;
using Trailr.Models;
using Trailr.Helpers;
using System.Threading.Tasks;

namespace Trailr.Controllers
{
    public class DashboardController : Controller
    {
        MongoDatabaseCustom mongoDatabase = new MongoDatabaseCustom("mongodb://localhost", "trailr");

        // GET: Dashboard
        [Authorize]
        public async Task<ActionResult> Index()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                string userEmail = Request.Cookies["userEmail"].Value;
                UserAccount user = await mongoDatabase.GetUser(userEmail);
                Session["user"] = user;
                return View();
            }
            return RedirectToAction("Login", "Account");
        }

        public string GetUserMail()
        {
            UserAccount currentUser = (UserAccount)Session["user"];
            return currentUser.Email;
        }

        public async Task<ActionResult> Project(string id)
        {
            ViewBag.ProjectId = id;
            return View();
        }
    }
}