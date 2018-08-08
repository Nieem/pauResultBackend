using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PauFacultyPortal.ViewModel.Dashboard
{
    public class DashboardViewModel
    {
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
        public int StudentId { set; get; }
        //---------------------- semester
        public int ActiveSemester { set; get; }

        //--------------- notification  ------------------------------------
        public List<NotificationViewModel> DashboardNotifications { get; set; }
        public List<LinechartViewModel> LinechartDatas { get; set; }

        public List<BarChartViewModel> BarChartData { get; set; }

    }
}
