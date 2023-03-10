using PauFacultyPortal.Models;
using PauFacultyPortal.ViewModel.Semester;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PauFacultyPortal.Service.Semester
{
    public class SemesterService
    {

        PauFacultyPortalEntities _db = new PauFacultyPortalEntities();
        public List<SemesterViewModel> GetSemesters(string userID)
        {
            List<SemesterViewModel> modelList = new List<SemesterViewModel>();
            if (!string.IsNullOrEmpty(userID))
            {

                var accountList = _db.Accounts.Where(x => x.LoginIdentity == userID).FirstOrDefault();
                var TeacherId = _db.Teachers.Where(x => x.LoginId == accountList.LoginIdentity).FirstOrDefault().TeacherId;

                if (TeacherId > 0)
                {
                    var semesters = from sem in _db.Semesters
                                    join secSem in _db.Sections
                                    on sem.SemesterId equals secSem.SemesterId
                                    where secSem.TeacherId == TeacherId
                                    select new
                                    {
                                        semesterID = sem.SemesterId,
                                        semesterName = sem.SemesterNYear,
                                        semesterIsActive = sem.ActiveSemester

                                    };

                    var orderBySemester = semesters.Distinct().OrderByDescending(x => x.semesterID);

                    foreach (var item in orderBySemester.ToList())
                    {
                        SemesterViewModel model = new SemesterViewModel();
                        model = new SemesterViewModel()
                        {
                            SemesterID = item.semesterID,
                            SemesterName = item.semesterName,
                            ActiveSemester = item.semesterIsActive
                        };

                        modelList.Add(model);

                    }

                }
            }
            return modelList;
        }
    }
}
