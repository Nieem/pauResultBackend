using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PauFacultyPortal.ViewModel.Dashboard
{
    public class DashboardViewModel
    {
        // Teacher 
        public int AccountId { set; get; }
        public string Name { set; get; }
        public string PhoneNo { set; get; }
        public string TeleExchange { set; get; }
        public string Designation { set; get; }
        public string DepartmentSection { set; get; }
        public string Pic { set; get; }
        // public int AccountsRoleId { set; get; }

        public int SectionId { set; get; }
        public int TotalSections { set; get; } // 
        public int currentSectionCount { set; get; }
        public int completeMarksUpload { set; get; }
        public int leftMarksupload { set; get; }
        public int totalenrolled { set; get; }
        public int Currentenrolled { set; get; }
        public bool CourseAdvising { set; get; }
        public int LoginId { set; get; }
        public int TeacherId { set; get; }
        public string SectionName { set; get; }
        public int SemesterId { set; get; }
        
        public int CourseForDepartmentId { set; get; }
        public int ConfirmSubmitByFaculty { set; get; }
        public DateTime ExpireDateTime { set; get; }
        public int StudentIdentificationId { set; get; }
        public int DepartmentId { set; get; }
        public string StudentId { set; get; }
        //---------------------- semester
        public int ActiveSemester { set; get; }

        //--------------- notification  ------------------------------------
        public List<NotificationViewModel> DashboardNotifications { get; set; }
        public List<LinechartViewModel> LinechartDatas { get; set; }
        public List<BarChartViewModel> BarChartData { get; set; }

        // ----------------------  Student ----------------------------------------
        public Nullable<int> StudentInfoId { get; set; }
        public int SchoolId { get; set; }     
        public int SemesterInfoId { get; set; }
        public string SemesterAndYear { get; set; } 
        public string Password { get; set; }
        public bool DiplomaStudent { get; set; }
        public string StudentPicture { get; set; }
      //  public string Remark { get; set; }
        public bool CreditTransfer { get; set; }
        public bool BlockStudent { get; set; }
    //    public string BlockReason { get; set; }
        public Nullable<System.DateTime> BlockExpireDate { get; set; }
    //    public string EntryBy { get; set; }

        public string StudentName { get; set; }
        public string FathersName { get; set; }
        public string MothersName { get; set; }
        public string PresentAddress { get; set; }
        public string ParmanentAddress { get; set; }  
        public string MobileNo { get; set; }
        public System.DateTime DateOfBirth { get; set; }
        public string BloodGroupsId { get; set; }
        public string MaritalStatusId { get; set; }
        public string GenderId { get; set; }
        public string Nationality { get; set; }
        public string SkillInOtherfield { get; set; }
        public string EmailAddress { get; set; }
        public string PresentDistrict { get; set; }
        public string PresentPostalCode { get; set; }
        public string ParmanentDistrict { get; set; }
        public string ParmanentPostalCode { get; set; }
        public string LocalGuardianName { get; set; }
        public string LocalGuardianRelationship { get; set; }
        public string LocalGuardianAddress { get; set; }
        public string LocalGuardianContact { get; set; }

        public List<StudentAcademicInfoModel> StudentAcademicData { get; set; }
     

    }
}
