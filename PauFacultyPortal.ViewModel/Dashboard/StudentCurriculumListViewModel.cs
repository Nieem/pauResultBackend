using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PauFacultyPortal.ViewModel.Dashboard
{
    public class StudentCurriculumListViewModel
    {
        public string StudentId { get; set; }
        public int StudentIdentificationId { set; get; }
        public string SemesterNYear { get; set; }

        public string SemesterName { get; set; }
        public string CourseCode { get; set; }
        public string CourseName { get; set; }
        public double Credit { get; set; }
        public string Prerequisit { get; set; }
        public string Status { get; set; }
        public string Grade { get; set; }
        public double CGPA { get; set; }
    }
}
