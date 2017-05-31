﻿using System;
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
        MongoDatabaseCustom database = new MongoDatabaseCustom("mongodb://localhost", "trailr");

        [ActionName("GetProjects")]
        public async Task<IEnumerable<Project>> GetProjects(string email)
        {
            List<Project> povratna = new List<Project>();
            povratna = await database.GetProjects(email);
            return povratna;
        }

        [HttpPost]
        [ActionName("AddProject")]
        public async Task<IEnumerable<Project>> AddProject([FromBody]ProjectRequest prequest)
        {
            Project p = new Project() { Title = prequest.Title, Id = new MongoDB.Bson.ObjectId(), DateCreated = DateTime.Now, TimeSpent = new TimeSpan(0, 0, 0), UserEmail = prequest.UserEmail };
            await database.AddProject(p);
            List<Project> povratna = await database.GetProjects(prequest.UserEmail);
            return povratna;
        }

        [ActionName("GetProjectInfo")]
        public async Task<Project> GetProjectInfo(string id)
        {
            Project p = await database.GetProject(id);
            return p;
        }

    }
}
