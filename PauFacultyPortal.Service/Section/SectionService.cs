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

        public int UpdateStuentResult(SectionStudentsViewModel student)
        {

           var studentIdentificationID = _db.StudentIdentifications
                .Where(x => x.StudentId == student.StudentID).FirstOrDefault().StudentIdentificationId;

            var entity = _db.CourseForStudentsAcademics
                .Where(x => x.SectionId == student.SectionID && x.StudentIdentificationId == studentIdentificationID).FirstOrDefault();

            var entitySection = _db.Sections
                .Where(x => x.SectionId == student.SectionID).FirstOrDefault();

            double courseCredit = _db.CourseForDepartments
                .Where(x => x.CourseForDepartmentId == entity.CourseForDepartmentId).FirstOrDefault().Credit;

            double totalGrade = courseCredit * student.Grade;

            entity.Grade = courseCredit;
            entity.LetterGrade = student.LetterGrade;
            entity.TotalGrade = student.TotalGrade;
            //  entity.SpecialMarkSubmit = false;
            entitySection.ConfirmSubmitByFaculty = true;

            _db.SaveChanges();
            return 1;
        }

        public bool CheckStudentEntity(SectionStudentsViewModel students)
        {

            var studentIdentificationID = _db.StudentIdentifications.Where(x => x.StudentId == students.StudentID).FirstOrDefault().StudentIdentificationId;

            DateTime todaydate = DateTime.Today;
            var entity = (from cs in _db.CourseForStudentsAcademics
                       
                        join sc in _db.Sections on cs.SectionId equals sc.SectionId
                        join sm in _db.Semesters on sc.SemesterId equals sm.SemesterId
                        where (cs.SectionId == students.SectionId && cs.StudentIdentificationId == students.StudentIdentificationId && sc.HighLight == true && sc.ConfirmSubmitByFaculty == false && sc.ExpireDateTime >= todaydate)
                        || (cs.Grade.ToString() == null && cs.SpecialMarkSubmit == true)
                        || (sm.SpecialGradeuploadDeadLine >= todaydate && sm.FinalTerm == true && sm.ActiveSemester == true)
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

                var accountList = _db.Accounts.Where(x => x.LoginIdentity == userID).FirstOrDefault();
                var teacherInfo = _db.Teachers.Where(x => x.LoginId == accountList.LoginIdentity).FirstOrDefault();
                int SectionWiseStudentCount = teacherInfo == null ? 0 : _db.CourseForStudentsAcademics.Where(x => x.SectionId == SectionID).Count();

                if (SectionWiseStudentCount > 0)
                {
                    var result = (from crsAcademic in _db.CourseForStudentsAcademics
                                  join std in _db.StudentIdentifications on crsAcademic.StudentIdentificationId equals std.StudentIdentificationId
                                  join stdInfo in _db.StudentInfoes on std.StudentId equals stdInfo.StudentId
                                  join sec in _db.Sections on crsAcademic.SectionId equals sec.SectionId
                                  join sem in _db.Semesters on crsAcademic.SemesterId equals sem.SemesterId
                                  where crsAcademic.SemesterId == sec.SemesterId &&
                                  crsAcademic.CourseForDepartmentId == sec.CourseForDepartmentId && crsAcademic.SectionId == SectionID
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
