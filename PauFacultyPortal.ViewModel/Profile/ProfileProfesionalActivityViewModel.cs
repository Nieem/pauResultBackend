using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PauFacultyPortal.ViewModel.Profile
{
    public class ProfileProfesionalActivityViewModel
    {
        public int AccountExtProfessionalActivityId { set; get; }
        public string AttendType { set; get; }
        public string DateOfActivity { set; get; }
        public string Location { set; get; }
        public string Description { get; set; }
    }
}
