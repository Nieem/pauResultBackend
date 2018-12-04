using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PauFacultyPortal.Models;
using PauFacultyPortal.ViewModel.Dashboard;
using System.Dynamic;

namespace PauFacultyPortal.Service
{
    public class DashboardService
    {
        PauFacultyPortalEntities _db = new PauFacultyPortalEntities();

        public List<DashboardViewModel> GetTeacherProfileInfo(string userId)
        {
            List<DashboardViewModel> list = new List<DashboardViewModel>();
            if (!string.IsNullOrEmpty(userId) && (userId.Length == 6))
            {
                var accountList = _db.Accounts.Where(x => x.LoginIdentity == userId).FirstOrDefault();
                string userpic = "http://123.136.27.58/umsapi/api/ProfileImageTransferService?imageName=" + accountList.LoginIdentity.ToString() + ".jpg";

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

        public List<StudentDashBoardViewModel> GetStudentProfileInfo(string userId)
        {
            List<StudentDashBoardViewModel> studentlist = new List<StudentDashBoardViewModel>();

            if (!string.IsNullOrEmpty(userId) && (userId.Length == 9))
            {
                string userpic = "http://123.136.27.58/umsapi/api/ProfileImageTransferService?imageName=" + userId.ToString() + ".jpg&type=1";

                var students = (from s in _db.StudentIdentifications
                                where s.StudentId == userId
                                join si in _db.StudentInfoes on s.StudentId equals si.StudentId
                                join b in _db.BloodGroups on si.BloodGroupsId equals b.BloodGroupsId
                                join g in _db.Genders on si.GenderId equals g.GenderId
                                join m in _db.MaritalStatus on si.MaritalStatusId equals m.MaritalStatusId
                                join d in _db.Departments on s.DepartmentId equals d.DepartmentId
                                //StudentAttatchedFileCategories
                                select new
                                {
                                    StudentInfoId = si.StudentInfoId,
                                    StudentId = s.StudentId,
                                    StudentName = si.StudentName,
                                    FathersName = si.FathersName,
                                    MothersName = si.MothersName,
                                    PresentAddress = si.PresentAddress,
                                    ParmanentAddress = si.ParmanentAddress,
                                    PhoneNo = si.PhoneNo,
                                    MobileNo = si.MobileNo,
                                    DateOfBirth = si.DateOfBirth,
                                    //  BloodGroupsId = si.BloodGroupsId,
                                    //  MaritalStatusId = si.MaritalStatusId,
                                    //  GenderId = si.GenderId,
                                    Nationality = si.Nationality,
                                    SkillInOtherfield = si.SkillInOtherfield,
                                    LocalGuardianName = si.LocalGuardianName,
                                    LocalGuardianRelationship = si.LocalGuardianRelationship,
                                    LocalGuardianAddress = si.LocalGuardianAddress,
                                    LocalGuardianContact = si.LocalGuardianContact,
                                    EmailAddress = si.EmailAddress,
                                    PresentDistrict = si.PresentDistrict,
                                    PresentPostalCode = si.PresentPostalCode,
                                    ParmanentDistrict = si.ParmanentDistrict,
                                    ParmanentPostalCode = si.ParmanentPostalCode,
                                    StudentIdentificationId = s.StudentIdentificationId,
                                    SchoolId = s.SchoolId,
                                    DepartmentId = s.DepartmentId,
                                    DepartmentName = d.DepartmentName,
                                    SemesterInfoId = s.SemesterInfo,
                                    SemesterAndYear = s.SemesterAndYear,
                                    Password = s.Password,
                                    DiplomaStudent = s.DiplomaStudent,
                                    StudentPicture = userpic,
                                    CreditTransfer = s.CreditTransfer,
                                    SemesterId = s.SemesterId,
                                    BlockStudent = s.BlockStudent,
                                    BloodGroupsId = b.Name,
                                    GenderId = g.GenderName,
                                    MaritalStatusId = m.MaritalStat
                                }).FirstOrDefault();
                List<StudentAcademicInfoModel> academiclist = new List<StudentAcademicInfoModel>();
                var stAcademicdata = _db.StudentAcademicInfoes.Where(a => a.StudentId == userId).ToList();
                foreach (var aci in stAcademicdata)
                {
                    var stdData = new StudentAcademicInfoModel()
                    {
                        StudentId = userId,
                        StudentAcademicInfoId = aci.StudentAcademicInfoId,
                        NameOfExamination = aci.NameOfExamination,
                        StartingSession = aci.StartingSession,
                        UniversityBoard = aci.UniversityBoard,
                        PassingYear = aci.PassingYear,
                        Result = aci.Result,
                        Group = aci.Group
                    };
                    academiclist.Add(stdData);
                }

                List<StudentDocumentsViewModel> documentList = new List<StudentDocumentsViewModel>();
                var StudentDocuments = _db.DocumentAddings.Where(dc => dc.StudentId == userId);
                foreach (var document in StudentDocuments)
                {
                    var dct = new StudentDocumentsViewModel() {

                        StudentId = document.StudentId,
                        SscCertificate = document.SscCertificate,
                        SscMarkSheet = document.SscMarkSheet,
                        HscCertificate = document.HscCertificate,
                        HscMarkSheet = document.HscMarkSheet,
                        DiplomaCertificate = document.DiplomaCertificate,
                        DiplomaMarkSheet = document.DiplomaMarkSheet,
                        Gce5Olavel = document.Gce5Olavel,
                        Gce5Alavel = document.Gce5Alavel,
                        BaBsCertificate = document.BaBsCertificate,
                        BaBsMarkSheet = document.BaBsMarkSheet,
                        MaMsCertificate = document.MaMsCertificate,
                        MaMsMarkSheet = document.MaMsMarkSheet,
                        TwoLettersofRecommendation = document.TwoLettersofRecommendation,
                        TwoStampSizePhotoGraph = document.TwoStampSizePhotoGraph,
                        SscTestimonial = document.SscTestimonial,
                        HscTestimonial = document.HscTestimonial,
                    };
                }

                StudentDashBoardViewModel stdDashboard = new StudentDashBoardViewModel()
                {
                    StudentInfoId = students.StudentInfoId,
                    StudentId = students.StudentId,
                    StudentName = students.StudentName,
                    FathersName = students.FathersName,
                    MothersName = students.MothersName,
                    PresentAddress = students.PresentAddress,
                    ParmanentAddress = students.ParmanentAddress,
                    PhoneNo = students.PhoneNo,
                    MobileNo = students.MobileNo,
                    DateOfBirth = students.DateOfBirth,
                    BloodGroupsId = students.BloodGroupsId,
                    MaritalStatusId = students.MaritalStatusId,
                    GenderId = students.GenderId,
                    Nationality = students.Nationality,
                    SkillInOtherfield = students.SkillInOtherfield,
                    LocalGuardianName = students.LocalGuardianName,
                    LocalGuardianRelationship = students.LocalGuardianRelationship,
                    LocalGuardianAddress = students.LocalGuardianAddress,
                    LocalGuardianContact = students.LocalGuardianContact,
                    EmailAddress = students.EmailAddress,
                    PresentDistrict = students.PresentDistrict,
                    PresentPostalCode = students.PresentPostalCode,
                    ParmanentDistrict = students.ParmanentDistrict,
                    ParmanentPostalCode = students.ParmanentPostalCode,

                    StudentIdentificationId = students.StudentIdentificationId,
                    SchoolId = students.SchoolId,
                    DepartmentId = students.DepartmentId,
                    DepartmentName = students.DepartmentName,
                    //     SemesterInfoId = students.SemesterInfo,
                    SemesterNYear = students.SemesterAndYear,
                    Password = students.Password,
                    DiplomaStudent = students.DiplomaStudent,
                    StudentPicture = userpic,
                    CreditTransfer = students.CreditTransfer,
                    SemesterId = students.SemesterId,
                    //   BlockStudent = students.BlockStudent,
                    StudentAcademicData = academiclist,
                    StudentDocuments = documentList,
                    EarnCredit = GetStudentTotalCredit(students.StudentIdentificationId),
                    CourseComplete = GetStudentTotalCourse(students.StudentIdentificationId),
                    CGPA = GetStudentCGPA(students.StudentIdentificationId)
                };
                studentlist.Add(stdDashboard);
            }
            return studentlist;
        }

        public IQueryable<CourseForStudentsAcademic> GetAllCourseForStudentsAcademics()
        {
            return _db.CourseForStudentsAcademics.OrderBy(s => s.StudentIdentificationId);
        }
        public IQueryable<StudentIdentification> GetStudentIdentificationData(string loginID)
        {
            return _db.StudentIdentifications.Where(s => s.StudentId == loginID);
        }

        public List<StudentGradeBySemesterViewModel> GetStudentGradesBySemester(string loginId)
        {
            List<StudentGradeBySemesterViewModel> list = new List<StudentGradeBySemesterViewModel>();

            int Identity = _db.StudentIdentifications.Where(std => std.StudentId == loginId).FirstOrDefault().StudentIdentificationId;

            double cgpaCal = 0.0, totalCredit = 0.0, totalGrade = 0.0;

            int p = 0;
            string semester = String.Empty;
            var x = 0.00;
            var y = 0.00;
            var z = 0.00;
            var earnedCredit = 0.0;
            var takenCredits = 0.0;
            var semesterEarnCredit = 0.0;
            var SemesterEarnResult = 0.0;
            var semterX = 0.0;
            var semterY = 0.0;


            var GetAllCourseByStudent = _db.CourseForStudentsAcademics.OrderBy(a => a.SemesterId).Where(s => s.StudentIdentificationId == Identity);

            var semList = GetAllCourseByStudent
                    .Select(a => new { SemNYr = a.Semester.SemesterNYear, SemID = a.Semester.SemesterId }).Distinct()
                    .OrderBy(a => a.SemID).Select(a => a.SemNYr).ToList();




            foreach (var item2 in semList)
            {
                StudentGradeBySemesterViewModel model = new StudentGradeBySemesterViewModel();
                List<CourseWiseResultViewModel> courseList = new List<CourseWiseResultViewModel>();

                model.SemesterName = item2;

                semester = item2;
                semesterEarnCredit = 0.0;
                SemesterEarnResult = 0.0;
                semterX = 0.0;
                semterY = 0.0;



                foreach (var item in GetAllCourseByStudent.Where(s => s.Semester.SemesterNYear.Equals(item2)))
                {

                    CourseWiseResultViewModel crsModel = new CourseWiseResultViewModel();
                    var creditVal = item.CourseForDepartment.Credit;
                    takenCredits += creditVal;
                    if (item.CourseStatusId == 2)
                    {
                        x = x + creditVal;
                        y = y + item.TotalGrade;
                        semterX = semterX + creditVal;
                        semterY = semterY + item.TotalGrade;

                        if (item.LetterGrade != "F")
                        {
                            earnedCredit += creditVal;
                            semesterEarnCredit += creditVal;
                        }

                    }
                    else
                    {
                        semterX = semterX + 0;
                        semterY = semterY + 0;
                        x = x + 0;
                        y = y + 0;
                    }


                    SemesterEarnResult = Math.Round(semterY / semterX, 2, MidpointRounding.AwayFromZero);
                    crsModel.CourseCode = _db.CourseForDepartments
                                          .Where(crscd => crscd.CourseForDepartmentId == item.CourseForDepartmentId)
                                          .FirstOrDefault().CourseCode;
                    crsModel.CourseName = _db.CourseForDepartments
                                          .Where(crscd => crscd.CourseForDepartmentId == item.CourseForDepartmentId)
                                          .FirstOrDefault().CourseName;

                    crsModel.Credits = creditVal;

                    crsModel.Grade = item.LetterGrade;
                    crsModel.GP = item.Grade;
                    crsModel.TGP = item.TotalGrade;
                    crsModel.ECR = string.Empty;
                    crsModel.SCGPA = string.Empty;
                    crsModel.CGPA = string.Empty;
                    crsModel.Status = _db.CourseStatus
                                          .Where(aa => aa.CourseStatusId == item.CourseStatusId)
                                          .FirstOrDefault().ShortName;
                    courseList.Add(crsModel);
                    model.CourseWiseResult = courseList;

                }
                totalCredit += x;
                totalGrade += y;
                model.TotalCredits = totalCredit;

                var res = Math.Round(y / x, 2, MidpointRounding.AwayFromZero);


                z = res;
                cgpaCal = totalGrade / totalCredit;

                model.TotalTGP = totalGrade;
                model.TotalECR = semesterEarnCredit;
                model.TotalSGPA = cgpaCal;
                model.TotalCGPA = z;
                list.Add(model);

            }

            return list;
        }

        public List<StudentReportByCurriculumViewModel> GetCourselistByCuriculum(string loginId)
        {
            int studentIdentificationid = GetStudentIdentificationData(loginId).FirstOrDefault().StudentIdentificationId;
            var studentData = _db.StudentIdentifications.Where(d => d.StudentIdentificationId == studentIdentificationid).FirstOrDefault();
            var takenCourses = GetAllCourseForStudentsAcademics().Where(s => s.StudentIdentificationId == studentIdentificationid);
            var studentWiseCourseList = _db.CourseForDepartments.Where(d => d.DepartmentId == studentData.DepartmentId).OrderBy(d => d.SerializedSemesterId);

            var data = new List<StudentReportByCurriculumViewModel>();

            foreach (var courselist in studentWiseCourseList)
            {
                string semestername = _db.SerializedSemesters.Where(ss => ss.SerializedSemesterId == courselist.SerializedSemesterId).FirstOrDefault().SemesterName;
                if (takenCourses.Count(s => s.CourseForDepartmentId == courselist.CourseForDepartmentId) > 0)
                {
                    CourseForDepartment courselist1 = courselist;

                    foreach (var courseForStudentsAcademic in takenCourses.Where(s => s.CourseForDepartmentId == courselist1.CourseForDepartmentId))
                    {
                        StudentReportByCurriculumViewModel courses = new StudentReportByCurriculumViewModel();
                        courses.StudentId = studentData.StudentId;
                        // courses.CourseForDepartmentId = courselist.CourseForDepartmentId;
                        courses.CourseCode = courselist.CourseCode;
                        courses.CourseName = courselist.CourseName;
                        courses.Credit = courselist.Credit;
                        courses.Prerequisit = courselist.PrerequisiteCourse;
                        courses.Status = courseForStudentsAcademic.CourseStatu.Status;
                        courses.Grade = courseForStudentsAcademic.LetterGrade;
                        courses.SemesterNYear = courseForStudentsAcademic.Semester.SemesterNYear;
                        courses.SemesterName = semestername;
                        data.Add(courses);
                    }
                }
                else
                {
                    StudentReportByCurriculumViewModel courses = new StudentReportByCurriculumViewModel();
                    courses.StudentId = studentData.StudentId;
                    //  courses.CourseForDepartmentId = courselist.CourseForDepartmentId;
                    courses.CourseCode = courselist.CourseCode;
                    courses.CourseName = courselist.CourseName;
                    courses.Credit = courselist.Credit;
                    courses.Prerequisit = courselist.PrerequisiteCourse;
                    courses.Status = "";
                    courses.Grade = "";
                    courses.SemesterNYear = "";
                    courses.SemesterName = semestername;
                    data.Add(courses);
                }
            }
            return data;
        }


        private double GetStudentCGPA(int studentIdentificationId)
        {
            double cgpaCal = 0.0, totalCredit = 0.0, totalGrade = 0.0;

            //int p = 0;
            string semester = String.Empty;
            var x = 0.00;
            var y = 0.00;
            var z = 0.00;
            var earnedCredit = 0.0;
            var takenCredits = 0.0;
            var semesterEarnCredit = 0.0;
            var SemesterEarnResult = 0.0;
            var semterX = 0.0;
            var semterY = 0.0;


            var GetAllCourseByStudent = _db.CourseForStudentsAcademics.OrderBy(a => a.SemesterId).Where(s => s.StudentIdentificationId == studentIdentificationId);


            foreach (var item2 in GetAllCourseByStudent
                    .Select(a => new { SemNYr = a.Semester.SemesterNYear, SemID = a.Semester.SemesterId }).Distinct()
                    .OrderBy(a => a.SemID).Select(a => a.SemNYr))
            {
                semester = item2;
                semesterEarnCredit = 0.0;
                SemesterEarnResult = 0.0;
                semterX = 0.0;
                semterY = 0.0;

                foreach (var item in GetAllCourseByStudent.Where(s => s.Semester.SemesterNYear.Equals(item2)))
                {

                    var creditVal = item.CourseForDepartment.Credit;
                    takenCredits += creditVal;
                    if (item.CourseStatusId == 2)
                    {
                        x = x + creditVal;
                        y = y + item.TotalGrade;
                        semterX = semterX + creditVal;
                        semterY = semterY + item.TotalGrade;

                        if (item.LetterGrade != "F")
                        {
                            earnedCredit += creditVal;
                            semesterEarnCredit += creditVal;
                        }

                    }
                    else
                    {
                        semterX = semterX + 0;
                        semterY = semterY + 0;
                        x = x + 0;
                        y = y + 0;
                    }


                    SemesterEarnResult = Math.Round(semterY / semterX, 2, MidpointRounding.AwayFromZero);

                }
                totalCredit += x;
                totalGrade += y;
                var res = Math.Round(y / x, 2, MidpointRounding.AwayFromZero);


                z = res;
                cgpaCal = totalGrade / totalCredit;

            }


            return z;

        }

        private double GetStudentTotalCourse(int studentIdentificationId)
        {
            double result = _db.CourseForStudentsAcademics.Where(x => x.StudentIdentificationId == studentIdentificationId &&
                            x.CourseStatusId == 2 && x.LetterGrade != "F").Count();
            return result;
        }

        private double GetStudentTotalCredit(int studentIdentificationId)
        {
            var query = (from stdResult in _db.CourseForStudentsAcademics
                         join crsDep in _db.CourseForDepartments
                         on stdResult.CourseForDepartmentId equals crsDep.CourseForDepartmentId
                         where stdResult.StudentIdentificationId == studentIdentificationId &&
                         stdResult.CourseStatusId == 2 && stdResult.LetterGrade != "F"
                         select new
                         {
                             stdId = stdResult.StudentIdentificationId,
                             credit = crsDep.Credit
                         }).ToList();

            double result = query.Count() == 0 ? 0.0 : query.GroupBy(x => x.stdId)
                                .Select(res => new
                                {
                                    totalCredit = Convert.ToDouble(res.Sum(y => y.credit).ToString())
                                }).FirstOrDefault().totalCredit;
            return result;
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
