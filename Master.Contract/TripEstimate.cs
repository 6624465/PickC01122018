using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Master.Contract
{
    public class TripEstimate 
    {
        public Int16 VehicleType { get; set; }
        public int VehicleGroup { get; set; }
        public string frmLatLong { get; set; }
        public string toLatLong { get; set; }
        public int LdUdCharges { get; set; }
    }
}
