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
        public IMongoCollection<Project> ProjectCollection { get; set; }

        //ovde kako bude rasla baza, imace sve kolekcije

        public MongoDatabaseCustom() { }

        public MongoDatabaseCustom(string address, string dbName)
        {
            this.Client = new MongoClient(address);
            this.Database = Client.GetDatabase(dbName);
            this.UserCollection = Database.GetCollection<UserAccount>("users");
            this.ProjectCollection = Database.GetCollection<Project>("projects");
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

        public async Task<List<Project>> GetProjects(string email)
        {
            var projects = await ProjectCollection.Find(x => x.UserEmail == email).ToListAsync();
            return projects;
        }
        
        //Add project
        public async Task<bool> AddProject(Project p)
        {
            await ProjectCollection.InsertOneAsync(p);
            return true;
        }

        public async Task<Project> GetProject(string id)
        {
            ObjectId obId = ObjectId.Parse(id);
            Project project = await ProjectCollection.Find(x => x.Id == obId).SingleAsync();
            return project;
        }

        public async Task<bool> UpdateProject(string id, int hoursPassed, int minutesPassed, int secondsPassed)
        {
            var filter = Builders<Project>.Filter.Eq("Id",ObjectId.Parse(id));
            var update = Builders<Project>.Update.Set("HoursSpent", hoursPassed.ToString()).Set("MinutesSpent", minutesPassed.ToString()).Set("SecondsSpent", secondsPassed.ToString());
            var result = await ProjectCollection.UpdateOneAsync(filter, update);
            return true;
        }
    }
}