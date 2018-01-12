using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Master.Contract
{
   public class BankDetails:IContract
    {
        public BankDetails()
        {

        }
        public string OperatorBankID { get; set; }
        public string BankName { get; set; }
        public string Branch { get; set; }
        public string AccountNumber { get; set; }
        public string AccountType { get; set; }
    }
}
