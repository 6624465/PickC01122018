using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Operation.Contract
{
    public class BookingHistoryDetails : IContract
    {
        public string BookingNo { get; set; }
        public DateTime BookingDate { get; set; }
        public string CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string LocationFrom { get; set; }
        public string LocationTo { get; set; }
        public string VehicleGroup { get; set; }
        public string DriverName { get; set; }
        public string VehicleNo { get; set; }
        public Int16 VehicleType { get; set; }
    }
}
