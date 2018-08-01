using PauFacultyPortal.Service;
using PauFacultyPortal.ViewModel;
using PauFacultyPortal.ViewModel.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PauFacultyPortal.Server.Controllers
{
    public class ProfileController : ApiController
    {
        ProfileService service = new ProfileService();

        [HttpGet]
        public IHttpActionResult Get(string LoginId)
         {
            ProfileViewModel profile = new ProfileViewModel();
            ResponseModel response = new ResponseModel();
            try
            {
                 profile = service.GetProfileInfo(LoginId);

                response = new ResponseModel(profile, true, "", null);
               // HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, models);

            }
            catch (Exception exception)
            {

                response = new ResponseModel(null, false, "Error Found", exception);
            }


            return Ok(response);
        }
    }
}
