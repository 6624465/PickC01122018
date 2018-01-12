using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PickCApi.Areas.Operation.DTO
{
    public class TripEstimateDTO
    {
        public decimal frmLat { get; set; }

        public decimal frmLog { get; set; }

        public decimal toLat { get; set; }

        public decimal toLog { get; set; }
    }

    public class TripEstimateResponse
    {
        public decimal distance { get; set; }

        public decimal duration { get; set; }

        public decimal amount { get; set; }
    }

}