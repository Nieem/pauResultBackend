using System;

namespace PauFacultyPortal.ViewModel
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