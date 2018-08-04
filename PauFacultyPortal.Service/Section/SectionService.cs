using PauFacultyPortal.Models;
using PauFacultyPortal.ViewModel.Section;
using PauFacultyPortal.ViewModel.Semester;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PauFacultyPortal.Service.Section
{
    public class SectionService
    {
        PauFacultyPortalEntities _db = new PauFacultyPortalEntities();
        public List<SectionListViewModel> GetSections(string userId,int semesterID)
        {
            List<SectionListViewModel> modelList = new List<SectionListViewModel>();
            SectionListViewModel model = new SectionListViewModel();
            if ((! string.IsNullOrEmpty(userId) ) && semesterID > 0)
            {

                var accountList = _db.Accounts.Where(x => x.LoginIdentity == userId).FirstOrDefault();
                var teacherInfo = _db.Teachers.Where(x => x.LoginId == accountList.LoginIdentity).FirstOrDefault();
                string semesterName = _db.Semesters.Where(x => x.SemesterId == semesterID).FirstOrDefault().SemesterNYear;





            }

            return modelList;

        }

        
    }
}
