using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PickCApi.Areas.Operation.DTO
{
    public class TrucksInRangeDTO
    {
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }

        public short VehicleType { get; set; }

        public short VehicleGroup { get; set; }
    }
}