using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PauFacultyPortal.ViewModel.Login_History
{
    public class LoginHistoriesViewModel
    {
        public int LoginHistoryId { get; set; }
        [StringLength(25)]
        [Index("IX_EntryTime")]
        public string EntryTime { set; get; }
        public string LastVisitedPage { set; get; }
        public string PcAddress { set; get; }
        public string DeviceType { set; get; }
        public string Browser { set; get; }
    }
}
