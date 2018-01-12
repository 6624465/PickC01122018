using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Master.Contract
{
  public class OperatorPasssword : IContract
    {
        public string MobileNo { get; set; }

        public string Password { get; set; }

        public string NewPassword { get; set; }
        //public object OTP { get; set; }
        //public object OTPVerifiedDate { get; set; }
    }

    public class ForgotPassword
    {
        public string MobileNo { get; set; }

        public string NewPassword { get; set; }

        //public string OTP { get; set; }
    }
}

