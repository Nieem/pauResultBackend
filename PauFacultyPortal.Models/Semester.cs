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
    
    public partial class Semester
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Semester()
        {
            this.CourseForStudentsAcademics = new HashSet<CourseForStudentsAcademic>();
            this.Sections = new HashSet<Section>();
        }
    
        public int SemesterId { get; set; }
        public string AcademicYear { get; set; }
        public string SemesterNYear { get; set; }
        public int BatchNo { get; set; }
        public bool ActiveSemester { get; set; }
        public bool MidTerm { get; set; }
        public bool FinalTerm { get; set; }
        public bool MidAdmitCard { get; set; }
        public bool FinalAdmitCard { get; set; }
        public bool CourseAdvising { get; set; }
        public bool EvaluationActivate { get; set; }
        public Nullable<int> DepartmentId { get; set; }
        public Nullable<System.DateTime> SpecialGradeuploadDeadLine { get; set; }
        public bool OnlineAdmisssionStatus { get; set; }
        public Nullable<System.DateTime> AdmissionStartDate { get; set; }
        public Nullable<System.DateTime> AdmissionEndDate { get; set; }
        public bool PublishResult { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CourseForStudentsAcademic> CourseForStudentsAcademics { get; set; }
        public virtual Department Department { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Section> Sections { get; set; }
    }
}