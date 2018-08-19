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
        public List<SemesterViewModel> Get()
        {
            SemesterViewModel semester = new SemesterViewModel();
            ResponseModel response = new ResponseModel();
            List<SemesterViewModel> semesters = new List<SemesterViewModel>();
            try
            {

                var loginId = ((ClaimsIdentity)User.Identity).FindFirst("LoginID").Value;
                semesters = service.GetSemesters(loginId);

                //response = new ResponseModel(semesters, true, "", null);

            }
            catch (Exception exception)
            {

                response.Exception = exception;
                //response = new ResponseModel(null, false, "Error Found", exception);
            }


            return semesters;
        }
    }
}
