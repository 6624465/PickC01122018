using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Operation.Contract;

namespace Operation.Contract
{
    public class Trip : IContract
    {
        public Trip() { }


        public string TripID { get; set; }


        public DateTime TripDate { get; set; }


        public string CustomerMobile { get; set; }


        public string DriverID { get; set; }


        public string VehicleNo { get; set; }


        public Int16 VehicleType { get; set; }


        public Int16 VehicleGroup { get; set; }


        public string LocationFrom { get; set; }


        public string LocationTo { get; set; }


        public decimal Distance { get; set; }


        public DateTime StartTime { get; set; }


        public DateTime? EndTime { get; set; }


        public decimal TripMinutes { get; set; }


        public decimal WaitingMinutes { get; set; }


        public string TotalWeight { get; set; }


        public string CargoDescription { get; set; }


        public string Remarks { get; set; }

        public decimal Latitude { get; set; }

        public decimal Longitude { get; set; }

        public decimal TripEndLat { get; set; }

        public decimal TripEndLong { get; set; }

        public decimal? DistanceTravelled { get; set; }

        public string BookingNo { get; set; }

    }

    public class TripEndDTO
    {
        public string TripId { get; set; }
        public DateTime EndTime { get; set; }
        public decimal TripEndLat { get; set; }
        public decimal TripEndLong { get; set; }
        public string Token { get; set; }
        public string DriverId { get; set; }
        public string CustomerMobile { get; set; }
        //public decimal? DistanceTravelled { get; set; }

    }
}




