using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Master.Contract;

namespace PickC.Web.ViewModels
{
    public class RateCardVm
    {
        public List<LookUp> VehicleGrouplookUpData { get; set; }
        public List<LookUp> VehicleTypelookUpData { get; set; }
        public string City { get; set; }
        public string VehicleGroup { get; set; }
        public Int16 VehicleType { get; set; }
    }
}