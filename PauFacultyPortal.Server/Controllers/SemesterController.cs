using PauFacultyPortal.Service.Semester;
using PauFacultyPortal.ViewModel;
using PauFacultyPortal.ViewModel.Semester;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;

namespace PauFacultyPortal.Server.Controllers
{
    public class SemesterController : ApiController
    {
        SemesterService service = new SemesterService();
        public HttpResponseMessage Get()
        {

            try
            {
                var loginId = ((ClaimsIdentity)User.Identity).FindFirst("LoginID").Value;
                List<SemesterViewModel> semesters = service.GetSemesters(loginId);
                return semesters != null ? Request.CreateResponse(HttpStatusCode.OK, semesters) : Request.CreateErrorResponse(HttpStatusCode.NotFound,
                  "No data found");
            }
            catch (Exception exception)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, exception);

            }

            
        }
    }
}
