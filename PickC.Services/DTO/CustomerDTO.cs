using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PickC.Services.DTO
{
    public class CustomerDTO
    {
        public string MobileNo { get; set; }
        public string Name { get; set; }
        public string EmailID { get; set; }
    }
    public class CustomerInfo
    {
        public string CustomerName { get; set; }
        public string MobileNo { get; set; }
        public string EmailID { get; set; }
        public int TotalBookings { get; set; }
        public DateTime RegistrationDate { get; set; }
    }
}
