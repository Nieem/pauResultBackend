using PauFacultyPortal.Models;
using PauFacultyPortal.ViewModel.Section;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PauFacultyPortal.Service.Section
{
    public class SectionService
    {
        PauFacultyPortalEntities _db = new PauFacultyPortalEntities();
        public List<SectionListViewModel> GetSections(int SemesterCode, string userId)
        {
            List<SectionListViewModel> modelList = new List<SectionListViewModel>();
            if ((!string.IsNullOrEmpty(userId)) && SemesterCode > 0)
            {

                var accountList = _db.Accounts.Where(x => x.LoginIdentity == userId).FirstOrDefault();
                var teacherInfo = _db.Teachers.Where(x => x.LoginId == accountList.LoginIdentity).FirstOrDefault();
                int teacherSectionCount = teacherInfo == null ? 0 : _db.Sections.Where(x => x.TeacherId == teacherInfo.TeacherId).Count();

                if (teacherSectionCount > 0)
                {
                    var result = (from sec in _db.Sections
                                  join sem in _db.Semesters
                                  on sec.SemesterId equals sem.SemesterId
                                  join crsDept in _db.CourseForDepartments
                                  on sec.CourseForDepartmentId equals crsDept.CourseForDepartmentId
                                  join crsAcademic in _db.CourseForStudentsAcademics
                                  on sec.SectionId equals crsAcademic.SectionId
                                  where sem.SemesterId == crsAcademic.SemesterId &&
                                  crsDept.CourseForDepartmentId == crsAcademic.CourseForDepartmentId &&
                                  sec.TeacherId == teacherInfo.TeacherId && sem.SemesterId == SemesterCode
                                  select new
                                  {
                                      SectionID = sec.SectionId,
                                      SectionName = sec.SectionName,
                                      CourseCode = crsDept.CourseCode,
                                      CourseName = crsDept.CourseName,
                                      SemesterCode = SemesterCode,
                                      SemesterName = sem.SemesterNYear,
                                      StudentID = crsAcademic.StudentIdentificationId

                                  });

                    var query = result.GroupBy(x => new { x.SectionID, x.SectionName, x.CourseCode, x.CourseName, x.SemesterCode, x.SemesterName })
                                       .Select(res => new
                                       {
                                           SectionID = res.FirstOrDefault().SectionID,
                                           SectionName = res.FirstOrDefault().SectionName,
                                           CourseCode = res.FirstOrDefault().CourseCode,
                                           CourseName = res.FirstOrDefault().CourseName,
                                           SemesterCode = res.FirstOrDefault().SemesterCode,
                                           SemesterName = res.FirstOrDefault().SemesterName,
                                           EnrollStudent = res.Count()
                                       });


                    //var orderByQuery = query.OrderByDescending(x => x.SemesterCode);

                    foreach (var item in query.ToList())
                    {
                        var model = new SectionListViewModel()
                        {
                            SectionCode = item.SectionID,
                            SectionName = item.SectionName,
                            CourseCode = item.CourseCode,
                            CourseTitle = item.CourseName,
                            SemesterName = item.SemesterName,
                            TotalStudentEnrolled = item.EnrollStudent
                        };

                        modelList.Add(model);
                    }
                }
            }
            return modelList;
        }

        // change date: 27 aug 2018 AA
        public bool UpdateStuentResult(SectionStudentsViewModel student)
        {

            var studentIdentificationID = _db.StudentIdentifications
                 .Where(x => x.StudentId == student.StudentID).FirstOrDefault().StudentIdentificationId;

            var entity = _db.CourseForStudentsAcademics
                .Where(x => x.SectionId == student.SectionID && x.StudentIdentificationId == studentIdentificationID).FirstOrDefault();

            var entitySection = _db.Sections
                .Where(x => x.SectionId == student.SectionID).FirstOrDefault();

            var courseCreditdata = _db.CourseForDepartments
                .Where(x => x.CourseForDepartmentId == entity.CourseForDepartmentId).FirstOrDefault();
            double courseCredit = courseCreditdata.Credit;

            double Gradepoint = GradePointCalculatorByGrade(student.LetterGrade);
            int CourseStatus = CourseStatusCalculate(student.LetterGrade);
            double totalGrade = courseCredit * Gradepoint;

            entity.Grade = Gradepoint;
            entity.LetterGrade = student.LetterGrade;
            entity.TotalGrade = totalGrade;
            entity.CourseStatusId = CourseStatus;
            entity.SectionId = student.SectionId;
            entity.GradingSystemId = ((courseCreditdata.CourseType).ToUpper() == "CORE") ? 1 : ((courseCreditdata.CourseType).ToUpper() == "LAB") ? 2 : 3; 

           // _db.SaveChanges();
            return _db.SaveChanges() > 0;
        }

        //  change date: 27 aug 2018 AA
        public bool UpdateSectionSubmitFinal(int sectionID)
        {
            var entitySection = _db.Sections.Where(x => x.SectionId == sectionID).FirstOrDefault();
            //  entity.SpecialMarkSubmit = false;
            entitySection.ConfirmSubmitByFaculty = true;
            entitySection.HighLight = false;
            return _db.SaveChanges() > 0;
        }
        // create date: 27 aug 2018 AA
        public double GradePointCalculatorByGrade(string Grade)
        {
            String grade = Grade;
           // char grade = Convert.ToChar(Grade);
            double gradepoint = 0.0;
            switch (grade)
            {
                case "A+":
                    gradepoint = 4.00;
                    break;
                case "A":
                    gradepoint = 3.75;
                    break;
                case "A-":
                    gradepoint = 3.50;
                    break;
                case "B+":
                    gradepoint = 3.25;
                    break;
                case "B":
                    gradepoint = 3.00;
                    break;
                case "B-":
                    gradepoint = 2.75;
                    break;
                case "C+":
                    gradepoint = 2.50;
                    break;
                case "C":
                    gradepoint = 2.25;
                    break;
                case "D":
                    gradepoint = 2.00;
                    break;
                case "W":
                    gradepoint = 0.00;
                    break;
                case "F":
                    gradepoint = 0.00;
                    break;
                //P4-W  NCP NCF I
                //case "NCP":
                //    gradepoint = 0.00;
                //    break;
                default:
                    gradepoint = 0.00;
                    break;
            }
            return gradepoint;

        }

        // create date: 27 aug 2018 AA
        public int CourseStatusCalculate(string Grade)
        {
            int CorseStatus = 0;
            switch(Grade)
            {
                case "F":
                    CorseStatus = 7;// 2, 5, 7
                    break;
                case "P4-W":
                    CorseStatus = 9;
                    break;
                case "W":
                    CorseStatus = 4;
                    break;
                case "NCF":
                    CorseStatus = 7;
                    break;
                case "NCP":
                    CorseStatus = 7;
                    break;
                default:
                    CorseStatus = 2;
                    break;
            }
            return CorseStatus;
        }

        public bool CheckStudentEntity(SectionStudentsViewModel students)
        {

            var studentIdentificationID = _db.StudentIdentifications.Where(x => x.StudentId == students.StudentID).FirstOrDefault().StudentIdentificationId;

            DateTime todaydate = DateTime.Today;
            var entity = (from cs in _db.CourseForStudentsAcademics
                          join sc in _db.Sections on cs.SectionId equals sc.SectionId
                          join sm in _db.Semesters on sc.SemesterId equals sm.SemesterId
                          where (cs.SectionId == students.SectionId && cs.StudentIdentificationId == students.StudentIdentificationId && sc.HighLight == true && sc.ConfirmSubmitByFaculty == false && sc.ExpireDateTime >= todaydate)
                          || (cs.SectionId == students.SectionId && cs.StudentIdentificationId == students.StudentIdentificationId && cs.LetterGrade.ToString() == null && cs.SpecialMarkSubmit == true)
                          || (cs.SectionId == students.SectionId && cs.StudentIdentificationId == students.StudentIdentificationId && sm.SpecialGradeuploadDeadLine >= todaydate && sm.FinalTerm == true && sm.ActiveSemester == true)
                          select cs);
            //.Join(_db.Sections, sc=>sc.SectionId,cs=>cs.SectionId,(sc,cs)=> new { SC=sc,CS=cs})
            //.Where((x => x.SC.SectionId == students.SectionID
            //&& x.StudentIdentificationId == studentIdentificationID
            //&& x.HighLight == true && x.Grade == null && x.ConfirmSubmitByFaculty == false && x.) ||
            bool check = entity == null ? false : true;
            return check;
        }

        public List<SectionStudentsViewModel> GetSectionWiseStudents(int SectionID, string userID)
        {
            List<SectionStudentsViewModel> modelList = new List<SectionStudentsViewModel>();

            if ((!string.IsNullOrEmpty(userID)) && SectionID > 0)
            {
                //var accountList = _db.Accounts.Where(x => x.LoginIdentity == userID).FirstOrDefault();
                var teacherInfo = _db.Teachers.Where(x => x.LoginId == userID).FirstOrDefault();
                var teacherWiseSectionsCount = teacherInfo == null ? 0 :
                _db.Sections.Where(x => x.TeacherId == teacherInfo.TeacherId && ( x.SectionId == SectionID)).Count();
                int SectionWiseStudentCount = teacherWiseSectionsCount == 0 ? 0 : _db.CourseForStudentsAcademics.Where(x => x.SectionId == SectionID).Count();

                if (SectionWiseStudentCount > 0)
                {
                    var result = (from crsAcademic in _db.CourseForStudentsAcademics
                                  join std in _db.StudentIdentifications on crsAcademic.StudentIdentificationId equals std.StudentIdentificationId
                                  join stdInfo in _db.StudentInfoes on std.StudentId equals stdInfo.StudentId
                                  join sec in _db.Sections on crsAcademic.SectionId equals sec.SectionId
                                  join sem in _db.Semesters on crsAcademic.SemesterId equals sem.SemesterId
                                  join tech in _db.Teachers on sec.TeacherId equals tech.TeacherId
                                  where crsAcademic.SemesterId == sec.SemesterId &&
                                  crsAcademic.CourseForDepartmentId == sec.CourseForDepartmentId
                                  && crsAcademic.SectionId == SectionID
                                  select new
                                  {
                                      SectionID = SectionID,
                                      StudentID = stdInfo.StudentId,
                                      StudentName = stdInfo.StudentName,
                                      LetterGrade = crsAcademic.LetterGrade,
                                      Grade = crsAcademic.Grade,
                                      ConfirmLetterGrade = crsAcademic.LetterGrade,
                                      ConfirmGrade = crsAcademic.Grade,
                                      HightLight = sec.HighLight,
                                      SpecialMarkSubmit = crsAcademic.SpecialMarkSubmit,
                                      ConfirmSubmitByFaculty = sec.ConfirmSubmitByFaculty,
                                      ExpireDateTime = sec.ExpireDateTime,
                                      FinalTerm = sem.FinalTerm,
                                      SpecialGradeuploadDeadLine = sem.SpecialGradeuploadDeadLine

                                  }).OrderByDescending(x => x.StudentID);


                    foreach (var item in result.ToList())
                    {
                        var model = new SectionStudentsViewModel()
                        {
                            SectionID = item.SectionID,
                            StudentID = item.StudentID,
                            StudentName = item.StudentName,
                            LetterGrade = item.LetterGrade,
                            Grade = item.Grade,
                            ConfirmLetterGrade = item.ConfirmLetterGrade,
                            ConfirmGrade = item.ConfirmGrade,
                            HighLight = item.HightLight,
                            SpecialMarkSubmit = item.SpecialMarkSubmit,
                            FinalTerm = item.FinalTerm,
                            SpecialGradeuploadDeadLine = item.SpecialGradeuploadDeadLine,
                            ConfirmSubmitByFaculty = item.ConfirmSubmitByFaculty
                        };

                        modelList.Add(model);
                    }
                }
            }

            return modelList;
        }

        //public List<SectionStudentsViewModel> Edit(int studentidentifctionID,int sectionID, string lettergrade)
        //{              
        //    var sectionl = _db.CourseForStudentsAcademics.Where(c=>c.StudentIdentificationId == studentidentifctionID && c.SectionId == sectionID).FirstOrDefault();
        //    sectionl.LetterGrade = lettergrade;
        //    sectionl.Grade = lettergrade;
        //    sectionl.TotalGrade = lettergrade;
        //    sectionl.MarkSubmitFinal = true;
        //    _db.SaveChanges();
        //    return sectionl;
        //}
    }
}
