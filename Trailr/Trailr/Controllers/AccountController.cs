using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Security.Cryptography;
using Trailr.Models;
using Trailr.Helpers;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Threading.Tasks;
using System.Web.Security;

namespace Trailr.Controllers
{
    public class AccountController : Controller
    {
        PasswordCrypter pcrypter = new PasswordCrypter();
        MongoDatabaseCustom mongoDatabase = new MongoDatabaseCustom("mongodb://localhost","trailr");
        // GET: Account
        public ActionResult Index()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Dashboard");
            }
            return View("Login");
        }
        
        // GET: Account/Login
        public ActionResult Login()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Dashboard");
            }
            return View();
        }

        // POST: Account/Login
        [HttpPost]
        public async Task<ActionResult> Login(UserAccount account)
        {
            var user = await GetUser(account.Email);
            if (user == null)
            {
                // baci ga na view da ne postoji taj user
                ViewBag.ValidationMessage = "Ne postoji user";
                return View("Login");
            }
            else
            {
                // check pass
                if (pcrypter.ValidatePassword(account.Password,user.Password))
                {
                    // ok
                    FormsAuthentication.SetAuthCookie(account.Email,false);
                    return RedirectToAction("Index","Dashboard");
                }
                else
                {
                    // nije dobar pass
                    ViewBag.ValidationMessage = "Nije dobar pass";
                    return View("Login");
                }
            }
        }

        // GET: Account/LogOut
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return View("Login");
        }

        // GET: Account/Register
        public ActionResult Register()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Dashboard");
            }
            return View();
        }

        // POST: Account/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(UserAccount account)
        {
            // ovde provere...

            string hashed = pcrypter.CryptPassword(account.Password);
            await mongoDatabase.UserCollection.InsertOneAsync(new UserAccount { Email = account.Email, Username = account.Username, Password = hashed });

            return RedirectToAction("Login");
        }

        public async Task< List<UserAccount> > GetUsers()
        {
            var usrs = await mongoDatabase.UserCollection.Find(_ => true).ToListAsync();
            return usrs;
        }

        public async Task<UserAccount> GetUser(string email)
        {
            UserAccount user = null;
            try
            {
                user = await mongoDatabase.UserCollection.Find(u => u.Email == email).SingleAsync();
            }
            catch (Exception)
            {
                user = null;
            }
            
            return user;

        }

    }
}