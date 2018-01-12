using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Master.Contract
{
    public class PendingAmount:IContract
    {
        public string DriverID { get; set; }
        public string DriverName { get; set; }
        public string VehicleNo { get; set; }
        public Int16 VehicleType { get; set; }
        public DateTime TripDate { get; set; }
        public string TripFrom { get; set; }
        public string TripTo { get; set; }
        public decimal Pendingamount { get; set; }
        public string Invoiceno { get; set; }
    }
}
