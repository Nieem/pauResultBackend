using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PauFacultyPortal.ViewModel.Login_History
{
    public class UserActivitiesViewModel
    {
        public int UserActivityId { get; set; }
        public int UserActivityTypeId { get; set; }
        public int AccountId { get; set; }
        public string Activity { get; set; }
        public string StudentId { get; set; }
        public Nullable<System.DateTime> ActivityTime { get; set; }
        public bool Seen { get; set; }
        public string ComputerIp { get; set; }
        //public virtual ICollection<UserActivity> UserActivities { set; get; }
    }
}
