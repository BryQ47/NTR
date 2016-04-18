using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentsListJS.Models
{
    public class GroupDTO
    {
        public int IDGroup { get; set; }
        public string Name { get; set; }
        public byte[] Stamp { get; set; }
    }
}