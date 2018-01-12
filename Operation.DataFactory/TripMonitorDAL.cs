using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Operation.Contract;
using Operation.DataFactory;

namespace Operation.DataFactory
{
    public class TripMonitorDAL
    {
        private Database db;
        private DbTransaction currentTransaction = null;
        private DbConnection connection = null;
        /// <summary>
        /// Constructor
        /// </summary>
        public TripMonitorDAL()
        {

            db = DatabaseFactory.CreateDatabase("PickC");

        }

        #region IDataFactory Members

        public List<TripMonitor> GetList()
        {
            return db.ExecuteSprocAccessor(DBRoutine.LISTTRIPMONITOR2, MapBuilder<TripMonitor>.BuildAllProperties()).ToList();
        }

        public bool Save<T>(T item, DbTransaction parentTransaction) where T : IContract
        {
            currentTransaction = parentTransaction;
            return Save(item);

        }

        public bool Save<T>(T item) where T : IContract
        {
            var result = 0;
            var tripmonitor = (TripMonitor)(object)item;

            if (currentTransaction == null)
            {
                connection = db.CreateConnection();
                connection.Open();
            }

            var transaction = (currentTransaction == null ? connection.BeginTransaction() : currentTransaction);

            try
            {

                var savecommand = db.GetStoredProcCommand(DBRoutine.SAVETRIPMONITOR);
                db.AddInParameter(savecommand, "DriverID", System.Data.DbType.String, tripmonitor.DriverID);
                db.AddInParameter(savecommand, "TripID", System.Data.DbType.String, tripmonitor.TripID);
                db.AddInParameter(savecommand, "VehicleNo", System.Data.DbType.String, tripmonitor.VehicleNo);
                db.AddInParameter(savecommand, "RefreshDate", System.Data.DbType.DateTime, tripmonitor.RefreshDate);
                db.AddInParameter(savecommand, "Latitude", System.Data.DbType.String, tripmonitor.Latitude);
                db.AddInParameter(savecommand, "Longitude", System.Data.DbType.String, tripmonitor.Longitude);
                db.AddInParameter(savecommand, "TripType", System.Data.DbType.Int16, tripmonitor.TripType);


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
            var tripmonitor = (TripMonitor)(object)item;

            var connnection = db.CreateConnection();
            connnection.Open();

            var transaction = connnection.BeginTransaction();

            try
            {
                var deleteCommand = db.GetStoredProcCommand(DBRoutine.DELETETRIPMONITOR);
                db.AddInParameter(deleteCommand, "DriverID", System.Data.DbType.String, tripmonitor.DriverID);


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
                connection.Close();
            }

            return result;
        }

        public IContract GetItem<T>(IContract lookupItem) where T : IContract
        {
            var item = ((TripMonitor)lookupItem);

            var tripmonitorItem = db.ExecuteSprocAccessor(DBRoutine.SELECTTRIPMONITOR,
                                                    MapBuilder<TripMonitor>.BuildAllProperties(),
                                                    item.DriverID).FirstOrDefault();

            if (tripmonitorItem == null) return null;

            return tripmonitorItem;
        }

        #endregion

    }
}
