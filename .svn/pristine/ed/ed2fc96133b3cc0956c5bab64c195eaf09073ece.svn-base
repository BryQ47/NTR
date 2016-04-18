using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StudentsListJS;
using StudentsListJS.Controllers;
using StudentsListJS.Models;
using System.Web.Mvc;
using System.Web.Http.Results;

namespace StudentsListJS.Tests.Controllers
{
    [TestClass]
    public class StudentsListTests
    {

        [TestMethod]
        public void Index()
        {
            HomeController controller = new HomeController();
            ViewResult result = controller.Index() as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("Lista Studentów JS", result.ViewBag.Title);
        }

        [TestMethod]
        public void StudentsGet()
        {
            var controller = new StudentsController();
            var result = controller.Get() as JsonResult<Response>;
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Content.groups);
            Assert.IsNotNull(result.Content.students);
            Assert.IsTrue(String.IsNullOrEmpty(result.Content.error));
        }

        [TestMethod]
        public void StudentCUD()
        {
            var controller = new StudentsController();

            // Find some group for student
            var result = controller.Get() as JsonResult<Response>;

            // Create
            var stud = new Student();
            stud.FirstName = "Testname";
            stud.LastName = "Testname";
            stud.IndexNo = "8302445220";
            stud.IDGroup = result.Content.groups[0].IDGroup;
            result = controller.Post(stud, "POST") as JsonResult<Response>;
            Assert.IsTrue(String.IsNullOrEmpty(result.Content.error));
            var addedStud = result.Content.students.ToList().Find(s => s.IndexNo.Equals("8302445220"));
            Assert.IsNotNull(addedStud);

            // Update
            stud.IndexNo = "8302445221";
            result = controller.Post(stud, "PUT") as JsonResult<Response>;
            Assert.IsTrue(String.IsNullOrEmpty(result.Content.error));
            var modifiedStud = result.Content.students.ToList().Find(s => s.IndexNo.Equals("8302445221"));
            Assert.IsNotNull(modifiedStud);

            // Delete
            result = controller.Post(stud, "DELETE") as JsonResult<Response>;
            Assert.IsTrue(String.IsNullOrEmpty(result.Content.error));
            var deletedStud = result.Content.students.ToList().Find(s => s.IndexNo.Equals("8302445221"));
            Assert.IsNull(deletedStud);
        }

        [TestMethod]
        public void StudentConcurrentEditing()
        {
            var controller = new StudentsController();

            // Find some group for student
            var result = controller.Get() as JsonResult<Response>;

            // Add student
            var stud = new Student();
            stud.FirstName = "Testname";
            stud.LastName = "Testname";
            stud.IndexNo = "8302445220";
            stud.IDGroup = result.Content.groups[0].IDGroup;
            result = controller.Post(stud, "POST") as JsonResult<Response>;
            Assert.IsTrue(String.IsNullOrEmpty(result.Content.error));
            
            // Try to edit concurrently
            var stud1 = stud.Copy();
            var stud2 = stud.Copy();
            stud1.FirstName = "Edited 1";
            stud2.FirstName = "Edited 2";
            result = controller.Post(stud1, "PUT") as JsonResult<Response>;
            Assert.IsTrue(String.IsNullOrEmpty(result.Content.error));
            result = controller.Post(stud2, "PUT") as JsonResult<Response>;
            Assert.IsFalse(String.IsNullOrEmpty(result.Content.error));
            var modifiedStud = result.Content.students.ToList().Find(s => s.IndexNo.Equals("8302445220"));
            Assert.AreEqual("Edited 1", modifiedStud.FirstName);

            // Make cleanup
            result = controller.Post(stud, "DELETE") as JsonResult<Response>;
            Assert.IsTrue(String.IsNullOrEmpty(result.Content.error));
        }

        [TestMethod]
        public void StudentsConstraintsTest()
        {
            var controller = new StudentsController();

            // Find some group for student
            var result = controller.Get() as JsonResult<Response>;

            // Try add student without group
            var stud = new Student();
            stud.FirstName = "Testname";
            stud.LastName = "Testname";
            stud.IndexNo = "8302445220";
            result = controller.Post(stud, "POST") as JsonResult<Response>;
            Assert.IsFalse(String.IsNullOrEmpty(result.Content.error));
            var addeddStud = result.Content.students.ToList().Find(s => s.IndexNo.Equals("8302445220"));
            Assert.IsNull(addeddStud);

            // Try add student with too long name
            stud.IDGroup = result.Content.groups[0].IDGroup;
            stud.FirstName = "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA";
            result = controller.Post(stud, "POST") as JsonResult<Response>;
            Assert.IsFalse(String.IsNullOrEmpty(result.Content.error));
            addeddStud = result.Content.students.ToList().Find(s => s.IndexNo.Equals("8302445220"));
            Assert.IsNull(addeddStud);

            // Try add student with non unique index
            stud.FirstName = "Testname";
            stud.IndexNo = result.Content.students[0].IndexNo;
            result = controller.Post(stud, "POST") as JsonResult<Response>;
            Assert.IsFalse(String.IsNullOrEmpty(result.Content.error));
            addeddStud = result.Content.students.ToList().Find(s => s.IndexNo.Equals("8302445220"));
            Assert.IsNull(addeddStud);
        }

