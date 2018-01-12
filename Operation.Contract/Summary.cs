using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Operation.Contract
{
    public class Summary:IContract
    {
        public Summary()
        {

        }

        public decimal CurrentBalance { get; set; }
        public decimal TodaySummary { get; set; }
        public int TodayBookings { get; set; }
        public decimal TodayPayment { get; set; }
        public decimal LastPayment { get; set; }
    }
}
