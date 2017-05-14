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

namespace Trailr.Controllers
{
    public class AccountController : Controller
    {
        PasswordCrypter pcrypter = new PasswordCrypter();
        MongoDatabaseCustom mongoDatabase = new MongoDatabaseCustom("mongodb://localhost","trailr");
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

        // POST: Account/Login
        [HttpPost]
        public string LoginUser()
        {
            return "logged?";
        }

        // GET: Account/Register
        public ActionResult Register()
        {
            return View();
        }

        // POST: Account/Register
        [HttpPost]
        public async Task<ActionResult> Register(UserAccount account)
        {
            string hashed = pcrypter.CryptPassword(account.Password);

            var collection = mongoDatabase.Database.GetCollection<UserAccount>("users");

            await collection.InsertOneAsync(new UserAccount { Email = account.Email, Username = account.Username, Password = hashed });

            return View("Login");
        }

    }
}