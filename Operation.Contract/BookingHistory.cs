using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Operation.Contract
{
   public class BookingHistory
    {
        public string BookingNo { get; set; }
        public string CustomerName { get; set; }
        public int CustomerMobile { get; set; }
        public string VehicleNo { get; set; }
        public string DriverName { get; set; }
        public string TripFrom { get; set; }
        public string TripTo { get; set; }
        public string InvoiceAmount { get; set; }

        public DateTime Datefrom { get; set; }
        public DateTime DateTo { get; set; }
    }
}
