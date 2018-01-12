using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Operation.Contract;

namespace Operation.Contract
{
    public class DriverActivity : IContract
    {
        public DriverActivity() { }

        public string TokenNo { get; set; }

        public string DriverId { get; set; }

        public bool IsLogIn { get; set; }

        public DateTime LoginDate { get; set; }

        public DateTime LogoutDate { get; set; }

        public bool IsOnDuty { get; set; }

        public DateTime DutyOnDate { get; set; }

        public DateTime DutyOffDate { get; set; }

        public decimal Latitude { get; set; }

        public decimal Longitude { get; set; }

        public decimal CurrentLat { get; set; }

        public decimal CurrentLong { get; set; }

        public decimal LogOutLat { get; set; }

        public decimal LogOutLong { get; set; }

    }
}




