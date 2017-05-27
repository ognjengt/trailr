using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web.Http;
using System.Web.Http.Results;
using Trailr.Models;

namespace Trailr.Controllers
{
    public class ProjectsController : ApiController
    {
        [ActionName("GetProjects")]
        public IEnumerable<Project> GetProjects()
        {
            List<Project> povratna = new List<Project>();
            Project p1 = new Project();
            p1.DateCreated = DateTime.Now;
            p1.Id = new MongoDB.Bson.ObjectId();
            p1.TimeSpent = new TimeSpan(10, 15, 20);
            p1.Title = "Test project 1";
            p1.UserEmail = "ognjen";

            Project p2 = new Project();
            p2.DateCreated = DateTime.Now;
            p2.Id = new MongoDB.Bson.ObjectId();
            p2.TimeSpent = new TimeSpan(20, 2, 33);
            p2.Title = "Drugi proj";
            p2.UserEmail = "ognjen";

            povratna.Add(p1);
            povratna.Add(p2);

            return povratna;


        } 
    }
}
