using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Trailr.Models
{
    public class UpdateProjectRequest
    {
        public string Id { get; set; }
        public int HoursPassed { get; set; }
        public int MinutesPassed { get; set; }
        public int SecondsPassed { get; set; }
    }
}