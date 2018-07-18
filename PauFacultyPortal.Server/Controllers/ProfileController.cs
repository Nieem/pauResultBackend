using PauFacultyPortal.Service;
using PauFacultyPortal.ViewModel;
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
            ResponseModel response = new ResponseModel();
            try
            {
                List<ProfileViewModel> models = service.GetProfileInfo(LoginId);
                response = new ResponseModel(models, true, "", null);

            }
            catch (Exception exception)
            {

                response = new ResponseModel(null, false, "Error Found", exception);
            }


            return Ok(response);
        }
    }
}
