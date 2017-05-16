using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Trailr.Controllers
{
    public class DashboardController : Controller
    {
        // GET: Dashboard
        [Authorize]
        public ActionResult Index()
        {
            Session["userEmail"] = Request.Cookies["userEmail"].Value;
            return View();
        }

        public ActionResult Project(int id)
        {
            // get project i baci ga na view
            
            return Content("id= " + id);
        }
    }
}