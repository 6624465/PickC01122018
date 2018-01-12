using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Master.Contract
{
    public class ContactUs : IContract
    {
       
        public string Name { get; set; }
        public string Email { get; set; }
        public string MobileNo { get; set; }     
        public string Message { get; set; }
        public string Subject { get; set; }
        public string Type{ get;set;}
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}
