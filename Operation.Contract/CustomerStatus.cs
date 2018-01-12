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
        public DateTime LastLoginDate { get; set; }
    }
}
