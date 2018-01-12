using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Master.Contract
{
  public  class TripCountList :IContract
    {
        public string DriverName { get; set; }

        public string VehicleNo { get; set; }
        public Int16 VehicleType { get; set; }
        public string LocationFrom { get; set; }
        public string LocationTo { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }

    }
}
