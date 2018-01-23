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
    public class TripDAL
    {
        private Database db;
        private DbTransaction currentTransaction = null;
        private DbConnection connection = null;
        /// <summary>
        /// Constructor
        /// </summary>
        public TripDAL()
        {

            db = DatabaseFactory.CreateDatabase("PickC");

        }

        #region IDataFactory Members

        public List<Trip> GetList()
        {
            return db.ExecuteSprocAccessor(DBRoutine.LISTTRIP, MapBuilder<Trip>.BuildAllProperties()).ToList();
        }

        public bool Save<T>(T item, DbTransaction parentTransaction) where T : IContract
        {
            currentTransaction = parentTransaction;
            return Save(item);

        }

        public bool Save<T>(T item) where T : IContract
        {
            var result = 0;
            var trip = (Trip)(object)item;

            if (currentTransaction == null)
            {
                connection = db.CreateConnection();
                connection.Open();
            }

            var transaction = (currentTransaction == null ? connection.BeginTransaction() : currentTransaction);

            try
            {

                var savecommand = db.GetStoredProcCommand(DBRoutine.SAVETRIP);
                db.AddInParameter(savecommand, "TripID", System.Data.DbType.String, trip.TripID);
                db.AddInParameter(savecommand, "TripDate", System.Data.DbType.DateTime, trip.TripDate);
                db.AddInParameter(savecommand, "CustomerMobile", System.Data.DbType.String, trip.CustomerMobile);
                db.AddInParameter(savecommand, "DriverID", System.Data.DbType.String, trip.DriverID);
                db.AddInParameter(savecommand, "VehicleNo", System.Data.DbType.String, trip.VehicleNo);
                db.AddInParameter(savecommand, "VehicleType", System.Data.DbType.Int16, trip.VehicleType);
                db.AddInParameter(savecommand, "VehicleGroup", System.Data.DbType.Int16, trip.VehicleGroup);
                db.AddInParameter(savecommand, "LocationFrom", System.Data.DbType.String, trip.LocationFrom);
                db.AddInParameter(savecommand, "LocationTo", System.Data.DbType.String, trip.LocationTo);
                db.AddInParameter(savecommand, "Distance", System.Data.DbType.Decimal, trip.Distance);
                db.AddInParameter(savecommand, "StartTime", System.Data.DbType.DateTime, trip.StartTime);
                //db.AddInParameter(savecommand, "EndTime", System.Data.DbType.DateTime, trip.EndTime);
                //db.AddInParameter(savecommand, "TripMinutes", System.Data.DbType.Decimal, trip.TripMinutes);
                //db.AddInParameter(savecommand, "WaitingMinutes", System.Data.DbType.Decimal, trip.WaitingMinutes);
                db.AddInParameter(savecommand, "TotalWeight", System.Data.DbType.String, trip.TotalWeight);
                db.AddInParameter(savecommand, "CargoDescription", System.Data.DbType.String, trip.CargoDescription);
                db.AddInParameter(savecommand, "Remarks", System.Data.DbType.String, trip.Remarks);
                db.AddInParameter(savecommand, "Latitude", System.Data.DbType.Decimal, trip.Latitude);
                db.AddInParameter(savecommand, "Longitude", System.Data.DbType.Decimal, trip.Longitude);
                db.AddInParameter(savecommand, "DistanceTravelled", System.Data.DbType.Decimal, trip.DistanceTravelled);
                db.AddInParameter(savecommand, "BookingNo", System.Data.DbType.String, trip.BookingNo);

                db.AddOutParameter(savecommand, "NewOrderNo", System.Data.DbType.String, 50);

                //trip.TripID = 
                result = db.ExecuteNonQuery(savecommand, transaction);

                if (result > 0)
                    trip.TripID = savecommand.Parameters["@NewOrderNo"].Value.ToString();

                if (currentTransaction == null)
                    transaction.Commit();

            }
            catch (Exception ex)
            {
                if (currentTransaction == null)
                    transaction.Rollback();

                throw ex;
            }
            finally
            {
                transaction.Dispose();
                connection.Close();
            }

            return (result > 0 ? true : false);

        }

        public bool EndTrip(TripEndDTO tripEndDTO, decimal distance)
        {
            var result = 0;

            if (currentTransaction == null)
            {
                connection = db.CreateConnection();
                connection.Open();
            }

            var transaction = (currentTransaction == null ? connection.BeginTransaction() : currentTransaction);

            try
            {

                var savecommand = db.GetStoredProcCommand(DBRoutine.TRIPEND);
                db.AddInParameter(savecommand, "TokenNo", System.Data.DbType.String, tripEndDTO.Token);
                db.AddInParameter(savecommand, "TripID", System.Data.DbType.String, tripEndDTO.TripId);
                db.AddInParameter(savecommand, "DriverID", System.Data.DbType.String, tripEndDTO.DriverId);
                db.AddInParameter(savecommand, "EndTime", System.Data.DbType.DateTime, tripEndDTO.EndTime);
                db.AddInParameter(savecommand, "TripEndLat", System.Data.DbType.Decimal, tripEndDTO.TripEndLat);
                db.AddInParameter(savecommand, "TripEndLong", System.Data.DbType.Decimal, tripEndDTO.TripEndLong);
                db.AddInParameter(savecommand, "DistanceTravelled", System.Data.DbType.Decimal, 0.00M);
                db.AddInParameter(savecommand, "Distance", System.Data.DbType.Decimal, distance);
                result = db.ExecuteNonQuery(savecommand, transaction);

                if (currentTransaction == null)
                    transaction.Commit();

            }
            catch (Exception ex)
            {
                if (currentTransaction == null)
                    transaction.Rollback();

                throw ex;
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
            var trip = (Trip)(object)item;

            var connnection = db.CreateConnection();
            connnection.Open();

            var transaction = connnection.BeginTransaction();

            try
            {
                var deleteCommand = db.GetStoredProcCommand(DBRoutine.DELETETRIP);
                db.AddInParameter(deleteCommand, "TripID", System.Data.DbType.String, trip.TripID);


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
            var item = ((Trip)lookupItem);

            var tripItem = db.ExecuteSprocAccessor(DBRoutine.SELECTTRIP,
                                                    MapBuilder<Trip>.BuildAllProperties(),
                                                    item.TripID).FirstOrDefault();

            if (tripItem == null) return null;

            return tripItem;
        }

        public Trip CustomerCurrentTrip(string mobileNo)
        {
            var tripItem = db.ExecuteSprocAccessor(DBRoutine.CUSTOMERCURRENTTRIP,
                                                    MapBuilder<Trip>.BuildAllProperties(),
                                                    mobileNo).FirstOrDefault();

            if (tripItem == null) return null;

            return tripItem;
        }

        public Trip DriverCurrentTrip(string driverID)
        {
            var tripItem = db.ExecuteSprocAccessor(DBRoutine.DRIVERCURRENTTRIP,
                                                    MapBuilder<Trip>.BuildAllProperties(),
                                                    driverID).FirstOrDefault();

            if (tripItem == null) return null;

            return tripItem;
        }

        public bool TripUpdateTravelledDistance(string tripID, decimal distanceTravelled)
        {
            var result = 0;

            if (currentTransaction == null)
            {
                connection = db.CreateConnection();
                connection.Open();
            }

            var transaction = (currentTransaction == null ? connection.BeginTransaction() : currentTransaction);

            try
            {

                var udpatecommand = db.GetStoredProcCommand(DBRoutine.TRIPUPDATETRAVELLEDDISTANCE);
                db.AddInParameter(udpatecommand, "TripID", System.Data.DbType.String, tripID);
                db.AddInParameter(udpatecommand, "DistanceTravelled", System.Data.DbType.Decimal, distanceTravelled);


                result = db.ExecuteNonQuery(udpatecommand, transaction);

                if (currentTransaction == null)
                    transaction.Commit();

            }
            catch (Exception ex)
            {
                if (currentTransaction == null)
                    transaction.Rollback();

                throw ex;
            }
            finally
            {
                transaction.Dispose();
                connection.Close();
            }

            return (result > 0 ? true : false);

        }

        #endregion

        public List<UserBookingList> GetTotalTrips(UserData data)
        {
            return db.ExecuteSprocAccessor(DBRoutine.TOTALTRIPS, MapBuilder<UserBookingList>.BuildAllProperties(), data.DateFrom, data.DateTo).ToList();
        }

        public UserDataDashBoard GetuserDataDashBoard()
        {
            return db.ExecuteSprocAccessor(DBRoutine.DASHBOARDCOUNT, MapBuilder<UserDataDashBoard>.BuildAllProperties()).FirstOrDefault();
        }

        public List<CustomerStatus> GetCustomerStatusList()
        {
            return db.ExecuteSprocAccessor(DBRoutine.REGISTEREDBUTNOTBOOKEDLIST, MapBuilder<CustomerStatus>.BuildAllProperties()).ToList();
        }

        public List<CustomerStatus> GetCustomerStatusList(RegButNotBookedSearch obj)
        {
            return db.ExecuteSprocAccessor(DBRoutine.REGISTEREDBUTNOTBOOKEDLISTBYDATERANGE, MapBuilder<CustomerStatus>.BuildAllProperties(),obj.DateFrom,obj.DateTo).ToList();
        }
        public List<CustomerCancellation> getCancelledListByCustomer()
        {
            return db.ExecuteSprocAccessor(DBRoutine.BOOKINGCANCELLEDBYCUSTOMER, MapBuilder<CustomerCancellation>.BuildAllProperties()).ToList();
        }
        public List<DriverCancellation> getCancelledListByDriver()
        {
            return db.ExecuteSprocAccessor(DBRoutine.BOOKINGCANCELLEDBYDRIVERLIST, MapBuilder<DriverCancellation>.BuildAllProperties()).ToList();
        }
        public List<TripDailyBasis> getTripDetailsDailyBasis()
        {
            return db.ExecuteSprocAccessor(DBRoutine.TOTALTRIPDETAILSDAILYBASIS, MapBuilder<TripDailyBasis>.BuildAllProperties()).ToList();
        }

    }
}
