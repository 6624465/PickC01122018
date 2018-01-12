using System;
using System.Collections.Generic;
using Master.Contract;
using Master.DataFactory;
using PickC.Services.DTO;
namespace Master.BusinessFactory
{
  public  class TruckBO
    {
        private TruckDAL truckDAL;
        public TruckBO()
        {
            truckDAL = new TruckDAL();
        }
        public List<TruckList> GetList(string MobileNo)
        {
            return truckDAL.GetList(MobileNo);
        }
        public List<TruckList> GetListByType(int VehicleType,string MobileNo)
        {
            return truckDAL.GetListByType(VehicleType,MobileNo);
        }
    }
}
