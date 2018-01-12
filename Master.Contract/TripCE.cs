using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Master.Contract
{
   public class TripCE :IContract
    {
        public int TripsCount { get; set; }
        public decimal TripsEarnings { get; set; }
    }
}
