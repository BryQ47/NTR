﻿using System;
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

namespace StudentsListJS.Controllers
{
    public class GroupsController : ExtendedApiController
    {
        private IHttpActionResult DefaultResponse(string exMsg = null)
        {
            try
            {
                var groups = from g in storage.GetGroups()
                             select new GroupDTO
                             {
                                 IDGroup = g.IDGroup,
                                 Name = g.Name,
                                 Stamp = g.Stamp
                             };

                return Json(new Response { students = null, groups = groups.ToArray(), error = exMsg });
            }
            catch (Exception ex)
            {
                log.Error("Błąd serwera" + ex.Message);
                return ErrorResponse("Nie można załadować listy grup. " + ex.Message, HttpStatusCode.InternalServerError);
            }
        }


        // GET: api/Groups
        public IHttpActionResult Get()
        {
            return DefaultResponse();
        }

        // POST: api/Students
        public IHttpActionResult Post([FromBody()] Group group, [FromUri()] string operation)
        {
            string msg = null;
            try
            {
                switch (operation)
                {
                    case "POST":
                        storage.AddGroup(group);
                        break;
                    case "PUT":
                        storage.UpdateGroup(group);
                        break;
                    case "DELETE":
                        storage.DeleteGroup(group);
                        break;
                    default:
                        msg = "Nieznana operacja";
                        break;
                }
            }
            catch (DBConcurrencyException cex)
            {
                log.Error("Concurrency exception. Opracja: " + operation + ". " + cex.Message);
                msg = "Grupa została zmodyfikowana! " + cex.Message;
            }
            catch (Exception ex)
            {
                log.Error("Błąd mofyfikacji. Operacja: " + operation + ". " + ex.Message);
                msg = ex.Message;
            }
            return DefaultResponse(msg);
        }
    }
}