using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using StudentsListJS.Models;
using System.Web.Http.Results;

namespace StudentsListJS.Controllers
{
    public class StudentsController : ExtendedApiController
    {
        private IHttpActionResult DefaultResponse(string exMsg = null)
        {
            try
            {
                var students = from s in storage.GetStudents()
                               select new StudentDTO
                               {
                                   BirthDate = s.BirthDate,
                                   BirthPlace = s.BirthPlace,
                                   FirstName = s.FirstName,
                                   IDGroup = s.IDGroup,
                                   IDStudent = s.IDStudent,
                                   IndexNo = s.IndexNo,
                                   LastName = s.LastName,
                                   GroupName = s.Group.Name,
                                   Stamp = s.Stamp
                               };

                var groups = from g in storage.GetGroups()
                             select new GroupDTO
                             {
                                 IDGroup = g.IDGroup,
                                 Name = g.Name,
                                 Stamp = g.Stamp
                             };

                return Json(new Response { students = students.ToArray(), groups = groups.ToArray(), error = exMsg });
            }
            catch (Exception ex)
            {
                log.Error("Błąd serwera" + ex.Message);
                return ErrorResponse("Nie można załadować listy studentów. " + ex.Message, HttpStatusCode.InternalServerError);
            }
        }


        // GET: api/Students
        public IHttpActionResult Get()
        {
            return DefaultResponse();
        }

        // POST: api/Students
        public IHttpActionResult Post([FromBody()] Student student, [FromUri()] string operation)
        {
            string msg = null;
            try
            {
                switch (operation)
                {
                    case "POST":
                        storage.AddStudent(student);
                        break;
                    case "PUT":
                        storage.UpdateStudent(student);
                        break;
                    case "DELETE":
                        storage.DeleteStudent(student);
                        break;
                    default:
                        msg = "Nieznana operacja";
                        break;
                }
            }
            catch (DBConcurrencyException cex)
            {
                log.Error("Concurrency exception. Opracja: " + operation + ". " + cex.Message);
                msg = "Student został zmodyfikowany! " + cex.Message;
            }
            catch (Exception ex)
            {
                log.Error("Błąd mofyfikacji. " + ex.Message);
                msg = ex.Message;
            }
            return DefaultResponse(msg);
        }
    }
}