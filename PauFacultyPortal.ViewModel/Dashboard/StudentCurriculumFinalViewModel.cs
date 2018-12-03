using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PauFacultyPortal.ViewModel.Dashboard
{
   public class StudentCurriculumFinalViewModel
    {
        public string SemesterName { get; set; }
        public List<StudentReportByCurriculumViewModel> courseList { get; set; }
    }
}
