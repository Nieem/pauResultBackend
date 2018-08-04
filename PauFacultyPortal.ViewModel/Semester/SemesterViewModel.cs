using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PauFacultyPortal.ViewModel.Semester
{
    public class SemesterViewModel
    {
        public int SemesterID { get; set; }
        public string SemesterName { get; set; }

        public bool ActiveSemester { get; set; }
    }
}
