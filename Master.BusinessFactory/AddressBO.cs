using System;
using System.Collections.Generic;
using Master.Contract;
using Master.DataFactory;

namespace Master.BusinessFactory
{
    public class AddressBO
    {
        private AddressDAL addressDAL;
        public AddressBO()
        {
            addressDAL = new AddressDAL();
        }

        public List<Address> GetList(string addressLinkID)
        {
            return addressDAL.GetList(addressLinkID);
        }

        public bool SaveAddress(Address newItem)
        {

            return addressDAL.Save(newItem);
        }

        public bool DeleteAddress(Address item)
        {
            return addressDAL.Delete(item);
        }

        public Address GetAddress(Address item)
        {
            return (Address)addressDAL.GetItem<Address>(item);
        }

    }
}
