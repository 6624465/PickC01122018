using System;
using System.Collections.Generic;
using Operation.Contract;
using Operation.DataFactory;

namespace Operation.BusinessFactory
{
    public class CustomerLogInBO
    {
        private CustomerLoginDAL customerloginDAL;
        public CustomerLogInBO()
        {
            customerloginDAL = new CustomerLoginDAL();
        }

        public List<CustomerLogin> GetList()
        {
            return customerloginDAL.GetList();
        }

        public bool SaveCustomerLogIn(CustomerLogin newItem)
        {
            return customerloginDAL.Save(newItem);
        }

        public bool AuthUser(CustomerLogin item)
        {
            return customerloginDAL.AuthUser(item);
        }

        public bool DeleteCustomerLogIn(CustomerLogin item)
        {
            return customerloginDAL.Delete(item);
        }

        public CustomerLogin GetCustomerLogIn(CustomerLogin item)
        {
            return (CustomerLogin)customerloginDAL.GetItem<CustomerLogin>(item);
        }

        public string CustomerLogIn(string mobileNo, string password)
        {
            return customerloginDAL.CustomerLogIn(mobileNo, password);
        }

    }
}
