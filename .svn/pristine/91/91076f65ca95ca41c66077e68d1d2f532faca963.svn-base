using System.Net;
using System.Net.Http;
using System.Web.Http;
using StudentsListJS.Models;
using log4net;

namespace StudentsListJS.Controllers
{
    public abstract class ExtendedApiController : ApiController
    {
        protected Storage storage = new Storage();

        protected ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public IHttpActionResult ErrorResponse(string msg, HttpStatusCode code = HttpStatusCode.BadRequest)
        {
            HttpResponseMessage responseMsg = new HttpResponseMessage(HttpStatusCode.InternalServerError);
            responseMsg.Content = new StringContent("One or more errors occured");
            return ResponseMessage(responseMsg) as IHttpActionResult;
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
