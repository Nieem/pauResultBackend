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
        public HttpResponseMessage Get()
        {
            try
            {
                var loginId = ((ClaimsIdentity)User.Identity).FindFirst("LoginID").Value;
                List<ProfileViewModel> models = service.GetProfileInfo(loginId);
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
