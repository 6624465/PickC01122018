using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Operation.Contract
{
public    class DriverMonitorInCustomer :IContract
    {
        public DriverMonitorInCustomer() { }

        public string DriverId { get; set; }
        public decimal CurrentLat { get; set; }
        public decimal CurrentLong { get; set; }
    }
}
