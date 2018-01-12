using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Master.Contract;
using Operation.Contract;

namespace PickC.Web.ViewModels
{
    public class TripEstimateVm
    {
        public List<LookUp> VehicleGrouplookUpData { get; set; }
        public List<LookUp> VehicleTypelookUpData { get; set; }

        public Customer customer { get; set; }
        public Booking booking { get; set; }
    }
}