﻿using PauFacultyPortal.Models;
using PauFacultyPortal.ViewModel.Section;
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
                                      SectionID = sec.SemesterId,
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
                                 where crsAcademic.SectionId == SectionID
                                  select new
                                 {
                                     StudentID = stdInfo.StudentId,
                                     StudentName = stdInfo.StudentName,
                                     LetterGrade = crsAcademic.LetterGrade,
                                     Grade = crsAcademic.Grade,
                                     ConfirmLetterGrade = crsAcademic.LetterGrade,
                                     ConfirmGrade = crsAcademic.Grade


                                 }).OrderByDescending(x=>x.StudentID);


                    foreach (var item in result.ToList())
                    {
                        var model = new SectionStudentsViewModel()
                        {
                            StudentID = item.StudentID,
                            StudentName = item.StudentName,
                            LetterGrade = item.LetterGrade,
                            Grade= item.Grade,
                            ConfirmLetterGrade = item.ConfirmLetterGrade,
                            ConfirmGrade = item.ConfirmGrade
                            


                        };

                        modelList.Add(model);
                    }
                }


            }

            return modelList;
        }





    }
}
