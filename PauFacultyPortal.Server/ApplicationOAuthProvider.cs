using Microsoft.Owin.Security.OAuth;
using PauFacultyPortal.Service.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace PauFacultyPortal.Server
{
    public class ApplicationOAuthProvider : OAuthAuthorizationServerProvider
    {

        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {

            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            AuthService service = new AuthService();
            var user = service.GetUserInfo(context.UserName,context.Password);
            if (user != null)
            {
                if (!string.IsNullOrEmpty(user.LoginIdentity) && !string.IsNullOrEmpty(user.Password))
                {
                    var identity = new ClaimsIdentity(context.Options.AuthenticationType);
                    identity.AddClaim(new Claim("LoginID",user.LoginIdentity));
                    identity.AddClaim(new Claim("Email",user.Email));
                    identity.AddClaim(new Claim("Password",user.Password));
                    identity.AddClaim(new Claim("Name",user.Name));
                    identity.AddClaim(new Claim("LoginTime",DateTime.Now.ToString()));
                    context.Validated(identity);

                }
                else
                {
                    context.SetError("invalid_grant", "Provided username and password is incorrect");
                    context.Rejected();
                }

            }
            else
            {
                context.SetError("invalid_grant", "Provided username and password is incorrect");
                context.Rejected();
            }
        }
    }
}