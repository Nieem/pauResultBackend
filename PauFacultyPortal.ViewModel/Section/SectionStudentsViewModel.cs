using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PauFacultyPortal.ViewModel.Section
{
    public class SectionStudentsViewModel
    {
        //[Required(ErrorMessage = "SectionID is required")]
        public int SectionID { get; set; }

        //[Required(ErrorMessage = "StudentID is required")]
        public string StudentID { get; set; }
        public string StudentName { get; set; }

        //[Required(ErrorMessage = "LetterGrade is required")]
        public string LetterGrade { get; set; }

        //[Required(ErrorMessage = "Grade is required")]
        public double Grade { get; set; }

        //[Required(ErrorMessage = "ConfirmLetterGrade is required")]
        //[Compare("LetterGrade",ErrorMessage = "LetterGrade and ConfirmLetterGrade is Mismatch")]
        public string ConfirmLetterGrade { get; set; }

        //[Required(ErrorMessage = "ConfirmGrade is required")]
        //[Compare("Grade", ErrorMessage = "Grade and ConfirmGrade is Mismatch")]
        public double ConfirmGrade { get; set; }

        public bool HighLight { get; set; }

       public bool? MarkSubmitFinal { get; set; }
        public bool? FinalTerm { get; set; }
        public DateTime? SpecialGradeuploadDeadLine { get; set; }
        public bool? ConfirmSubmitByFaculty { get; set; }


    }
}
