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
    [RoutePrefix("api")]
    public class DashboardController : ApiController
    {
        DashboardService service = new DashboardService();

        [Route("Dashboard/Get")]
        [HttpGet]
        public HttpResponseMessage Get()
        {
            try
            {
                var loginId = ((ClaimsIdentity)User.Identity).FindFirst("LoginID").Value;
                List<DashboardViewModel> models = loginId == null ? null : service.GetTeacherProfileInfo(loginId);
                return models != null ? Request.CreateResponse(HttpStatusCode.OK, models) : Request.CreateErrorResponse(HttpStatusCode.NotFound,
                    "No data found");
            }
            catch (Exception ex)
            {

                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }

        }

        [Route("Dashboard/StudentDashboard")]
        [HttpGet]
        public HttpResponseMessage GetStudentDashboard()
        {
            try
            {
                var loginId = ((ClaimsIdentity)User.Identity).FindFirst("LoginID").Value;
                List<StudentDashBoardViewModel> models = loginId == null ? null : service.GetStudentProfileInfo(loginId);
                return models != null ? Request.CreateResponse(HttpStatusCode.OK, models) : Request.CreateErrorResponse(HttpStatusCode.NotFound,
                    "No data found");
            }
            catch (Exception ex)
            {

                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [Route("Dashboard/GetcourseListbyCurriculum")]
        [HttpGet]
        public HttpResponseMessage GetcourseListbyCurriculum()
        {
            try
            {
                var loginId = ((ClaimsIdentity)User.Identity).FindFirst("LoginID").Value;
                var courseData = service.GetCourselistByCuriculum(loginId);
                return courseData != null ? Request.CreateResponse(HttpStatusCode.OK,courseData): Request.CreateErrorResponse(HttpStatusCode.NotFound, "No Data Found");
            }
            catch (Exception Ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, Ex);
            }
        }


        [Route("Dashboard/BarChartData")]
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

        [Route("Dashboard/LineChartData")]
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