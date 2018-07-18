using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PauFacultyPortal.ViewModel
{
    public class ProfileViewModel
    {
        public int AccountId { set; get; }
        public string Name { set; get; }
        public string Email { set; get; }
        //[Remote("CheckPhoneForAccount", "Check", ErrorMessage = "This phone number is occupied by another User.")]
        public string PhoneNo { set; get; }
        public string TeleExchange { set; get; }
        public string Designation { set; get; }
        public string DepartmentSection { set; get; }
        public string LoginIdentity { set; get; }
        public string Password { set; get; }
        public int AccountsRoleId { set; get; }
        public string Pic { set; get; }
        public bool Status { set; get; }
        public bool PartimeTeacher { set; get; }
        public DateTime? ExpireDate { set; get; }
        public DateTime? CreatedDate { set; get; }
        public bool Deactivate { set; get; }
        public string CauseOfDeactivate { set; get; }
        public bool TeporaryBlock { set; get; }
        public DateTime? TemporaryBlockExpireDate { set; get; }
        public string LastPsswordChange { get; set; }

        //==================== Education ======================================

        public List<ProfileEducationViewModel> ProfileEducation { get; set; }


        //================   Professional activity ====================================
        public List<ProfileProfesionalActivityViewModel> ProfileProfessionalActivity { get; set; }


        //=====================================================

        public List<ProfileProjectViewModel> ProfileProject { get; set; }


        //====================== Metainformation=====================================

        public List<ProfileMetainfoViewModel> ProfileMetainformation { get; set; }


        //======================  MetaProfessional ====================================\
        public List<ProfileMetaProfesionalViewModel> ProfileMetaProfessional { get; set; }


    }
}
