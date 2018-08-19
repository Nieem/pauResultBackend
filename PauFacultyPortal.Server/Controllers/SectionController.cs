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
        public List<SectionListViewModel> Get(int SemesterID)
        {
            //ResponseModel response = new ResponseModel();
            //try
            //{
            var loginId = ((ClaimsIdentity)User.Identity).FindFirst("LoginID").Value;
            List<SectionListViewModel> models = service.GetSections(SemesterID, loginId);
            // response = new ResponseModel(models, true, "", null);

            //}
            //catch (Exception exception)
            //{

            //    response = new ResponseModel(null, false, "Error Found", exception);
            //}


            //return Ok(response);
            return models;
        }

        [HttpGet]
        public List<SectionStudentsViewModel> GetSectionStudent(int SectionID)
        {


            //ResponseModel response = new ResponseModel();
            //try
            //{
            // List<ProfileViewModel>

            var loginId = ((ClaimsIdentity)User.Identity).FindFirst("LoginID").Value;
            List<SectionStudentsViewModel> models = service.GetSectionWiseStudents(SectionID, loginId);
            // response = new ResponseModel(models, true, "", null);

            //}
            //catch (Exception exception)
            //{

            //    response = new ResponseModel(null, false, "Error Found", exception);
            //}


            //return Ok(response);
            return models;

        }

        [HttpPut]
        public HttpResponseMessage UpdateStudentData([FromBody]SectionStudentsViewModel students)
        {

            try
            {
                var CheckStudent = service.CheckStudentEntity(students);

                if (CheckStudent)
                {
                    int result = service.UpdateStuentResult(students);
                    return result > 0 ? Request.CreateResponse(HttpStatusCode.OK, students) : Request.CreateResponse(HttpStatusCode.NotModified, students);
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

    }
}
