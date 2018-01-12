using System;
using System.Collections.Generic;
using Operation.Contract;
using Operation.DataFactory;


namespace Operation.BusinessFactory
{
   public class OperatorLogInBO
    {
        private OperatorLogInDAL operatorloginDAL;

        public OperatorLogInBO()
        {
            operatorloginDAL = new OperatorLogInDAL();
        }
        public string OperatorLogIn(string mobileNo, string password)
        {
            return operatorloginDAL.OperatorLogIn(mobileNo, password);
        }
        public bool AuthUser(OperatorLogIn item)
        {
            return operatorloginDAL.AuthUser(item);
        }
        public bool DeleteOperatorLogIn(OperatorLogIn item)
        {
            return operatorloginDAL.DeleteOperatorLogIn(item);
        }
        public List<OperatorMonitor> GetOperatorDriverMonitor(string MobileNo)
        {
            return operatorloginDAL.GetOperatorDriverMonitor(MobileNo);
        }
    }
}
