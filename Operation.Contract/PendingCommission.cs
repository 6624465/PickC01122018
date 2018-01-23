using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Master.Contract;

namespace Operation.Contract
{
    public class PendingCommission
    {
        public List<DriverPendingCommission> driverPendingCommision { get; set; }

        public List<DriverPendingAmount> driverPendingAmount { get; set; }
    }
    public class DriverPendingCommission
    {
            public string DriverName { get; set; }
            public string DriverId { get; set; }
            public string VehicleNo { get; set; }
            public string BookingNo { get; set; }
            public DateTime BookingDate { get; set; }
            public Decimal TripAmount { get; set; }
            public string PaymentType { get; set; }
            public Decimal CommissionAmount { get; set; }
        
    }
}
