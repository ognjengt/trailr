using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;
using MongoDB.Driver;
using Trailr.Models;

namespace Trailr.Helpers
{
    public class MongoDatabaseCustom
    {
        public MongoClient Client { get; set; }
        public IMongoDatabase Database { get; set; }

        //ovde kako bude rasla baza, imace sve kolekcije

        public MongoDatabaseCustom() { }

        public MongoDatabaseCustom(string address, string dbName)
        {
            this.Client = new MongoClient(address);
            this.Database = Client.GetDatabase(dbName);
        }
    }
}