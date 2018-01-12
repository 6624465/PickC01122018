using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PickCApi.Areas.Operation.DTO
{
    public class BookingCancelDTO
    {
        public string driverID { get; set; }
        public string vehicleNo { get; set; }
        public string bookingNo { get; set; }
        public string cancelRemarks { get; set; }
        public bool istripstarted { get; set; }
        public bool IsLoadingUnloading { get; set; } 
    }
}