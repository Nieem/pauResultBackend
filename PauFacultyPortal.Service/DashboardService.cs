﻿using System;
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
                                       where ss.CourseAdvising.Equals(true)
                                       select ss).FirstOrDefault().SemesterId;

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

                var completeMarksUpld = (from mk in _db.CourseForStudentsAcademics
                                         join ss in _db.Sections on mk.SectionId equals ss.SectionId
                                         where ss.TeacherId == teacher_id && ss.SemesterId == presentSemester && mk.LetterGrade != null
                                         select new
                                         {
                                             mk.CourseForStudentsAcademicId
                                         }).Count();


                var leftMarksupload = (from mk in _db.CourseForStudentsAcademics
                                       join ss in _db.Sections on mk.SectionId equals ss.SectionId
                                       where ss.TeacherId == teacher_id && ss.SemesterId == presentSemester && mk.LetterGrade == null
                                       select new
                                       {
                                           mk.CourseForStudentsAcademicId
                                       }).Count();

                var totalenrolled = (from cs in _db.CourseForStudentsAcademics
                                     join st in _db.Sections on cs.SectionId equals st.SectionId
                                     join tc in _db.Teachers on st.TeacherId equals tc.TeacherId
                                     where tc.LoginId == userId
                                     select new
                                     {
                                         cs.CourseForStudentsAcademicId
                                     }).Count();

                var Currentenrolled = (from cs in _db.CourseForStudentsAcademics
                                       join st in _db.Sections on cs.SectionId equals st.SectionId
                                       join tc in _db.Teachers on st.TeacherId equals tc.TeacherId
                                       where tc.LoginId == userId && cs.SemesterId == presentSemester
                                       select new
                                       {
                                           cs.CourseForStudentsAcademicId
                                       }).Count();

                List<NotificationViewModel> notifications = new List<NotificationViewModel>();
                var allnotifications = _db.Notifications.ToList();

                foreach (var nt in allnotifications)
                {
                    NotificationViewModel ntmodel = new NotificationViewModel()
                    {
                        notification_id = nt.notification_id,
                        title = nt.title,
                        description = nt.description,
                        from_date = nt.from_date,
                        to_date = nt.to_date,
                        AccountsRoleId = nt.AccountsRoleId,
                        created_by = nt.created_by

                    };
                    notifications.Add(ntmodel);
                }

                List<LinechartViewModel> Linechart = new List<LinechartViewModel>();
                //var SemesterList = new List<string>();
                //var studentCount = new List<int>();

                var allchartData = (from cs in _db.CourseForStudentsAcademics
                                    join st in _db.Sections on cs.SectionId equals st.SectionId
                                    join tc in _db.Teachers on st.TeacherId equals tc.TeacherId
                                    join sm in _db.Semesters on cs.SemesterId equals sm.SemesterId
                                    where tc.LoginId == userId
                                    select new
                                    {
                                        sm.SemesterNYear,
                                    });
                var chartdatas = allchartData.GroupBy(x => new { x.SemesterNYear })
                                      .Select(res => new
                                      {
                                          SemesterNYear = res.FirstOrDefault().SemesterNYear,
                                          totalStudents = res.Count()
                                      });

                //foreach (var item in chartdatas.ToList())
                //{
                //    SemesterList.Add(item.SemesterNYear);
                //}
                //foreach (var item2 in chartdatas.ToList())
                //{
                //    studentCount.Add(item2.totalStudents);
                //}
                foreach (var item in chartdatas.ToList())
                {
                    LinechartViewModel Linechartmodel = new LinechartViewModel()
                    {
                        SemesterNYear = item.SemesterNYear,
                        totalStudents = item.totalStudents
                    };
                    Linechart.Add(Linechartmodel);
                }




                List<BarChartViewModel> barChartList = new List<BarChartViewModel>();
                //var CourseSemesterList = new List<string>();
                //var CourseCountList = new List<int>();

                var barChart = (from sec in _db.Sections
                                join crd in _db.CourseForDepartments on sec.CourseForDepartmentId equals crd.CourseForDepartmentId
                                join sem in _db.Semesters on sec.SemesterId equals sem.SemesterId
                                join tc in _db.Teachers on sec.TeacherId equals tc.TeacherId
                                where tc.LoginId == userId
                                select new
                                {
                                    sem.SemesterNYear,
                                });
                var barChartData = barChart.GroupBy(x => new { x.SemesterNYear })
                                     .Select(res => new
                                     {
                                         SemesterNYear = res.FirstOrDefault().SemesterNYear,
                                         totalCourse = res.Count()
                                     });


                //foreach (var item in barChartData.ToList())
                //{
                //    CourseSemesterList.Add(item.SemesterNYear);
                //}
                //foreach (var item2 in barChartData.ToList())
                //{
                //    CourseCountList.Add(item2.totalCourse);
                //}

                foreach (var item in barChartData.ToList())
                {
                    var barChartModel = new BarChartViewModel()
                    {
                        SemesterNYear = item.SemesterNYear,
                        totalCourse = item.totalCourse
                    };

                    barChartList.Add(barChartModel);
                }




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
                    totalenrolled = totalenrolled,
                    Currentenrolled = Currentenrolled,
                    DashboardNotifications = notifications,
                    LinechartDatas = Linechart,
                    BarChartData = barChartList
                };

                list.Add(model);
            }
            return list;
        }

        public List<BarChartViewModel> GetBarChartData(string loginId, bool barChartStatus)
        {

            List<BarChartViewModel> barChartList = new List<BarChartViewModel>();
            if (barChartStatus)
            {

                var barChart = (from sec in _db.Sections
                                join crd in _db.CourseForDepartments on sec.CourseForDepartmentId equals crd.CourseForDepartmentId
                                join sem in _db.Semesters on sec.SemesterId equals sem.SemesterId
                                join tc in _db.Teachers on sec.TeacherId equals tc.TeacherId
                                where tc.LoginId == loginId
                                select new
                                {
                                    sem.SemesterNYear,
                                });
                var barChartData = barChart.GroupBy(x => new { x.SemesterNYear })
                                     .Select(res => new
                                     {
                                         SemesterNYear = res.FirstOrDefault().SemesterNYear,
                                         totalCourse = res.Count()
                                     });


                foreach (var item in barChartData.ToList())
                {
                    var barChartModel = new BarChartViewModel()
                    {
                        SemesterNYear = item.SemesterNYear,
                        totalCourse = item.totalCourse
                    };

                    barChartList.Add(barChartModel);
                }



            }

            return barChartList;



        }


        public List<LinechartViewModel> GetLineChartData(string loginId, bool lineChart)
        {

            List<LinechartViewModel> LinechartList = new List<LinechartViewModel>();
            if (lineChart)
            {
                
                    var allchartData = (from cs in _db.CourseForStudentsAcademics
                                        join st in _db.Sections on cs.SectionId equals st.SectionId
                                        join tc in _db.Teachers on st.TeacherId equals tc.TeacherId
                                        join sm in _db.Semesters on cs.SemesterId equals sm.SemesterId
                                        where tc.LoginId == loginId
                                        select new
                                        {
                                            sm.SemesterNYear,
                                        });
                    var chartdatas = allchartData.GroupBy(x => new { x.SemesterNYear })
                                          .Select(res => new
                                          {
                                              SemesterNYear = res.FirstOrDefault().SemesterNYear,
                                              totalStudents = res.Count()
                                          });

                    foreach (var item in chartdatas.ToList())
                    {
                        LinechartViewModel Linechartmodel = new LinechartViewModel()
                        {
                            SemesterNYear = item.SemesterNYear,
                            totalStudents = item.totalStudents
                        };
                        LinechartList.Add(Linechartmodel);
                    }
                


            }
            return LinechartList;



        }
    }
}

//----------------------------------------
//-------------------------------------------
//   SELECT COUNT(sm.SemesterNYear) as total, SemesterNYear FROM[UmsDb2_4_0_055].[dbo].[CourseForStudentsAcademics] as cs
//join[UmsDb2_4_0_055].[dbo].[Sections] as s on s.SectionId = cs.SectionId
//join[UmsDb2_4_0_055].[dbo].[Teachers] as t on s.TeacherId = t.TeacherId
//join[UmsDb2_4_0_055].[dbo].[Semesters] as sm on cs.SemesterId = sm.SemesterId
//where t.LoginId = 140055
//group by sm.SemesterNYear
//-------------------------------------------              
//foreach (var crt in allchartData)
//{
//    LinechartViewModel linchart = new LinechartViewModel()
//    {
//    };
//    allchartData.Add(linchart);
//}