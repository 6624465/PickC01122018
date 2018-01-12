using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Master.Contract
{
   public class CustomerBillDetails:IContract
    {
        public string BookingNo { get; set; }
        public string TotalAmount { get; set; }
        public string DriverId { get; set; }

    }
}
