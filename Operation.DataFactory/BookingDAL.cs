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
    public class BookingDAL
    {
        private Database db;
        private DbTransaction currentTransaction = null;
        private DbConnection connection = null;
        /// <summary>
        /// Constructor
        /// </summary>
        public BookingDAL()
        {

            db = DatabaseFactory.CreateDatabase("PickC");

        }

        #region IDataFactory Members

        public List<Booking> GetList()
        {
            return db.ExecuteSprocAccessor(DBRoutine.LISTBOOKING, MapBuilder<Booking>.BuildAllProperties()).ToList();
        }
        public List<BookingHistoryList> GetBookingHistoryList()
        {
            return db.ExecuteSprocAccessor(DBRoutine.BOOKINGHISTORYLIST, MapBuilder<BookingHistoryList>.BuildAllProperties()).ToList();
        }
        public List<Booking> GetCustomerBySearch(int? status)
        {

            List<Booking> list = db.ExecuteSprocAccessor(DBRoutine.LISTOFBOOKINGBYSTATUS,
                                                       MapBuilder<Booking>.BuildAllProperties(), status).ToList();
            return list;
        }

        public List<Booking> GetListByMobileNo(string mobileNo)
        {
            return db.ExecuteSprocAccessor(DBRoutine.LISTOFBOOKINGBYMOBILENO, MapBuilder<Booking>.BuildAllProperties(), mobileNo).ToList();
        }
        public List<Booking> GetListByBookingNo(string BookingNo)
        {
            return db.ExecuteSprocAccessor(DBRoutine.LISTOFBOOKINGBYBOOKINGNO, MapBuilder<Booking>.BuildAllProperties(), BookingNo).ToList();
        }

        public bool Save<T>(T item, DbTransaction parentTransaction) where T : IContract
        {
            currentTransaction = parentTransaction;
            return Save(item);

        }

        public bool Save<T>(T item) where T : IContract
        {
            var result = 0;
            var booking = (Booking)(object)item;

            if (currentTransaction == null)
            {
                connection = db.CreateConnection();
                connection.Open();
            }

            var transaction = (currentTransaction == null ? connection.BeginTransaction() : currentTransaction);

            try
            {

                var savecommand = db.GetStoredProcCommand(DBRoutine.SAVEBOOKING);
                db.AddInParameter(savecommand, "BookingNo", System.Data.DbType.String, booking.BookingNo);
                db.AddInParameter(savecommand, "BookingDate", System.Data.DbType.DateTime, booking.BookingDate);
                db.AddInParameter(savecommand, "CustomerID", System.Data.DbType.String, booking.CustomerId);
                db.AddInParameter(savecommand, "RequiredDate", System.Data.DbType.DateTime, booking.RequiredDate);
                db.AddInParameter(savecommand, "LocationFrom", System.Data.DbType.String, booking.LocationFrom);
                db.AddInParameter(savecommand, "LocationTo", System.Data.DbType.String, booking.LocationTo);
                db.AddInParameter(savecommand, "CargoDescription", System.Data.DbType.String, booking.CargoDescription ?? "");
                db.AddInParameter(savecommand, "VehicleType", System.Data.DbType.Int16, booking.VehicleType);
                db.AddInParameter(savecommand, "VehicleGroup", System.Data.DbType.Int16, booking.VehicleGroup);
                db.AddInParameter(savecommand, "Remarks", System.Data.DbType.String, booking.Remarks ?? "");
                /*
                db.AddInParameter(savecommand, "IsConfirm", System.Data.DbType.Boolean, booking.IsConfirm);
                db.AddInParameter(savecommand, "ConfirmDate", System.Data.DbType.DateTime, booking.ConfirmDate);
                db.AddInParameter(savecommand, "DriverID", System.Data.DbType.String, booking.DriverID);
                db.AddInParameter(savecommand, "VehicleNo", System.Data.DbType.String, booking.VehicleNo);
                db.AddInParameter(savecommand, "IsCancel", System.Data.DbType.Boolean, booking.IsCancel);
                db.AddInParameter(savecommand, "CancelTime", System.Data.DbType.DateTime, booking.CancelTime);
                db.AddInParameter(savecommand, "CancelRemarks", System.Data.DbType.String, booking.CancelRemarks);
                db.AddInParameter(savecommand, "IsComplete", System.Data.DbType.Boolean, booking.IsComplete);
                db.AddInParameter(savecommand, "CompleteTime", System.Data.DbType.DateTime, booking.CompleteTime);
                */
                db.AddInParameter(savecommand, "PayLoad", System.Data.DbType.String, booking.PayLoad ?? "");
                db.AddInParameter(savecommand, "CargoType", System.Data.DbType.String, booking.CargoType ?? "");
                db.AddInParameter(savecommand, "Latitude", System.Data.DbType.Decimal, booking.Latitude);
                db.AddInParameter(savecommand, "Longitude", System.Data.DbType.Decimal, booking.Longitude);
                db.AddInParameter(savecommand, "ToLatitude", System.Data.DbType.Decimal, booking.ToLatitude);
                db.AddInParameter(savecommand, "ToLongitude", System.Data.DbType.Decimal, booking.ToLongitude);
                db.AddInParameter(savecommand, "ReceiverMobileNo", System.Data.DbType.String, "9666245400");
                db.AddInParameter(savecommand, "LoadingUnLoading", System.Data.DbType.Int16, booking.LoadingUnLoading);
                db.AddOutParameter(savecommand, "NewBookingNo", System.Data.DbType.String, 50);


                result = db.ExecuteNonQuery(savecommand, transaction);

                if (result > 0)
                {
                    booking.BookingNo = savecommand.Parameters["@NewBookingNo"].Value.ToString();
                }

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
            var booking = (Booking)(object)item;

            var connnection = db.CreateConnection();
            connnection.Open();

            var transaction = connnection.BeginTransaction();

            try
            {
                var deleteCommand = db.GetStoredProcCommand(DBRoutine.DELETEBOOKING);
                db.AddInParameter(deleteCommand, "BookingNo", System.Data.DbType.String, booking.BookingNo);


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
            var item = ((Booking)lookupItem);

            var bookingItem = db.ExecuteSprocAccessor(DBRoutine.SELECTBOOKING,
                                                    MapBuilder<Booking>.BuildAllProperties(),
                                                    item.BookingNo).FirstOrDefault();

            if (bookingItem == null) return null;

            return bookingItem;
        }

        #endregion


        public List<Booking> GetNearBookingsForDriver(Guid tokenNo, string driverID, decimal latitude, decimal longitude, short minDistance)
        {


            return db.ExecuteSprocAccessor(DBRoutine.LISTNEARBOOKINGSFORDRIVER,
                                            MapBuilder<Booking>
                                            .BuildAllProperties(),
                                            tokenNo, driverID, (decimal)latitude, (decimal)longitude, minDistance).ToList();
        }

        public List<NearTrucksInRange> GetTrucksInRange(string CustomerID, decimal latitude, decimal longitude, short minDistance, short vehicleGroup, short vehicleType)
        {


            return db.ExecuteSprocAccessor(DBRoutine.LISTNEARTRUCKSINRANGE,
                                            MapBuilder<NearTrucksInRange>
                                            .BuildAllProperties(),
                                            "", (decimal)latitude, (decimal)longitude, minDistance, vehicleType, vehicleGroup).ToList();
        }
        public bool BookingRejectedByDriver(string tokenNo, string driverID, string vehicleNo, string bookingNo, string cancelRemarks, bool isTripStarted, bool IsLoadingUnloading)
        {

            var result = false;


            connection = db.CreateConnection();
            connection.Open();

            var transaction = connection.BeginTransaction();

            try
            {
                var deleteCommand = db.GetStoredProcCommand(DBRoutine.BOOKINGREJECTEDBYDRIVER);
                db.AddInParameter(deleteCommand, "TokenNo", System.Data.DbType.String, tokenNo);
                db.AddInParameter(deleteCommand, "DriverID", System.Data.DbType.String, driverID);
                db.AddInParameter(deleteCommand, "VehicleNo", System.Data.DbType.String, vehicleNo);
                db.AddInParameter(deleteCommand, "BookingNo", System.Data.DbType.String, bookingNo);
                db.AddInParameter(deleteCommand, "CancelRemarks", System.Data.DbType.String, cancelRemarks);
                db.AddInParameter(deleteCommand, "IsTripStarted", System.Data.DbType.Boolean, isTripStarted);
                db.AddInParameter(deleteCommand, "IsLoadingUnloading", System.Data.DbType.Boolean, IsLoadingUnloading);


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
        public bool BookingCancelledByDriver(string tokenNo, string driverID, string vehicleNo, string bookingNo, string cancelRemarks, bool isTripStarted,bool IsLoadingUnloading)
        {

            var result = false;


            connection = db.CreateConnection();
            connection.Open();

            var transaction = connection.BeginTransaction();

            try
            {
                var deleteCommand = db.GetStoredProcCommand(DBRoutine.BOOKINGCANCELLEDBYDRIVER);
                db.AddInParameter(deleteCommand, "TokenNo", System.Data.DbType.String, tokenNo);
                db.AddInParameter(deleteCommand, "DriverID", System.Data.DbType.String, driverID);
                db.AddInParameter(deleteCommand, "VehicleNo", System.Data.DbType.String, vehicleNo);
                db.AddInParameter(deleteCommand, "BookingNo", System.Data.DbType.String, bookingNo);
                db.AddInParameter(deleteCommand, "CancelRemarks", System.Data.DbType.String, cancelRemarks);
                db.AddInParameter(deleteCommand, "IsTripStarted", System.Data.DbType.Boolean, isTripStarted);
                db.AddInParameter(deleteCommand, "IsLoadingUnloading", System.Data.DbType.Boolean, IsLoadingUnloading);


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

        public bool BookingConfirmByDriver(string driverID, string tokenNo, string vehicleNo, string bookingNo,string CustomerOTP)
        {

            var result = false;

            var count = IsBookingAlreadyConfirmed(bookingNo);

            if (count > 0)
            {
                throw new Exception("Booking is Already Confirmed by Other Driver!");

            }



            connection = db.CreateConnection();
            connection.Open();

            var transaction = connection.BeginTransaction();

            try
            {
                var deleteCommand = db.GetStoredProcCommand(DBRoutine.BOOKINGCONFIRMBYDRIVER);
                db.AddInParameter(deleteCommand, "TokenNo", System.Data.DbType.String, tokenNo);
                db.AddInParameter(deleteCommand, "DriverID", System.Data.DbType.String, driverID);
                db.AddInParameter(deleteCommand, "VehicleNo", System.Data.DbType.String, vehicleNo);
                db.AddInParameter(deleteCommand, "BookingNo", System.Data.DbType.String, bookingNo);
                db.AddInParameter(deleteCommand, "OTP", System.Data.DbType.String, CustomerOTP);

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




        public Int64 IsBookingAlreadyConfirmed(string bookingNo)
        {

            var recordcommand = db.GetStoredProcCommand(DBRoutine.ISBOOKINGALREADYCONFIRMED, bookingNo);
            var recordCount = Convert.ToInt64(db.ExecuteScalar(recordcommand));
            return recordCount;

        }

        public string GetCustomerDeviceIDByBookingNo(string bookingNo)
        {

            var recordcommand = db.GetStoredProcCommand(DBRoutine.GETCUSTOMERDEVICEIDBYBOOKINGNO, bookingNo);
            var result = db.ExecuteScalar(recordcommand).ToString();
            return result;

        }

        public string GetCustomerDeviceIDByTripID(string TripID)
        {

            var recordcommand = db.GetStoredProcCommand(DBRoutine.GETCUSTOMERDEVICEIDBYTRIPID, TripID);
            var result = db.ExecuteScalar(recordcommand).ToString();
            return result;

        }

        public List<Master.Contract.Driver> GetNearTrucksDeviceID(string bookingNo, short minDistance, short vehicleType, short vehicleGroup, decimal latitude, decimal longitude)
        {


            return db.ExecuteSprocAccessor(DBRoutine.NEARTRUCKSDEVICELIST,
                                            MapBuilder<Master.Contract.Driver>
                                            .BuildAllProperties(),
                                            bookingNo, minDistance, vehicleType, vehicleGroup, latitude, longitude).ToList();
        }

        public string GetDriverDeviceIDByBookingNo(string bookingNo)
        {

            var recordcommand = db.GetStoredProcCommand(DBRoutine.GETDRIVERDEVICEIDBYBOOKINGNO, bookingNo);
            var result = db.ExecuteScalar(recordcommand).ToString();
            return result;

        }

        public bool SavePickupReachDateTime(string bookingNo, DateTime PickupReachDateTime)
        {

            var result = false;

            connection = db.CreateConnection();
            connection.Open();

            var transaction = connection.BeginTransaction();

            try
            {
                var updateCommand = db.GetStoredProcCommand(DBRoutine.SAVEPICKUPREACHDATETIME);
                db.AddInParameter(updateCommand, "BookingNo", System.Data.DbType.String, bookingNo);
                db.AddInParameter(updateCommand, "PickupReachDateTime", System.Data.DbType.DateTime, PickupReachDateTime);               


                result = Convert.ToBoolean(db.ExecuteNonQuery(updateCommand, transaction));
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

        public bool SaveDestinationReachDateTime(string bookingNo, DateTime DestinationReachDateTime)
        {

            var result = false;

            connection = db.CreateConnection();
            connection.Open();

            var transaction = connection.BeginTransaction();

            try
            {
                var updateCommand = db.GetStoredProcCommand(DBRoutine.SAVEDESTINATIONREACHDATETIME);
                db.AddInParameter(updateCommand, "BookingNo", System.Data.DbType.String, bookingNo);
                db.AddInParameter(updateCommand, "DestinationReachDateTime", System.Data.DbType.DateTime, DestinationReachDateTime);


                result = Convert.ToBoolean(db.ExecuteNonQuery(updateCommand, transaction));
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

        public List<BookingHistoryDetails> GetBookingListByMobileNo(string MobileNo)
        {
            return db.ExecuteSprocAccessor(DBRoutine.LISTOFBOOKINGHISTORYLISTBYMOBILENO, MapBuilder<BookingHistoryDetails>.BuildAllProperties(), MobileNo).ToList();
        }
        public bool UpdateCustomerPaymentValues(string bookingNo)
        {

            var result = false;

            connection = db.CreateConnection();
            connection.Open();

            var transaction = connection.BeginTransaction();

            try
            {
                var updateCommand = db.GetStoredProcCommand(DBRoutine.CUSTOMERRECEIVEDUPDATEPAYMENTVALUES);
                db.AddInParameter(updateCommand, "BookingNo", System.Data.DbType.String, bookingNo);

                result = Convert.ToBoolean(db.ExecuteNonQuery(updateCommand, transaction));
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
        public string GetCustomerDeviceIDByBookingNoByPaymentReceiveConfirm(string bookingNo)
        {

            var recordcommand = db.GetStoredProcCommand(DBRoutine.GETCUSTOMERDEVICEIDBYBOOKINGNOFORPAYMENTRECEIVE, bookingNo);
            var result = db.ExecuteScalar(recordcommand).ToString();
            return result;

        }
        public Booking GetCustomerCurrentConfirmTrip(string mobileNo)
        {
            var tripItem = db.ExecuteSprocAccessor(DBRoutine.CUSTOMERCURRENTTRIPBEFORETRIPSTART,
                                                    MapBuilder<Booking>.BuildAllProperties(),
                                                    mobileNo).FirstOrDefault();

            if (tripItem == null) return null;

            return tripItem;
        }

    }
}
