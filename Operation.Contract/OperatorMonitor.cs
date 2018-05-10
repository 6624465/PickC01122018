using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Operation.Contract
{
   public class OperatorMonitor : IContract
    {
        public string DriverName { get; set; }

        public string DriverID { get; set; }

        public string MobileNo { get; set; }


        public string VehicleRegistrationNo { get; set; }

        public string VehicleType { get; set; }

        public string VehicleGroup { get; set; }

        public decimal Latitude { get; set; }

        public decimal Longitude { get; set; }
    }
}
