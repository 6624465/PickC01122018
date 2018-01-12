using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Master.Contract
{
 public class DriverWiseDailyAmountPaymentTypeList :IContract
    {
        public DriverWiseDailyAmountPaymentTypeList()
        {

        }

        public string BookingNo { get; set; }
        public int VehicleType { get; set; }
        public decimal TripAmount { get; set; }
        public DateTime TripDate { get; set; }
        public string PaymentType { get; set; }
    }
}
