using PauFacultyPortal.Service.Section;
using PauFacultyPortal.ViewModel.Section;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;

namespace PauFacultyPortal.Server.Controllers
{
    public class SectionController : ApiController
    {

        SectionService service = new SectionService();

        [HttpGet]
        public HttpResponseMessage Get(int SemesterID)
        {
            try
            {
                var loginId = ((ClaimsIdentity)User.Identity).FindFirst("LoginID").Value;
                List<SectionListViewModel> models = service.GetSections(SemesterID, loginId);
                return models.Count > 0 ? Request.CreateResponse(HttpStatusCode.OK, models) : Request.CreateErrorResponse(HttpStatusCode.NotFound,
                    "Section does not exist");
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }

        }

        [HttpGet]
        public HttpResponseMessage GetSectionStudent(int SectionID)
        {
            try
            {
                var loginId = ((ClaimsIdentity)User.Identity).FindFirst("LoginID").Value;
                List<SectionStudentsViewModel> models = service.GetSectionWiseStudents(SectionID, loginId);
                return models.Count > 0 ? Request.CreateResponse(HttpStatusCode.OK, models) : Request.CreateErrorResponse(HttpStatusCode.NotFound,
                   "No Student Found");

            }
            catch (Exception ex)
            {

                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }

        }

        //[HttpPut]
        //public HttpResponseMessage SectionMarkSubmitFinal([FromBody] SectionFinalUpdateViewModel section)
        //{
        //    try
        //    {
        //        bool result = section.SectionID > 0 ? service.UpdateSectionSubmitFinal(section.SectionID) : false;
        //        return result == true ? Request.CreateResponse(HttpStatusCode.OK, section.SectionID) : Request.CreateResponse(HttpStatusCode.NotModified, section.SectionID);
        //    }
        //    catch (Exception ex)
        //    {

        //        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
        //    }
        //}


        [HttpPut]
        public HttpResponseMessage UpdateStudentData([FromBody]SectionStudentsViewModel students)
        {

            try
            {
                if (students != null && students.FinalUpdate != null)
                {
                    return students.FinalUpdate == false ? UpdateStudents(students) : FinalSubmit(students);

                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Request Information Not Found");
                }



            }
            catch (Exception ex)
            {

                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }

        }

        private HttpResponseMessage UpdateStudents(SectionStudentsViewModel students)
        {
            var CheckStudent = service.CheckStudentEntity(students);

            if (CheckStudent)
            {
                bool result = service.UpdateStuentResult(students);
                return result == true ? Request.CreateResponse(HttpStatusCode.OK, students) : Request.CreateResponse(HttpStatusCode.NotModified, students);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Request Information Not Found");
            }
        }

        private HttpResponseMessage FinalSubmit(SectionStudentsViewModel students)
        {


            if (students.SectionID > 0)
            {
                bool result = service.UpdateSectionSubmitFinal(students.SectionID);
                return result == true ? Request.CreateResponse(HttpStatusCode.OK, students.SectionID) : Request.CreateResponse(HttpStatusCode.NotModified, students.SectionID);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Request Information Not Found");
            }
        }

    }
}
