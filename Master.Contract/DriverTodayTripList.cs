using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Master.Contract
{
    public class DriverTodayTripList :IContract
    {
        public string BookingNo { get; set; }
        public string VehicleNo { get; set; }
        public string LocationFrom { get; set; }
        public string LocationTo { get; set; }
        public Int16 VehicleType { get; set; }
        public string DriverName { get; set; }

    }
}
