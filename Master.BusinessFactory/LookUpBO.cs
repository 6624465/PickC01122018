using Master.Contract;
using Master.DataFactory;
using System.Collections.Generic;

namespace Master.BusinessFactory
{
    public class LookUpBO
    {
        private LookUpDAL lookupDAL;
        public LookUpBO()
        {
            lookupDAL = new LookUpDAL();
        }

        public List<LookUp> GetList()
        {
            return lookupDAL.GetList();
        }

        public List<LookUp> GetVehicleGroupList()
        {
            return lookupDAL.GetVehicleGroupList();
        }


        public bool SaveLookUp(LookUp newItem)
        {
            return lookupDAL.Save(newItem);
        }

        public bool DeleteLookUp(LookUp item)
        {
            return lookupDAL.Delete(item);
        }

        public LookUp GetLookUp(LookUp item)
        {
            return (LookUp)lookupDAL.GetItem<LookUp>(item);
        }

        public List<LookUp> GetVehicleTypeList()
        {
            return lookupDAL.GetVehicleTypeList();
        }

        public List<CargoTypeList> GetCargoTypeList()
        {
            return lookupDAL.GetCargoTypeList();
        }

        public List<LookUp> GetLoadingUnLoadingList()
        {
            return lookupDAL.GetLoadingUnLoadingList();
        }

    }
}
