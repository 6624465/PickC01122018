using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PickC.Services.DTO
{
   public class SummaryDTO
    {
        public string driverId { get; set; }
        public decimal CurrentBalance { get; set; }
        public decimal TodaySummary { get; set; }
        public int TodayBookings { get; set; }
        public decimal TodayPayment { get; set; }
        public decimal LastPayment { get; set; }
        public decimal pickcCommission { get; set; }
    }
}
