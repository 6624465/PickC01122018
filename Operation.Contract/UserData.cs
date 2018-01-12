using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Operation.Contract
{
    public class UserData
    {
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public List<UserBookingList> userBookingList { get; set; }
        public List<CustomerStatus> customerStatusList { get; set; }
        public UserDataDashBoard userDataDashBoard { get; set; }
        public List<DriverCancellation> driverCancellation { get; set; }
        public List<CustomerCancellation> customerCancellation { get; set; }
    }
    public class UserBookingList
    {
        public string MonthView { get; set; }
        public Int16 totaltrips { get; set; }
    }

    public class UserDataDashBoard
    {
        public Int64 TotalBookings { get; set; }
        public Int64 RegisteredNotBook { get; set; }
        public Int64 DriverCancel { get; set; }
        public Int64 CustomerCancel { get; set; }

    }
    //public class Cancellation
    //{
    //    public List<DriverCancellation> driverCancellation { get; set; }
    //    public List<CustomerCancellation> customerCancellation { get; set; }

    //}
    public class CustomerCancellation
    {
        public string CustomerName { get; set; }
        public string MobileNo { get; set; }
        public string BookingNo { get; set; }
        public DateTime BookingDate { get; set; }
        public string Reason { get; set; }
        public bool Status { get; set; }

    }
    public class DriverCancellation
    {
        public string BookingNo { get; set; }
        public string DriverName { get; set; }
        public string TruckNo { get; set; }
        public string MobileNo { get; set; }
        public string Remarks { get; set; }

    }

}
