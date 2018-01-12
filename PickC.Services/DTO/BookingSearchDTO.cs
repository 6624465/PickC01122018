using Operation.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PickC.Services.DTO
{

    public class BookingSearchDTO
    {
        public List<Booking> booking { get; set; }
        public SearchDates dates { get; set; }

    }
    public class SearchDates
    {
        public DateTime fromDate { get; set; }
        public DateTime toDate { get; set; }
    }

}
