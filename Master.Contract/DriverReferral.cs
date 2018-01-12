using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Master.Contract
{
    public class DriverReferral : IContract
    {
        public Int64 ReferralId { get; set; }

        public string Name { get; set; }

        public string Mobile { get; set; }

        public string EmailID { get; set; }

        public string DriverID { get; set; }

        public bool Status { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public string ModifiedBy { get; set; }

        public DateTime ModifiedOn { get; set; }

        //public string OTP { get; set; }
        //public bool IsOTPVerified { get; set; }
        //public DateTime? OTPSendDate { get; set; }
        //public DateTime? OTPVerifiedDate { get; set; }
    }
}
