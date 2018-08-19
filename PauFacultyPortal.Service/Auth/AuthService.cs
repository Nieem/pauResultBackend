using PauFacultyPortal.Models;
using PauFacultyPortal.ViewModel.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace PauFacultyPortal.Service.Auth
{
    public class AuthService
    {
        PauFacultyPortalEntities _db = new PauFacultyPortalEntities();

        public List<UserViewModel> GetUserList()
        {
            List<UserViewModel> list = new List<UserViewModel>();

            var result = from act in _db.Accounts.Where(x => x.AccountsRoleId == 1 && x.AccountsRoleId == 5 && x.Deactivate == false)
                         select new
                         {
                             LoginIdentity = act.LoginIdentity,
                             Password = act.Password,
                             Name = act.Name,
                             Email = act.Email
                         };

            foreach (var item in result.ToList())
            {

                var model = new UserViewModel()
                {
                    LoginIdentity = item.LoginIdentity,
                    Password = item.Password,
                    Name = item.Name,
                    Email = item.Email

                };

                list.Add(model);
            }



            return list;
        }

        public UserViewModel CheckUserStatus(UserPasswordForgetViewModel data)
        {
            UserViewModel result = new UserViewModel();
            if (data != null && !string.IsNullOrEmpty(data.Email))
            {
                var user = from act in _db.Accounts.Where(x => x.AccountsRoleId == 5 && x.Deactivate == false &&
                           x.Email == data.Email && x.LoginIdentity == data.LoginIdentity)
                           select act;


                result = new UserViewModel()
                {
                    Email = user.FirstOrDefault().Email,
                    LoginIdentity= user.FirstOrDefault().LoginIdentity,
                    Name = user.FirstOrDefault().Name,
                    Password= user.FirstOrDefault().Password,
                    LoginTime= DateTime.Now.ToString()

                };
            }
            return result;
        }

        public void SendMail(UserViewModel checkUser)
        {
          
            string subject = "User LoginId and Password";
            string body = "Dear " + checkUser.Name + ", " + Environment.NewLine
                           + "Your LoginId: " + checkUser.LoginIdentity + ",Password: " + checkUser.Password + Environment.NewLine + "Thanks," + Environment.NewLine + "UMS(Primeasia University)";
                           
            string fromMail = "sydul.hassan@primeasia.edu.bd";
            string emailTo = "mamun.ruet@primeasia.edu.bd";

            MailMessage mail = new MailMessage();
            string host = "smtp.gmail.com";
            int port = 587;
            SmtpClient server = new SmtpClient(host, port);
            mail.From = new MailAddress(fromMail);
            mail.To.Add(emailTo);
            mail.Subject = subject;
            mail.Body = body;
            server.Credentials = new NetworkCredential("sydul.hassan@primeasia.edu.bd","30091991shr");
            server.EnableSsl = true;
            server.Send(mail);
           

          
        }

        public UserViewModel GetUserInfo(string Identity, string Password)
        {

            UserViewModel model = new UserViewModel();
            if (!string.IsNullOrEmpty(Identity) && !string.IsNullOrEmpty(Password))
            {
                var result = (from act in _db.Accounts.Where(x => (x.AccountsRoleId == 1 || x.AccountsRoleId == 5) && x.Deactivate == false && x.LoginIdentity == Identity && x.Password == Password)
                              select new
                              {
                                  LoginIdentity = act.LoginIdentity,
                                  Password = act.Password,
                                  Name = act.Name,
                                  Email = act.Email,


                              }).FirstOrDefault();

                model = new UserViewModel()
                {
                    LoginIdentity = result.LoginIdentity,
                    Password = result.Password,
                    Name = result.Name,
                    Email = result.Email

                };


            }


            return model;
        }



    }
}
