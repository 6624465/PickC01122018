using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Master.Contract;

namespace PickC.Internal.ViewModals
{
    public class TripMonitorVm
    {
        public Address address { get; set; }

        public string title { get; set; }
    }

    public class Address
    {
        public string address { get; set; }
        public string lat { get; set; }
        public string lng { get; set; }
    }


    public class DriverMonitorVm
    {
        public List<Driver> driverList { get; set; }
        public List<TripMonitorVm> tripMonitorVmList { get; set; }
    }
}