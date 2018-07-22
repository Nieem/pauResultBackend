using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PauFacultyPortal.ViewModel.Profile
{
    public class ProfileEducationViewModel
    {
        public int AccountExtEducationId { get; set; }
        public string NameOfExamination { set; get; }
        public string StartingSession { set; get; }

        public string UniversityBoard { set; get; }
        public string PassingYear { set; get; }
        public string Result { set; get; }
        public string SubjectStudied { set; get; }
    }
}
