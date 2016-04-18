using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using StudentsListMvc.Models;
using PagedList;
using System.Configuration;
using log4net;

namespace StudentsListMvc.Controllers
{
    public class StudentsController : Controller
    {
        private Storage storage = new Storage();

        private int pageSize = Int32.Parse(ConfigurationManager.AppSettings["entriesPerPage"]);

        private ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public ActionResult Index(string searchCity, string currentCity, int? selectedGroup, int? currentGroup, int? page, string error)
        {
            int pageNumber = (page ?? 1);

            try {

                var students = storage.GetStudents();

                if (!String.IsNullOrEmpty(searchCity))
                {
                    page = 1;
                }
                else
                {
                    searchCity = currentCity;
                }

                if (!String.IsNullOrEmpty(searchCity))
                {
                    students = students.Where(s => (String.IsNullOrEmpty(s.BirthPlace)) ? false : s.BirthPlace.Contains(searchCity)).ToList();
                }


                if (selectedGroup != null)
                {
                    page = 1;
                }
                else
                {
                    selectedGroup = currentGroup;
                }

                if (selectedGroup != null)
                {
                    students = students.Where(s => s.IDGroup == selectedGroup).ToList();
                }

                string errorMsg = "";
                //if (error != null && error == true)
                //    errorMsg = TempData["error"].ToString();
                errorMsg = error;

                
                var studentsPaged = students.ToPagedList(pageNumber, pageSize);

                return View(new StudentsList(studentsPaged, storage.GetGroups(), selectedGroup, searchCity, errorMsg, pageNumber));
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                return View(new StudentsList(storage.GetStudents().ToPagedList(1, pageSize), storage.GetGroups(), null, "", ex.Message, pageNumber));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CUDStudent(StudentsList studentsList, string command)
        {
            try {

                var student = studentsList.SelectedStudent;

                if (String.Equals(command, "Create"))
                {
                    log.Info("Creating student");
                    storage.AddStudent(student);
                }
                else if (String.Equals(command, "Update"))
                {
                    log.Info("Updating student");
                    storage.UpdateStudent(student);
                }
                else if (String.Equals(command, "Delete"))
                {
                    log.Info("Deleting student");
                    storage.DeleteStudentById(student.IDStudent);
                }

                return RedirectToAction("Index", new { currentCity = studentsList.SelectedCity, currentGroup = studentsList.SelectedGroup, page = studentsList.Page });
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                //TempData["error"] = ex.Message;
                return RedirectToAction("Index", new { currentCity = studentsList.SelectedCity, currentGroup = studentsList.SelectedGroup, error = ex.Message, page = studentsList.Page });
            }
        }
    }
}
