using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PauFacultyPortal.ViewModel.Profile
{
    public class ProfileMetainfoViewModel
    {
        public int AccountMetaInformationId { get; set; }
        public string FatherName { set; get; }
        public string MothersName { set; get; }
        public int MaritalStatusId { set; get; }
        public string SpouceName { set; get; }
        public int BloodGroupsId { set; get; }
        public int GenderId { set; get; }
        public string CurrentAddress { set; get; }
        public string PermanentAddress { set; get; }
        public DateTime? DateOfBirth { set; get; }
        public DateTime? JoiningDateTime { set; get; }
    }
}
