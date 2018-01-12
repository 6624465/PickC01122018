using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Operation.Contract
{
   public class UpdateDriverCurrentLocation : IContract
    {
        public UpdateDriverCurrentLocation() { }

        public string DriverID { get; set; }
        public string AUTH_TOKEN { get; set; }
        public decimal CurrentLatitude { get; set; }
        public decimal CurrentLongitude { get; set; }
        public bool IsLogIn { get; set; }
        public bool IsOnDuty { get; set; }
        public decimal Accuracy { get; set; }
        public decimal Bearing { get; set; }
    }
    public class AccuracyRate
    {
        public decimal accuracy { get; set; }
        public decimal bearing{ get; set; }
    }
    public class UpdateDriverCurrentLocationSample : IContract
    {
        
        public string DriverID { get; set; }
        //public string AUTH_TOKEN { get; set; }
        public decimal CurrentLatitude { get; set; }
        public decimal CurrentLongitude { get; set; }
        public bool IsLogIn { get; set; }
        public bool IsOnDuty { get; set; }
        public decimal Accuracy { get; set; }
        public decimal Bearing { get; set; }
    }
}
