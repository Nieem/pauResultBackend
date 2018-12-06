using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PauFacultyPortal.ViewModel.Dashboard
{
    public class StudentGradeBySemesterViewModel
    {
        public List<CourseWiseResultHeaderViewModel> courseWiseResultHeaders { get; set; }
        public List<CourseWiseResultViewModel> CourseWiseResult { get; set; }
    }
}
