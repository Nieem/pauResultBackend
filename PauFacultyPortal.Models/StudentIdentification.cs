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
    
    public partial class StudentIdentification
    {
        public int StudentIdentificationId { get; set; }
        public int SchoolId { get; set; }
        public int DepartmentId { get; set; }
        public int SemesterInfoId { get; set; }
        public string StudentId { get; set; }
        public string SemesterAndYear { get; set; }
        public bool Validation { get; set; }
        public System.DateTime AddedDate { get; set; }
        public Nullable<int> StudentInfoId { get; set; }
        public string Password { get; set; }
        public bool DiplomaStudent { get; set; }
        public string StudentPicture { get; set; }
        public string Remark { get; set; }
        public bool CreditTransfer { get; set; }
        public int SemesterId { get; set; }
        public bool BlockStudent { get; set; }
        public string BlockReason { get; set; }
        public Nullable<System.DateTime> BlockExpireDate { get; set; }
        public string EntryBy { get; set; }
        public string LastPsswordChange { get; set; }
        public bool SuspendedByProbation { get; set; }
        public int StudentGroupId { get; set; }
        public string StudentGuid { get; set; }
        public string AcademicRecordFile { get; set; }
    
        public virtual School School { get; set; }
        public virtual SemesterInfo SemesterInfo { get; set; }
    }
}
