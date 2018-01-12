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

namespace Master.DataFactory
{
    public class VehicleConfigDAL
    {
        private Database db;
        private DbTransaction currentTransaction = null;
        private DbConnection connection = null;
        /// <summary>
        /// Constructor
        /// </summary>
        public VehicleConfigDAL()
        {

            db = DatabaseFactory.CreateDatabase("PickC");

        }

        #region IDataFactory Members

        public List<VehicleConfig> GetList()
        {
            return db.ExecuteSprocAccessor(DBRoutine.LISTVEHICLECONFIG, MapBuilder<VehicleConfig>.BuildAllProperties()).ToList();
        }

        public bool Save<T>(T item, DbTransaction parentTransaction) where T : IContract
        {
            currentTransaction = parentTransaction;
            return Save(item);

        }

        public bool Save<T>(T item) where T : IContract
        {
            var result = 0;
            var vehicleconfig = (VehicleConfig)(object)item;

            if (currentTransaction == null)
            {
                connection = db.CreateConnection();
                connection.Open();
            }

            var transaction = (currentTransaction == null ? connection.BeginTransaction() : currentTransaction);

            try
            {

                var savecommand = db.GetStoredProcCommand(DBRoutine.SAVEVEHICLECONFIG);
                db.AddInParameter(savecommand, "VehicleType", System.Data.DbType.Int16, vehicleconfig.VehicleType);
                db.AddInParameter(savecommand, "VehicleDescription", System.Data.DbType.String, vehicleconfig.VehicleDescription);
                db.AddInParameter(savecommand, "Maker", System.Data.DbType.String, vehicleconfig.Maker);
                db.AddInParameter(savecommand, "Model", System.Data.DbType.String, vehicleconfig.Model);
                db.AddInParameter(savecommand, "Tonnage", System.Data.DbType.Decimal, vehicleconfig.Tonnage);
                db.AddInParameter(savecommand, "VehicleGroup", System.Data.DbType.Int16, vehicleconfig.VehicleGroup);


                result = db.ExecuteNonQuery(savecommand, transaction);

                if (currentTransaction == null)
                    transaction.Commit();

            }
            catch (Exception)
            {
                if (currentTransaction == null)
                    transaction.Rollback();

                throw;
            }
            finally
            {
                transaction.Dispose();
                connection.Close();

            }

            return (result > 0 ? true : false);

        }

        public bool Delete<T>(T item) where T : IContract
        {
            var result = false;
            var vehicleconfig = (VehicleConfig)(object)item;

            var connnection = db.CreateConnection();
            connnection.Open();

            var transaction = connnection.BeginTransaction();

            try
            {
                var deleteCommand = db.GetStoredProcCommand(DBRoutine.DELETEVEHICLECONFIG);
                db.AddInParameter(deleteCommand, "VehicleType", System.Data.DbType.Int16, vehicleconfig.VehicleType);


                result = Convert.ToBoolean(db.ExecuteNonQuery(deleteCommand, transaction));

                transaction.Commit();

            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw ex;
            }
            finally
            {
                transaction.Dispose();
                connnection.Close();

            }

            return result;
        }

        public IContract GetItem<T>(IContract lookupItem) where T : IContract
        {
            var item = ((VehicleConfig)lookupItem);

            var vehicleconfigItem = db.ExecuteSprocAccessor(DBRoutine.SELECTVEHICLECONFIG,
                                                    MapBuilder<VehicleConfig>.BuildAllProperties(),
                                                    item.VehicleType).FirstOrDefault();

            if (vehicleconfigItem == null) return null;

            return vehicleconfigItem;
        }

        #endregion

    }
}
