using PauFacultyPortal.Service.Semester;
using PauFacultyPortal.ViewModel;
using PauFacultyPortal.ViewModel.Semester;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PauFacultyPortal.Server.Controllers
{
    public class SemesterController : ApiController
    {
        SemesterService service = new SemesterService();
        public IHttpActionResult Get()
        {
            SemesterViewModel semester = new SemesterViewModel();
            ResponseModel response = new ResponseModel();
            try
            {
               int teacherID = 47;
               List<SemesterViewModel> semesters = service.GetSemesters(teacherID);

                response = new ResponseModel(semesters, true, "", null);

            }
            catch (Exception exception)
            {

                response = new ResponseModel(null, false, "Error Found", exception);
            }


            return Ok(response);
        }
    }
}
