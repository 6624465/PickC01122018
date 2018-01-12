using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Master.Contract;
using Master.DataFactory;
using PickC.Services.DTO;


namespace Master.DataFactory
{
   public class TruckDAL
    {
        private Database db;
        private DbTransaction currentTransaction = null;
        private DbConnection connection = null;
        /// <summary>
        /// Constructor
        /// </summary>
        public TruckDAL()
        {

            db = DatabaseFactory.CreateDatabase("PickC");

        }
        #region IDataFactory Members
        public List<TruckList> GetList(string MobileNo)
        {
            return db.ExecuteSprocAccessor(DBRoutine.LISTTRUCK, MapBuilder<TruckList>.MapAllProperties().DoNotMap(x => x.Maker).Build(), MobileNo).ToList();
        }
        public List<TruckList> GetListByType(int VehicleType,string MobileNo)
        {
            return db.ExecuteSprocAccessor(DBRoutine.SELECTTRUCK, MapBuilder<TruckList>.MapAllProperties().DoNotMap(x => x.Maker).Build(), VehicleType, MobileNo).ToList();
        }


        #endregion
    }
}
