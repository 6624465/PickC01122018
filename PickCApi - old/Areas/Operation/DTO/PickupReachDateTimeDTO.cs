using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PickCApi.Areas.Operation.DTO
{
    public class PickupReachDateTimeDTO
    {
        public string bookingNo { get; set; }

        public DateTime PickupReachDateTime { get; set; }
    }

    public class DestinationReachDateTimeDTO
    {
        public string bookingNo { get; set; }

        public DateTime DestinationReachDateTime { get; set; }
    }
}