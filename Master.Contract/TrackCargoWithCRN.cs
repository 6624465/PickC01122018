using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Master.Contract
{
    public class TrackCargoWithCRN:IContract
    {
        public string BookingNO { get; set; }
        public string BookingNo { get; set; }
        public string LocationFrom { get; set; }
        public string LocationTo { get; set; }
        public string VehicleNo { get; set; }
        public Int16 VehicleType { get; set; }
        public string VehicleCategory { get; set; }
        public string FromLatitude { get; set; }
        public string FromLongitude { get; set; }
        public string ToLatitude { get; set; }
        public string ToLongitude { get; set; }
        public string CurrentLatitude { get; set; }
        public string CurrentLongitude { get; set; }

    }
}
