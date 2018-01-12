using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Master.Contract
{
   public class OperatorDriversAmountList :IContract
    {
        public string DriverName { get; set; }
        public string vehicleNo { get; set; }
        public string TotalAmount { get; set; }
    }
}
