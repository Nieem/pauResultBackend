using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PauFacultyPortal.Models;
using PauFacultyPortal.ViewModel.Dashboard;


namespace PauFacultyPortal.Service
{
    public class DashboardService
    {
        PauFacultyPortalEntities _db = new PauFacultyPortalEntities();
        public List<DashboardViewModel> GetDashboardProfileInfo(string userId)
        {
            List<DashboardViewModel> list = new List<DashboardViewModel>();
            if (!string.IsNullOrEmpty(userId))
            {
                var accountList = _db.Accounts.Where(x => x.LoginIdentity == userId).FirstOrDefault();
                string userpic = "http://123.136.27.58/umsapi/api/ProfileImageTransferService?imageName=" + accountList.LoginIdentity.ToString() + ".jpg&type=1";

                  int presentSemester = (from ss in _db.Semesters
                                          where ss.CourseAdvising.Equals(true) select ss).FirstOrDefault().SemesterId;

                   // int presentSemester = 46;
                   int teacher_id = (from th in _db.Teachers where th.LoginId == userId select th).FirstOrDefault().TeacherId;

                var currentSectns = (from sc in _db.Sections
                                     join sm in _db.Semesters on sc.SemesterId equals sm.SemesterId
                                     join t in _db.Teachers on sc.TeacherId equals t.TeacherId
                                  
                                     where t.TeacherId == teacher_id && sm.CourseAdvising == true && sc.SemesterId == presentSemester
                                     && t.LoginId == userId
                                     select sc.SectionId).ToArray();
                                       
                var currentSectnCount = currentSectns.Count();

                var Totalsectioncount = (from st in _db.Sections
                                         join tc in _db.Teachers on st.TeacherId equals tc.TeacherId
                                         where tc.LoginId == userId
                                         select new
                                         {
                                             st.SectionId
                                         }).Count();

             //   List<int> enrolled = new List<int>();    
              string  currentSectionArray = string.Join(",", currentSectns.ToArray());

                // var result = from x in collection where new [] {1,2,3}.Contains(x) select x;
                //var result = from x in _db.CourseForStudentsAcademics where currentSectns.Contains(x.SectionId) select x;
                //dataSource.StateList.Where(s => countryCodes.Contains(s.CountryCode))

                var result = from x in _db.CourseForStudentsAcademics.Where(s => currentSectionArray.Contains(s.SectionId.ToString())) select x;  // where currentSectns.Contains(x.SectionId) select x;

                var completeMarksUpld = (from mk in _db.CourseForStudentsAcademics
                                         join ss in _db.Sections on mk.SectionId equals ss.SectionId                                        
                                           where ss.TeacherId == teacher_id && ss.SemesterId == presentSemester && mk.LetterGrade != null
                                           select new
                                           {
                                               mk.CourseForStudentsAcademicId
                                           }).Count();

                // //SectionId(x => x.SectionId).Except(currentSectns.Select(x =>
                var leftMarksupload = (from mk in _db.CourseForStudentsAcademics
                                       join ss in _db.Sections on mk.SectionId equals ss.SectionId                                      
                                       where ss.TeacherId == teacher_id && ss.SemesterId == presentSemester && mk.LetterGrade == null
                                       select new
                                       {
                                           mk.CourseForStudentsAcademicId
                                       }).Count();

                var totalenrolled = (from std in _db.StudentIdentifications
                                     where std.SemesterId == presentSemester 
                                     // && std.DepartmentId == 1
                                     select new
                                     {
                                         std.StudentIdentificationId
                                     }).Count();

                DashboardViewModel model = new DashboardViewModel()
                    {
                        AccountId = accountList.AccountId,
                        Name = accountList.Name,
              
                        PhoneNo = accountList.PhoneNo,
                        TeleExchange = accountList.TeleExchange,
                        Designation = accountList.Designation,
                        DepartmentSection = accountList.DepartmentSection,
                        Pic = userpic,
                        TotalSections = Totalsectioncount,
                        currentSectionCount = currentSectnCount,
                        completeMarksUpload = completeMarksUpld,
                        leftMarksupload = leftMarksupload,
                        totalenrolled = totalenrolled
                };

                list.Add(model);
            }
                return list; 
        }
    }
}   