        [TestMethod]
        public void GroupsGet()
        {
            var controller = new GroupsController();
            var result = controller.Get() as JsonResult<Response>;
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Content.groups);
            Assert.IsTrue(String.IsNullOrEmpty(result.Content.error));
        }

        [TestMethod]
        public void GroupCUD()
        {
            var controller = new GroupsController();

            // Create
            var group = new Group();
            group.Name = "Testgroup XV22";
            var result = controller.Post(group, "POST") as JsonResult<Response>;
            Assert.IsTrue(String.IsNullOrEmpty(result.Content.error));
            var addedGroup = result.Content.groups.ToList().Find(g => g.Name.Equals("Testgroup XV22"));
            Assert.IsNotNull(addedGroup);

            // Update
            group.Name = "Testgroup XV23";
            result = controller.Post(group, "PUT") as JsonResult<Response>;
            Assert.IsTrue(String.IsNullOrEmpty(result.Content.error));
            var modifiedGroup = result.Content.groups.ToList().Find(g => g.Name.Equals("Testgroup XV23"));
            Assert.IsNotNull(modifiedGroup);

            // Delete
            result = controller.Post(group, "DELETE") as JsonResult<Response>;
            Assert.IsTrue(String.IsNullOrEmpty(result.Content.error));
            var deletedGroup = result.Content.groups.ToList().Find(g => g.Name.Equals("Testgroup XV23"));
            Assert.IsNull(deletedGroup);
        }

        [TestMethod]
        public void GroupConcurrentEditing()
        {
            var controller = new GroupsController();

            // Add group
            var group = new Group();
            group.Name = "Testgroup XV22";
            var result = controller.Post(group, "POST") as JsonResult<Response>;
            Assert.IsTrue(String.IsNullOrEmpty(result.Content.error));

            // Try to edit concurrently
            var group1 = group.Copy();
            var group2 = group.Copy();
            group1.Name = "XV22 1";
            group2.Name = "XV22 2";
            result = controller.Post(group1, "PUT") as JsonResult<Response>;
            Assert.IsTrue(String.IsNullOrEmpty(result.Content.error));
            result = controller.Post(group2, "PUT") as JsonResult<Response>;
            Assert.IsFalse(String.IsNullOrEmpty(result.Content.error));
            var modifiedGroup = result.Content.groups.ToList().Find(g => g.Name.Equals("XV22 1"));
            Assert.AreEqual("XV22 1", modifiedGroup.Name);

            // Make cleanup
            result = controller.Post(group, "DELETE") as JsonResult<Response>;
            Assert.IsTrue(String.IsNullOrEmpty(result.Content.error));
        }

        [TestMethod]
        public void GroupsConstraintsTest()
        {
            var controller = new GroupsController();

            // Try group without name
            var group = new Group();
            //group.Name = "Testname";
            var result = controller.Post(group, "POST") as JsonResult<Response>;
            Assert.IsFalse(String.IsNullOrEmpty(result.Content.error));

            // Try add group with non unique name
            var gname = result.Content.groups[0].Name;
            group.Name = gname;
            result = controller.Post(group, "POST") as JsonResult<Response>;
            Assert.IsFalse(String.IsNullOrEmpty(result.Content.error));
        }

        [TestMethod]
        public void DeleteGroupWithStudents()
        {
            var controller = new GroupsController();
            var controller2 = new StudentsController();

            // Create
            var group = new Group();
            group.Name = "Testgroup XV22";
            var result = controller.Post(group, "POST") as JsonResult<Response>;
            Assert.IsTrue(String.IsNullOrEmpty(result.Content.error));

            // Add student
            var stud = new Student();
            stud.FirstName = "Testname";
            stud.LastName = "Testname";
            stud.IndexNo = "8302445220";
            stud.IDGroup = group.IDGroup;
            result = controller2.Post(stud, "POST") as JsonResult<Response>;
            Assert.IsTrue(String.IsNullOrEmpty(result.Content.error));

            // Try delete
            result = controller.Post(group, "DELETE") as JsonResult<Response>;
            Assert.IsFalse(String.IsNullOrEmpty(result.Content.error));
            var deletedGroup = result.Content.groups.ToList().Find(g => g.Name.Equals("Testgroup XV22"));
            Assert.IsNotNull(deletedGroup);

            // Remove student
            result = controller2.Post(stud, "DELETE") as JsonResult<Response>;
            Assert.IsTrue(String.IsNullOrEmpty(result.Content.error));

            // Delete
            result = controller.Post(group, "DELETE") as JsonResult<Response>;
            Assert.IsTrue(String.IsNullOrEmpty(result.Content.error));
            deletedGroup = result.Content.groups.ToList().Find(g => g.Name.Equals("Testgroup XV22"));
            Assert.IsNull(deletedGroup);
        }


    }
}
