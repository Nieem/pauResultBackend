using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PauFacultyPortal.ViewModel.Auth
{
    public class UserPasswordResetViewModel
    {
        public string LoginIdentity { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

    }
}
