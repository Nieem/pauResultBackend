using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PauFacultyPortal.ViewModel.Dashboard
{
    public class StudentAcademicInfoModel
    {
        public int StudentAcademicInfoId { get; set; }
        public string StudentId { get; set; }
        public string NameOfExamination { get; set; }
        public string StartingSession { get; set; }
        public string UniversityBoard { get; set; }
        public string PassingYear { get; set; }
        public string Result { get; set; }
        public string Group { get; set; }
    }
}
