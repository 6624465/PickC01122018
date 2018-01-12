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

        public string VehicleRegistrationNo { get; set; }

        public Int16 VehicleType { get; set; }

        public decimal Latitude { get; set; }

        public decimal Longitude { get; set; }
    }
}
