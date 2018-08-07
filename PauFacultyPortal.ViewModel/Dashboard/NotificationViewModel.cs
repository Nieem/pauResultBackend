using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PauFacultyPortal.ViewModel.Dashboard
{
    public class NotificationViewModel
    {

        public int notification_id { set; get; }
        public string title { set; get; }
        public string description { set; get; }
        public DateTime? from_date { set; get; }
        public DateTime? to_date { set; get; }
        public int AccountsRoleId { set; get; }
        public int? created_by { set; get; }
        public DateTime? create_date { set; get; }
    }
}