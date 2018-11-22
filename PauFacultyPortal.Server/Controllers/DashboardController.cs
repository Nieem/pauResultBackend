using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;
using System.Web.Http;
using PauFacultyPortal.Service;
using PauFacultyPortal.ViewModel;
using PauFacultyPortal.ViewModel.Dashboard;
using System.Security.Claims;
using System.Net;

namespace PauFacultyPortal.Server.Controllers
{
    public class DashboardController : ApiController
    {
        DashboardService service = new DashboardService();

        [HttpGet]
        public HttpResponseMessage Get()
        {
            try
            {
                var loginId = ((ClaimsIdentity)User.Identity).FindFirst("LoginID").Value;
                List<DashboardViewModel> models = loginId == null ? null : (loginId.Length == 6 ? service.GetTeacherProfileInfo(loginId): service.GetStudentProfileInfo(loginId));
                // GetTeacherProfileInfo   GetStudentProfileInfo     // GetDashboardProfileInfo
                return models != null ? Request.CreateResponse(HttpStatusCode.OK, models) : Request.CreateErrorResponse(HttpStatusCode.NotFound,
                    "No data found");
            }
            catch (Exception ex)
            {

                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }

        }


        [HttpGet]
        public HttpResponseMessage GetBarChartData(bool barChart)
        {
            try
            {
                var loginId = ((ClaimsIdentity)User.Identity).FindFirst("LoginID").Value;

                var models = loginId == null ? null : service.GetBarChartData(loginId, barChart);

                return models != null ? Request.CreateResponse(HttpStatusCode.OK, models) : Request.CreateErrorResponse(HttpStatusCode.NotFound,
                    "No data found");
            }
            catch (Exception ex)
            {

                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }

        }


        [HttpGet]
        public HttpResponseMessage GetLineChartData(bool lineChart)
        {
            try
            {
                var loginId = ((ClaimsIdentity)User.Identity).FindFirst("LoginID").Value;

                var models = loginId == null ? null : service.GetLineChartData(loginId, lineChart);

                return models != null ? Request.CreateResponse(HttpStatusCode.OK, models) : Request.CreateErrorResponse(HttpStatusCode.NotFound,
                    "No data found");
            }
            catch (Exception ex)
            {

                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }

        }
    }
}