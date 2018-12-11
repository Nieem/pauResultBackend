using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PauFacultyPortal.ViewModel.Dashboard
{
    public class StudentReportByCurriculumViewModel
    {
        public int SerializedSemesterId { get; set; }
        public string SerializedSemesterName { get; set; }
        public List<StudentCurriculumListViewModel> studentCurriculumLists { get; set; }

    }
}
