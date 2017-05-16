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
            string userEmail = Request.Cookies["userEmail"].Value;
            UserAccount user = await mongoDatabase.GetUser(userEmail);
            Session["user"] = user;
            return View();
        }

        public ActionResult Project(int id)
        {
            // get project i baci ga na view
            
            return Content("id= " + id);
        }
    }
}