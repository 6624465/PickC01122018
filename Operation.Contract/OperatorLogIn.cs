using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Operation.Contract;

namespace Operation.Contract
{
  public  class OperatorLogIn : IContract
    {
        public OperatorLogIn() { }

        public string TokenNo { get; set; }

        public string MobileNo { get; set; }


        public bool Status { get; set; }


        public DateTime LoginTime { get; set; }


        public DateTime LogoutTime { get; set; }
    }
}
