using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Master.Contract
{
public class DriverDetails:IContract
    {
        public DriverDetails()
        {

        }
        public string DriverID { get; set; }


        public string DriverName { get; set; }

        public string Password { get; set; }
        public string FatherName { get; set; }


        public DateTime DateOfBirth { get; set; }


        public string PlaceOfBirth { get; set; }


        public string Gender { get; set; }


        public string MaritialStatus { get; set; }

        public string MobileNo { get; set; }


        public string PhoneNo { get; set; }


        public string PANNo { get; set; }


        public string AadharCardNo { get; set; }


        public string LicenseNo { get; set; }


        public bool Status { get; set; }


        public string CreatedBy { get; set; }


        public DateTime CreatedOn { get; set; }


        public string ModifiedBy { get; set; }


        public DateTime ModifiedOn { get; set; }


        public bool IsVerified { get; set; }


        public string VerifiedBy { get; set; }

        public string DeviceID { get; set; }


        public DateTime VerifiedOn { get; set; }

        public List<Address> AddressList { get; set; }

        public string Nationality { get; set; }
        public string OperatorID { get; set; }
        public List<BankDetails> BankDetails { get; set; }
    }
}
