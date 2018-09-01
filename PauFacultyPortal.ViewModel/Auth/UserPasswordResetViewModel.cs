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
   

        public string OldPassword { get; set; }

       
        public string NewPassword { get; set; }

    }
}
