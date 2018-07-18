using PauFacultyPortal.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PauFacultyPortal.Service
{

    public class ProfileService
    {

        public List<ProfileViewModel> GetProfileInfo()
        {
            List<ProfileViewModel> list = new List<ProfileViewModel>();
            ProfileViewModel obj1 = new ProfileViewModel();
            ProfileViewModel obj2 = new ProfileViewModel();

            obj1.name = "rasel";
            obj2.name = "ali";
            list.Add(obj1);
            list.Add(obj2);

            return list;
        }

    }
}
