﻿using PauFacultyPortal.Service.Section;
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

        [HttpPut]
        public HttpResponseMessage UpdateStudentData([FromBody]SectionStudentsViewModel students)
        {

            try
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
            catch (Exception ex)
            {

                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }

        }

        [HttpPut]
        public HttpResponseMessage SectionMarkSubmitFinal(int sectionID)
        {
            try
            {
                bool result = service.UpdateSectionSubmitFinal(sectionID);
                return result == true ? Request.CreateResponse(HttpStatusCode.OK, sectionID) : Request.CreateResponse(HttpStatusCode.NotModified, sectionID);
            }
            catch (Exception ex)
            {

                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}
