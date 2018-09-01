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
        //PauFacultyPortalEntities _db = new PauFacultyPortalEntities();


        public List<UserViewModel> GetUserList()
        {
            List<UserViewModel> list = new List<UserViewModel>();
            using (var db = new PauFacultyPortalEntities())
            {
                var result = from act in db.Accounts.Where(x => x.AccountsRoleId == 5 && x.Deactivate == false)
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
            }



            return list;
        }

        public UserViewModel CheckUserStatus(UserPasswordForgetViewModel data)
        {
            var result = UserValidation(data.Email, data.LoginIdentity);
            return result;

        }

        public int ResetUserAuth(UserPasswordResetViewModel modifiedTeacher, string loginId, string email)
        {
            int result = 0;
            using (var db = new PauFacultyPortalEntities())
            {
                var entity = (from act in db.Accounts.Where(x => x.AccountsRoleId == 5 && x.Deactivate == false &&
                           x.Email == email && x.LoginIdentity == loginId && x.Password == modifiedTeacher.OldPassword)
                              select act).ToList();
                if (entity.Count() > 0)
                {
                    entity.FirstOrDefault().Password = modifiedTeacher.NewPassword;
                    entity.FirstOrDefault().LastPsswordChange = DateTime.Now.ToString();
                    result = db.SaveChanges();
                }
            }

            return result;
        }

        //public UserViewModel CheckUserStatus(UserPasswordResetViewModel data,string loginId,string email)
        //{
        //    UserViewModel result = new UserViewModel();

        //    if (!string.IsNullOrEmpty(data.NewPassword) && !string.IsNullOrEmpty(data.OldPassword))
        //    {
        //        using (var db = new PauFacultyPortalEntities())
        //        {
        //            var user = from act in db.Accounts.Where(x => x.AccountsRoleId == 5 && x.Deactivate == false &&
        //                       x.Email == email && x.LoginIdentity == loginId && x.Password == data.OldPassword)
        //                       select act;


        //            result = new UserViewModel()
        //            {
        //                Email = user.FirstOrDefault().Email,
        //                LoginIdentity = user.FirstOrDefault().LoginIdentity,
        //                Name = user.FirstOrDefault().Name,
        //                Password = user.FirstOrDefault().Password,
        //                LoginTime = DateTime.Now.ToString()

        //            };
        //        }


        //    }
        //    return result;
        //}

        private UserViewModel UserValidation(string email, string loginId)
        {
            UserViewModel result = new UserViewModel();

            if (!string.IsNullOrEmpty(loginId) && !string.IsNullOrEmpty(email))
            {
                using (var db = new PauFacultyPortalEntities())
                {
                    var user = from act in db.Accounts.Where(x => x.AccountsRoleId == 5 && x.Deactivate == false &&
                               x.Email == email && x.LoginIdentity == loginId)
                               select act;


                    result = new UserViewModel()
                    {
                        Email = user.FirstOrDefault().Email,
                        LoginIdentity = user.FirstOrDefault().LoginIdentity,
                        Name = user.FirstOrDefault().Name,
                        Password = user.FirstOrDefault().Password,
                        LoginTime = DateTime.Now.ToString()

                    };
                }


            }


            return result;

        }

        public void SendMail(UserViewModel checkUser)
        {

            string subject = "User LoginId and Password";
            string body = "Dear " + checkUser.Name + ", " + Environment.NewLine
                           + "Your LoginId: " + checkUser.LoginIdentity + ",Password: " + checkUser.Password + Environment.NewLine + "Thanks," + Environment.NewLine + "UMS(Primeasia University)";

            string fromMail = "ums@primeasia.edu.bd";
            string emailTo = "husneara_asma@yahoo.com";

            MailMessage mail = new MailMessage();
            string host = "smtp.gmail.com";
            int port = 587;
            SmtpClient server = new SmtpClient(host, port);
            mail.From = new MailAddress(fromMail);
            mail.To.Add(emailTo);
            mail.Subject = subject;
            mail.Body = body;
            server.Credentials = new NetworkCredential("ums@primeasia.edu.bd", "primeasia123");
            server.EnableSsl = true;
            server.Send(mail);



        }

        public UserViewModel GetUserInfo(string Identity, string Password)
        {

            UserViewModel model = new UserViewModel();

            if (!string.IsNullOrEmpty(Identity) && !string.IsNullOrEmpty(Password))
            {
                using (var db = new PauFacultyPortalEntities())
                {
                    var result = (from act in db.Accounts.Where(x => (x.AccountsRoleId == 1 || x.AccountsRoleId == 5) && x.Deactivate == false && x.LoginIdentity == Identity && x.Password == Password)
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

            }


            return model;
        }



    }
}
