using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentsListJS.Models
{
    public class Response
    {
        public StudentDTO[] students { get; set; }
        public GroupDTO[] groups { get; set; }
        public string error { get; set; }
    }
}