using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;

namespace Trailr.Models
{
    public class Project
    {
        public ObjectId Id { get; set; }
        public string Title { get; set; }
    }
}