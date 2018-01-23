using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Operation.Contract
{
    public class CustomerStatus
    {
        public string Name { get; set; }
        public string MobileNo { get; set; }
        public string EmailId { get; set; }
        public DateTime RegisteredDate { get; set; }
        public DateTime? LastLoginDate { get; set; }
    }
    public class RegButNotBookedSearch
    {
     
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
    }
}
