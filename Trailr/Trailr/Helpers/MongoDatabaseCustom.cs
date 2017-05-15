using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;
using MongoDB.Driver;
using Trailr.Models;
using System.Threading.Tasks;

namespace Trailr.Helpers
{
    public class MongoDatabaseCustom
    {
        public MongoClient Client { get; set; }
        public IMongoDatabase Database { get; set; }
        public IMongoCollection<UserAccount> UserCollection { get; set; }

        //ovde kako bude rasla baza, imace sve kolekcije

        public MongoDatabaseCustom() { }

        public MongoDatabaseCustom(string address, string dbName)
        {
            this.Client = new MongoClient(address);
            this.Database = Client.GetDatabase(dbName);
            this.UserCollection = Database.GetCollection<UserAccount>("users");
        }

        public async Task<List<UserAccount>> GetUsers()
        {
            var usrs = await UserCollection.Find(_ => true).ToListAsync();
            return usrs;
        }

        public async Task<UserAccount> GetUser(string email)
        {
            UserAccount user = null;
            try
            {
                user = await UserCollection.Find(u => u.Email == email).SingleAsync();
            }
            catch (Exception)
            {
                user = null;
            }

            return user;

        }
    }
}