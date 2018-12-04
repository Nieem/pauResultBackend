using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PauFacultyPortal.ViewModel.Dashboard
{
    public class StudentDashBoardViewModel
    {

        // ----------------------  Student ----------------------------------------
        public Nullable<int> StudentInfoId { get; set; }
      
        public string StudentId { get; set; }
        public int StudentIdentificationId { set; get; }
        //studentIdentificationid
        public int SchoolId { get; set; }
      //  public int SemesterInfoId { get; set; }
        public string SemesterNYear { get; set; }
        public int SerializedSemesterId { get; set; }
        public string SemesterName { get; set; }
        public string Password { get; set; }
        public bool DiplomaStudent { get; set; }
        public string StudentPicture { get; set; }
        public bool CreditTransfer { get; set; }
        public bool BlockStudent { get; set; }
        public Nullable<System.DateTime> BlockExpireDate { get; set; }

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
        public string PhoneNo { set; get; }
        public int DepartmentId { set; get; }
        public string DepartmentName { get; set; }
        public int SemesterId { set; get; }

        public double EarnCredit { get; set; }
        public double CourseComplete { get; set; }
        public double CGPA { get; set; }

        public List<StudentAcademicInfoModel> StudentAcademicData { get; set; }

        public List<StudentDocumentsViewModel> StudentDocuments { get; set; }
    }
}
