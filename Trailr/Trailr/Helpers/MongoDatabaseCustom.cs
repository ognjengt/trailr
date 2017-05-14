using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Trailr.Helpers
{
    public class MongoDatabaseCustom
    {
        public MongoClient Client { get; set; }
        public IMongoDatabase Database { get; set; }

        public MongoDatabaseCustom() { }

        public MongoDatabaseCustom(string address, string dbName)
        {
            this.Client = new MongoClient(address);
            this.Database = Client.GetDatabase(dbName);
        }
    }
}