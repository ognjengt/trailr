using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Trailr.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Index()
        {
            return View("Login");
        }
        
        // GET: Account/Login
        public ActionResult Login()
        {
            return View();
        }

        // GET: Account/Register
        public ActionResult Register()
        {
            return View();
        }
    }
}