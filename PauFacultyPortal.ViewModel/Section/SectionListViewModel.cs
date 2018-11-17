using PauFacultyPortal.ViewModel.Semester;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PauFacultyPortal.ViewModel.Section
{
    public class SectionListViewModel
    {
        public int SectionCode { get; set; }
        public string SectionName { get; set; }
        public string CourseCode { get; set; }

        public string CourseTitle { get; set; }
        public string SemesterName { get; set; }
        public int TotalStudentEnrolled { get; set; }

        public double TotalAttendanceMark { get; set; }
        public double TotalClassTestMark { get; set; }
        public double TotalMidMark { get; set; }
        public double TotalFinalMark { get; set; }

    }
}
