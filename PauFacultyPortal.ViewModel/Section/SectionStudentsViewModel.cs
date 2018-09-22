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

       public bool? SpecialMarkSubmit { get; set; }
        public bool? FinalTerm { get; set; }
        public DateTime? SpecialGradeuploadDeadLine { get; set; }
        public bool? ConfirmSubmitByFaculty { get; set; }
        public DateTime? ExpireDateTime { get; set; }

        //=========================== Coursefor StudentAcademic  Added by Asma, 09-08-2018 =======================
        public int CourseForStudentsAcademicId { get; set; }
        public int StudentIdentificationId { get; set; }
        public int SemesterId { get; set; }
        public double TotalGrade { get; set; }
        public int CourseStatusId { get; set; }
        public bool isVisible { get; set; }
        //  ,[Remarks]

        public bool FinalUpdate { get; set; }

        public string CourseCode { get; set; }

        public string CourseTitle { get; set; }
        public string SemesterName { get; set; }

    }
}
