using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentsListMvc.Models
{
    public class GroupsList
    {

        public IPagedList<Group> Groups { get; set; }
        public Group SelectedGroup { get; set; }
        public string ErrorMsg { get; set; }

    }
}