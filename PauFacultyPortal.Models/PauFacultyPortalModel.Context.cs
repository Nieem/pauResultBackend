﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class PauFacultyPortalEntities : DbContext
    {
        public PauFacultyPortalEntities()
            : base("name=PauFacultyPortalEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<AccountExtEducation> AccountExtEducations { get; set; }
        public virtual DbSet<AccountExtProfessionalActivity> AccountExtProfessionalActivities { get; set; }
        public virtual DbSet<AccountExtProject> AccountExtProjects { get; set; }
        public virtual DbSet<AccountExtPublication> AccountExtPublications { get; set; }
        public virtual DbSet<AccountMetaInformation> AccountMetaInformations { get; set; }
        public virtual DbSet<AccountMetaProfessional> AccountMetaProfessionals { get; set; }
        public virtual DbSet<AccountReferenceLinktable> AccountReferenceLinktables { get; set; }
        public virtual DbSet<CourseForStudentsAcademic> CourseForStudentsAcademics { get; set; }
        public virtual DbSet<CourseStatu> CourseStatus { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<GradingSystem> GradingSystems { get; set; }
        public virtual DbSet<Notification> Notifications { get; set; }
        public virtual DbSet<School> Schools { get; set; }
        public virtual DbSet<Section> Sections { get; set; }
        public virtual DbSet<SemesterInfo> SemesterInfoes { get; set; }
        public virtual DbSet<Semester> Semesters { get; set; }
        public virtual DbSet<StudentInfo> StudentInfoes { get; set; }
        public virtual DbSet<TeacherDesignation> TeacherDesignations { get; set; }
        public virtual DbSet<Teacher> Teachers { get; set; }
        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<CourseForDepartment> CourseForDepartments { get; set; }
        public virtual DbSet<StudentIdentification> StudentIdentifications { get; set; }
    }
}