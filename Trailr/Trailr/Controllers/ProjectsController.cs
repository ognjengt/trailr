using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web.Http;
using System.Web.Http.Results;
using Trailr.Models;
using Trailr.Helpers;
using Trailr.Controllers;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace Trailr.Controllers
{
    public class ProjectsController : ApiController
    {
        [ActionName("GetProjects")]
        public async Task<IEnumerable<Project>> GetProjects(string email)
        {
            MongoDatabaseCustom database = new MongoDatabaseCustom("mongodb://localhost", "trailr");
            List<Project> povratna = new List<Project>();

            // Cisto da bi ubacio random element da vidim da li radi i radi.
            //await database.ProjectCollection.InsertOneAsync(new Project { UserEmail = "ognjen@ognjen.com", DateCreated = DateTime.Now, TimeSpent = new TimeSpan(20,15,1), Title = "Jos jedan breee", Id = new MongoDB.Bson.ObjectId() });

            // TODO: ovde nekako naci nacin da se prosledi email od trenutnog korisnika sa sesije
            povratna = await database.GetProjects(email);

            return povratna;


        }


    }
}
