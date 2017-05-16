using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;
using System.Diagnostics;

namespace Trailr.Models
{
    public class Project
    {
        public ObjectId Id { get; set; }
        public string Title { get; set; }
        public TimeSpan TimeSpent { get; set; }
        public DateTime DateCreated { get; set; }
        public string UserEmail { get; set; }
    }
}