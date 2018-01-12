using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Operation.Contract;

namespace Operation.Contract
{
    public class TripMonitor : IContract
    {
        public TripMonitor() { }


        public string DriverID { get; set; }


        public string TripID { get; set; }


        public string VehicleNo { get; set; }


        public DateTime RefreshDate { get; set; }

        public string Latitude { get; set; }

        public string Longitude { get; set; }

        public Int16 TripType { get; set; }


    }
}




