using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Master.Contract;

namespace PickC.Services.DTO
{
    public class OperatorVehicleDTO
    {
        public IEnumerable<LookUp> VehicleType { get; set; }
        public IEnumerable<OperatorVehicles> operatorVehicles { get; set; }
        public OperatorVehicle operatorVehicle { get; set; }   
    }
}
