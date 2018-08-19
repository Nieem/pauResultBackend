using PauFacultyPortal.Service;
using PauFacultyPortal.ViewModel;
using PauFacultyPortal.ViewModel.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;

namespace PauFacultyPortal.Server.Controllers
{
    public class ProfileController : ApiController
    {
        ProfileService service = new ProfileService();

        [HttpGet]
        public List<ProfileViewModel> Get()
        {
            //ResponseModel response = new ResponseModel();
            //try
            //{
            var loginId = ((ClaimsIdentity)User.Identity).FindFirst("LoginID").Value;
            List<ProfileViewModel> models = service.GetProfileInfo(loginId);
            // response = new ResponseModel(models, true, "", null);

            //}
            //catch (Exception exception)
            //{

            //    response = new ResponseModel(null, false, "Error Found", exception);
            //}


            //return Ok(response);
            return models;
        }
    }
}
