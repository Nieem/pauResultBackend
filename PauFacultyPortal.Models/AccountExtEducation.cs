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
    
    public partial class AccountExtEducation
    {
        public int AccountExtEducationId { get; set; }
        public int AccountMetaProfessionalId { get; set; }
        public string NameOfExamination { get; set; }
        public string StartingSession { get; set; }
        public string UniversityBoard { get; set; }
        public string PassingYear { get; set; }
        public string Result { get; set; }
        public string SubjectStudied { get; set; }
        public Nullable<System.DateTime> EntryDate { get; set; }
        public string EntryBy { get; set; }
        public Nullable<int> AccountId { get; set; }
    
        public virtual AccountMetaProfessional AccountMetaProfessional { get; set; }
    }
}