using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PauFacultyPortal.ViewModel.Profile
{
    public class ProfileProjectViewModel
    {
        public int AccountExtProjectId { get; set; }
        public string ProjectTitle { get; set; }
        public string ShortDescription { set; get; }
        public string project_Location { get; set; }
        public DateTime? ProjectStratedDate { set; get; }
        public DateTime? ProjectCompletedDate { set; get; }
    }
}
