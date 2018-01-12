using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Master.Contract;

namespace Master.Contract
{
	public class Customer:IContract
	{
		public Customer() { }


		public string MobileNo { get; set; }


		public string Password { get; set; }


		public string Name { get; set; }


		public string EmailId { get; set; }

        public string DeviceId { get; set; }



        public DateTime  CreatedOn { get; set; }

        public string OTP { get; set; }
        public bool IsOTPVerified { get; set; }
        public DateTime? OTPSendDate { get; set; }
        public DateTime? OTPVerifiedDate { get; set; }

    }
   
}




