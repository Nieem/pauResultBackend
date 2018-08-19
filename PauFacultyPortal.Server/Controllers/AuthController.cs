using PauFacultyPortal.ViewModel.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;

namespace PauFacultyPortal.Server.Controllers
{
    public class AuthController : ApiController
    {

        [HttpGet]       
        public UserViewModel GetLoginUserInfo()
        {

            var ClaimsClass = (ClaimsIdentity)User.Identity;
            IEnumerable<Claim> items = ClaimsClass.Claims;
            UserViewModel model = new UserViewModel()
            {
                Name = ClaimsClass.FindFirst("Name").Value,
                LoginIdentity = ClaimsClass.FindFirst("LoginID").Value,
                Password = ClaimsClass.FindFirst("Password").Value,
                Email = ClaimsClass.FindFirst("Email").Value,
                LoginTime = ClaimsClass.FindFirst("LoginTime").Value,
            };

            return model;
        }
    }
}
