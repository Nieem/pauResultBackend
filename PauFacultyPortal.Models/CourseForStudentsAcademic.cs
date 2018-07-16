//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PauFacultyPortal.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class CourseForStudentsAcademic
    {
        public int CourseForStudentsAcademicId { get; set; }
        public int StudentIdentificationId { get; set; }
        public int CourseForDepartmentId { get; set; }
        public int SemesterId { get; set; }
        public double Attendance { get; set; }
        public double ClassTest { get; set; }
        public double Midterm { get; set; }
        public double FinalTerm { get; set; }
        public double TotalMark { get; set; }
        public double Grade { get; set; }
        public double TotalGrade { get; set; }
        public string LetterGrade { get; set; }
        public int CourseStatusId { get; set; }
        public Nullable<int> PreviousCourseStatus { get; set; }
        public System.DateTime AddedDate { get; set; }
        public Nullable<int> SectionId { get; set; }
        public Nullable<int> PaymentRegistrationId { get; set; }
        public Nullable<int> GradingSystemId { get; set; }
        public Nullable<int> ReferenceCourseId { get; set; }
        public bool RetakeAdv { get; set; }
        public bool ImpAdv { get; set; }
        public bool EvaluationComplete { get; set; }
        public bool DeclareMajor { get; set; }
        public bool WithHelded { get; set; }
        public bool ReportedCase { get; set; }
        public string Remarks { get; set; }
        public string ScrutinizedBy { get; set; }
        public bool FirstScrutinized { get; set; }
        public bool SecondScrutinized { get; set; }
        public bool SuspendedForProbation { get; set; }
    
        public virtual CourseForDepartment CourseForDepartment { get; set; }
        public virtual CourseStatu CourseStatu { get; set; }
        public virtual GradingSystem GradingSystem { get; set; }
        public virtual Semester Semester { get; set; }
        public virtual StudentIdentification StudentIdentification { get; set; }
    }
}
