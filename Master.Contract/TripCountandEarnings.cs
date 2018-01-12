using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Master.Contract;

namespace Master.Contract
{
  public  class TripCountandEarnings : IContract
    {
        public TripCountandEarnings()
        {

        }
        public SearchDates dates { get; set; }
       
        public List<TripCE> trip { get; set; }
        public List<TripCElist> tripCElist { get; set; }
        public class SearchDates
        {
            public DateTime FromDate { get; set; }
            public DateTime ToDate { get; set; }

        }
    }
}
