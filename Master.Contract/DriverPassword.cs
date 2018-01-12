using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Master.Contract;

namespace Master.Contract
{
  public class DriverPassword : IContract
      {
            public string DriverID { get; set; }

            public string Password { get; set; }

            public string NewPassword { get; set; }
        }

        public class DriverForgotPassword
        {
            public string MobileNo { get; set; }

            public string NewPassword { get; set; }

            //public string OTP { get; set; }
        }
    }

