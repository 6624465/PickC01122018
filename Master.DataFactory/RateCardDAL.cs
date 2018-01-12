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
    public class RateCardDAL
    {
        private Database db;
        private DbTransaction currentTransaction = null;
        private DbConnection connection = null;
        /// <summary>
        /// Constructor
        /// </summary>
        public RateCardDAL()
        {

            db = DatabaseFactory.CreateDatabase("PickC");

        }

        #region IDataFactory Members

        public List<RateCard> GetList()
        {
            return db.ExecuteSprocAccessor(DBRoutine.LISTRATECARD, MapBuilder<RateCard>.BuildAllProperties()).ToList();
        }

        public bool Save<T>(T item, DbTransaction parentTransaction) where T : IContract
        {
            currentTransaction = parentTransaction;
            return Save(item);

        }

        public bool Save<T>(T item) where T : IContract
        {
            var result = 0;
            var ratecard = (RateCard)(object)item;

            if (currentTransaction == null)
            {
                connection = db.CreateConnection();
                connection.Open();
            }

            var transaction = (currentTransaction == null ? connection.BeginTransaction() : currentTransaction);

            try
            {

                var savecommand = db.GetStoredProcCommand(DBRoutine.SAVERATECARD);
                db.AddInParameter(savecommand, "Category", System.Data.DbType.Int16, ratecard.Category);
                db.AddInParameter(savecommand, "VehicleType", System.Data.DbType.Int16, ratecard.VehicleType);
                db.AddInParameter(savecommand, "RateType", System.Data.DbType.Int16, ratecard.RateType);
                db.AddInParameter(savecommand, "BaseFare", System.Data.DbType.Decimal, ratecard.BaseFare);
                db.AddInParameter(savecommand, "BaseKM", System.Data.DbType.Decimal, ratecard.BaseKM);
                db.AddInParameter(savecommand, "DistanceFare", System.Data.DbType.Decimal, ratecard.DistanceFare);
                db.AddInParameter(savecommand, "RideTimeFare", System.Data.DbType.Decimal, ratecard.RideTimeFare);
                db.AddInParameter(savecommand, "WaitingFare", System.Data.DbType.Decimal, ratecard.WaitingFare);
                db.AddInParameter(savecommand, "CancellationFee", System.Data.DbType.Decimal, ratecard.CancellationFee);
                db.AddInParameter(savecommand, "DriverAssistance", System.Data.DbType.Decimal, ratecard.DriverAssistance);
                db.AddInParameter(savecommand, "OverNightCharges", System.Data.DbType.Decimal, ratecard.OverNightCharges);


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
                if (currentTransaction == null)
                {
                    transaction.Dispose();
                    connection.Close();
                }

            }

            return (result > 0 ? true : false);

        }

        public bool Delete<T>(T item) where T : IContract
        {
            var result = false;
            var ratecard = (RateCard)(object)item;

            var connnection = db.CreateConnection();
            connnection.Open();

            var transaction = connnection.BeginTransaction();

            try
            {
                var deleteCommand = db.GetStoredProcCommand(DBRoutine.DELETERATECARD);
                db.AddInParameter(deleteCommand, "Category", System.Data.DbType.Int16, ratecard.Category);


                result = Convert.ToBoolean(db.ExecuteNonQuery(deleteCommand, transaction));

                transaction.Commit();

            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw ex;
            }
            finally {
                transaction.Dispose();
                connection.Close();

            }

            return result;
        }

        public IContract GetItem<T>(IContract lookupItem) where T : IContract
        {
            var item = ((RateCard)lookupItem);

            var ratecardItem = db.ExecuteSprocAccessor(DBRoutine.SELECTRATECARD,
                                                    MapBuilder<RateCard>.BuildAllProperties(),
                                                    item.Category, item.VehicleType, item.RateType).FirstOrDefault();

            if (ratecardItem == null) return null;

            return ratecardItem;
        }

        #endregion

    }
}
