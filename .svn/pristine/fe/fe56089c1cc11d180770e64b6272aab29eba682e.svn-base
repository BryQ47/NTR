using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using StudentsListMvc.Models;
using log4net;
using System.Configuration;
using PagedList;

namespace StudentsListMvc.Controllers
{
    public class GroupsController : Controller
    {
        private Storage storage = new Storage();

        private int pageSize = Int32.Parse(ConfigurationManager.AppSettings["entriesPerPage"]);

        private ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public ActionResult Index(string error, int? page)
        {
            try
            { 
                var groups = storage.GetGroups();

                string errorMsg = error;
                //if (error != null && error == true)
                //    errorMsg = TempData["error"].ToString();

                int pageNumber = (page ?? 1);
                var groupsPaged = groups.ToPagedList(pageNumber, pageSize);

                return View(new GroupsList() { Groups = groupsPaged, ErrorMsg = errorMsg });
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                return View(new GroupsList() { Groups = storage.GetGroups().ToPagedList(1, pageSize), ErrorMsg = ex.Message });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CUDGroup(GroupsList groupsList, string command)
        {
            try
            {

                var group = groupsList.SelectedGroup;

                if (String.Equals(command, "Create"))
                {
                    log.Info("Creating student");
                    storage.AddGroup(group);
                }
                else if (String.Equals(command, "Update"))
                {
                    log.Info("Updating student");
                    storage.UpdateGroup(group);
                }
                else if (String.Equals(command, "Delete"))
                {
                    log.Info("Deleting student");
                    storage.DeleteGroupById(group.IDGroup);
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                //TempData["error"] = ex.Message;
                return RedirectToAction("Index", new { error = ex.Message });
            }
        }
    }
}
