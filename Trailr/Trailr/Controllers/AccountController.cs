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
        public async Task<ActionResult> Index()
        {
            List<UserAccount> listaUsera = await GetUsers();
            ViewBag.Users = listaUsera;
            return View("Login");
        }
        
        // GET: Account/Login
        public async Task<ActionResult> Login()
        {
            List<UserAccount> listaUsera = await GetUsers();
            ViewBag.Users = listaUsera;
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
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(UserAccount account)
        {
            string hashed = pcrypter.CryptPassword(account.Password);
            var userCollection = mongoDatabase.Database.GetCollection<UserAccount>("users");
            await userCollection.InsertOneAsync(new UserAccount { Email = account.Email, Username = account.Username, Password = hashed });

            List<UserAccount> listaUsera = await GetUsers();
            ViewBag.Users = listaUsera;

            return View("Login");
        }

        public async Task< List<UserAccount> > GetUsers()
        {
            var userCollection = mongoDatabase.Database.GetCollection<UserAccount>("users");
            var usrs = await userCollection.Find(_ => true).ToListAsync();
            return usrs;
        }

    }
}