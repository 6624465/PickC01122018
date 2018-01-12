using System;
using System.Collections.Generic;
using Master.Contract;
using Master.DataFactory;

namespace Master.BusinessFactory
{
    public class VehicleConfigBO
    {
        private VehicleConfigDAL vehicleconfigDAL;
        public VehicleConfigBO()
        {
            vehicleconfigDAL = new VehicleConfigDAL();
        }

        public List<VehicleConfig> GetList()
        {
            return vehicleconfigDAL.GetList();
        }

        public bool SaveVehicleConfig(VehicleConfig newItem)
        {

            return vehicleconfigDAL.Save(newItem);
        }

        public bool DeleteVehicleConfig(VehicleConfig item)
        {
            return vehicleconfigDAL.Delete(item);
        }

        public VehicleConfig GetVehicleConfig(VehicleConfig item)
        {
            return (VehicleConfig)vehicleconfigDAL.GetItem<VehicleConfig>(item);
        }

    }
}
