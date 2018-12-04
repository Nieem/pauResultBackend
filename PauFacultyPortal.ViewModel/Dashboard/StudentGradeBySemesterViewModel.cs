using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PauFacultyPortal.ViewModel.Dashboard
{
    public class StudentGradeBySemesterViewModel
    {
        public string SemesterName { get; set; }

        public double TotalCredits { get; set; }

        public double TotalTGP { get; set; }
        public double TotalECR { get; set; }
        public double TotalSGPA { get; set; }

        public double TotalCGPA { get; set; }

        public List<CourseWiseResultViewModel> CourseWiseResult { get; set; }
    }
}
