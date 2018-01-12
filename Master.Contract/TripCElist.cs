using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Master.Contract
{
public class TripCElist
    {

        public string DriverName { get; set; }
        public string vehicleNo { get; set; }
        public string LocationFrom { get; set; }
        public string LocationTo { get; set; }
        public string VehicleType { get; set; }
        public decimal TripsEarnings { get; set; }
    }
}
