using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PauFacultyPortal.ViewModel.Auth
{
    public class UserPasswordResetViewModel
    {
        public string LoginIdentity { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        
        [Compare("Password", ErrorMessage = "Password and ConfirmPassword is Mismatch")]
        public string ConfirmPassword { get; set; }

    }
}
