using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StudentsListMvc.Models
{
    public class StudentsList
    {
        public StudentsList()
        {

        }
        public StudentsList(IPagedList<Student> students, List<Group> groups, int? selectedGroup, string selectedCity, string errorMsg, int page)
        {
            Page = page;
            Students = students;
            if (selectedGroup != null)
            {
                var chosenGroup = groups.Find(g => g.IDGroup == selectedGroup);
                Groups = new SelectList(groups, "IDGroup", "Name", chosenGroup);
            }
            else
            {
                Groups = new SelectList(groups, "IDGroup", "Name");
            }
            SelectedGroup = selectedGroup;
            SelectedCity = selectedCity;
            ErrorMsg = errorMsg;

            if (selectedGroup != null)
                SelectedStudent = new Student() { IDGroup = selectedGroup.Value };
        }
        public IPagedList<Student> Students { get; set; }
        public Student SelectedStudent { get; set; }
        public SelectList Groups { get; set; }
        public int? SelectedGroup { get; set; }
        public string SelectedCity { get; set; }
        public string ErrorMsg { get; set; }

        public int Page { get; set; }
    }
}