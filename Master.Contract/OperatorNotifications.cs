using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Master.Contract
{
  public  class OperatorNotifications:IContract
    {
        public bool DriverLoginLogoutStatus { get; set; }
        public bool DriverOnOffDuty { get; set; }
        public bool DailyIncentiveUpdate { get; set; }
        public string DriverID { get; set; }
    }
}
