using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Master.Contract
{
public class TripEstimateForCustomer
    {
        public decimal TotalDistanceFare { get; set; }
        public decimal LoadingUnLoadingCharges { get; set; }
        public decimal TotalTripEstimateminValue { get; set; }

        public decimal GST { get; set; }
        public decimal ApproximateDistanceKM { get; set; }
        public decimal ApproximateTime { get; set; }
        public decimal TotalTripEstimatemaxValue { get; set; }
       public decimal ApproximateTimeFare { get; set; }
    }
}
