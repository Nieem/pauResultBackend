using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PauFacultyPortal.ViewModel.Section
{
    public class SectionStudentsViewModel
    {
        public int SectionID { get; set; }
        public string StudentID { get; set; }
        public string StudentName { get; set; }

        public string LetterGrade { get; set; }

        public double Grade { get; set; }


        public string ConfirmLetterGrade { get; set; }

        public double ConfirmGrade { get; set; }

        public bool HighLight { get; set; }

       


    }
}
