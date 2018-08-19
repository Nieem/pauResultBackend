using PauFacultyPortal.Models;
using PauFacultyPortal.ViewModel.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
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
