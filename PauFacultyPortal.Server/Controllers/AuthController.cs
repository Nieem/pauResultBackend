using PauFacultyPortal.Service.Auth;
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
        public HttpResponseMessage GetLoginUserInfo()
        {
            try
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
                    UserType = ClaimsClass.FindFirst("UserType").Value
                };
                return model != null ? Request.CreateResponse(HttpStatusCode.OK, model) : Request.CreateErrorResponse(HttpStatusCode.NotFound,
                    "User does not exist");
            }
            catch (Exception ex)
            {

                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }

        }

        [HttpPost]
        [AllowAnonymous]
        public HttpResponseMessage SendMail([FromBody]UserPasswordForgetViewModel data)
        {
            try
            {
                AuthService service = new AuthService();
                UserViewModel checkUser = service.CheckUserStatus(data);
                if (checkUser.Email != null && (checkUser.Email == data.Email))
                {
                    service.SendMail(checkUser);
                    return Request.CreateResponse(HttpStatusCode.OK, checkUser);
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
        public HttpResponseMessage ResetUserPassword([FromBody] UserPasswordResetViewModel data)
        {
            try
            {
                AuthService service = new AuthService();
                var loginId = ((ClaimsIdentity)User.Identity).FindFirst("LoginID").Value;
                var email = ((ClaimsIdentity)User.Identity).FindFirst("Email").Value;
                var loginPassword = ((ClaimsIdentity)User.Identity).FindFirst("Password").Value;
                bool checkUser = data.OldPassword == loginPassword ? true : false;
                if (checkUser)
                {
                    int result = service.ResetUserAuth(data, loginId, email);
                    return result > 0 ? Request.CreateResponse(HttpStatusCode.OK, "Success") : Request.CreateResponse(HttpStatusCode.NotModified, data);
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
