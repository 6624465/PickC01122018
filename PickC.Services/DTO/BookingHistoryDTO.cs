using Operation.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PickC.Services.DTO
{
    public class BookingHistoryDTO
    { 
        public List<Booking> booking { get; set; }
        public List<BookingHistory> bookingHistory { get; set; }
        public string BookingNo { get; set; }
        public string CustomerMobile { get; set; }
        public DateTime? Datefrom { get; set; }
        public DateTime? DateTo { get; set; }
        public List<BookingHistoryDetails> BookingHistoryDetails { get; set; }
        public BookingSearchsDTO bookings { get; set; }
    }
    public class BookingSearchsDTO
    {
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
    }

}
