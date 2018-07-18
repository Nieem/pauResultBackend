using PauFacultyPortal.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PauFacultyPortal.Models;

namespace PauFacultyPortal.Service
{
    public class ProfileService
    {
        private PauFacultyPortalEntities _db = new PauFacultyPortalEntities();
        public List<ProfileViewModel> GetProfileInfo(string userId)
        {

            var accountList = _db.Accounts.Where(x => x.LoginIdentity == userId).FirstOrDefault();
            var educationList = _db.AccountExtEducations.Where(x=>x.AccountId == accountList.AccountId).ToList();
            List<ProfileViewModel> list = new List<ProfileViewModel>();
            if (accountList != null)
            {    
                string userpic = "http://123.136.27.58/umsapi/api/ProfileImageTransferService?imageName=" + accountList.LoginIdentity.ToString() + ".jpg&type=1";
                ProfileViewModel model = new ProfileViewModel()
                {
                    AccountId = accountList.AccountId,
                    Name = accountList.Name,
                    Email = accountList.Email,
                    PhoneNo = accountList.PhoneNo,
                    TeleExchange = accountList.TeleExchange,
                    Designation = accountList.Designation,
                    DepartmentSection = accountList.DepartmentSection,
                    LoginIdentity = accountList.LoginIdentity,
                    Password = accountList.Password,
                    AccountsRoleId = accountList.AccountsRoleId,
                    Pic = userpic,
                    Status = accountList.Status,
                    PartimeTeacher =accountList.PartimeTeacher,
                    ExpireDate = accountList.ExpireDate,
                    Deactivate = accountList.Deactivate,
                    CauseOfDeactivate = accountList.CauseOfDeactivate,
                    TeporaryBlock = accountList.TeporaryBlock,
                    TemporaryBlockExpireDate = accountList.TemporaryBlockExpireDate,
                                      
                };


                list.Add(model);
            }
            
            return list;

        }

    }
}
