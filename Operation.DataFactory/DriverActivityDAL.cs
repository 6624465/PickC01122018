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
using System.Data;

namespace Operation.DataFactory
{
    public class DriverActivityDAL
    {
        private Database db;
        private DbTransaction currentTransaction = null;
        private DbConnection connection = null;
        /// <summary>
        /// Constructor
        /// </summary>
        public DriverActivityDAL()
        {

            db = DatabaseFactory.CreateDatabase("PickC");

        }

        #region IDataFactory Members

        public List<DriverActivity> GetList()
        {
            return db.ExecuteSprocAccessor(DBRoutine.LISTDRIVERACTIVITY, MapBuilder<DriverActivity>.BuildAllProperties()).ToList();
        }

        public bool AuthenticateDriver<T>(T item, DbTransaction parentTransaction) where T : IContract
        {
            currentTransaction = parentTransaction;
            return true;
        }

        public bool AuthenticateDriver<T>(T item) where T : IContract
        {
            var result = 0;
            var driveractivity = (DriverActivity)(object)item;

            if (currentTransaction == null)
            {
                connection = db.CreateConnection();
                connection.Open();
            }

            var transaction = (currentTransaction == null ? connection.BeginTransaction() : currentTransaction);

            try
            {
                var authcmd = db.GetStoredProcCommand(DBRoutine.AUTHENTICATEDRIVER);

                db.AddInParameter(authcmd, "TokenNo", System.Data.DbType.String, driveractivity.TokenNo);
                db.AddInParameter(authcmd, "DriverID", System.Data.DbType.String, driveractivity.DriverId);
                db.AddInParameter(authcmd, "Latitude", System.Data.DbType.String, driveractivity.Latitude);
                db.AddInParameter(authcmd, "Longitude", System.Data.DbType.String, driveractivity.Longitude);

                result = Convert.ToInt32(db.ExecuteScalar(authcmd, transaction));

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


        public bool Save<T>(T item, DbTransaction parentTransaction) where T : IContract
        {
            currentTransaction = parentTransaction;
            return Save(item);

        }

        public bool Save<T>(T item) where T : IContract
        {
            var result = 0;
            var driveractivity = (DriverActivity)(object)item;

            if (currentTransaction == null)
            {
                connection = db.CreateConnection();
                connection.Open();
            }

            var transaction = (currentTransaction == null ? connection.BeginTransaction() : currentTransaction);

            try
            {

                var savecommand = db.GetStoredProcCommand(DBRoutine.SAVEDRIVERACTIVITY);
                db.AddInParameter(savecommand, "TokenNo", System.Data.DbType.String, driveractivity.TokenNo);
                db.AddInParameter(savecommand, "DriverID", System.Data.DbType.String, driveractivity.DriverId);
                db.AddInParameter(savecommand, "IsLogIn", System.Data.DbType.Boolean, driveractivity.IsLogIn);
                db.AddInParameter(savecommand, "LoginDate", System.Data.DbType.DateTime, driveractivity.LoginDate);
                db.AddInParameter(savecommand, "LogoutDate", System.Data.DbType.DateTime, driveractivity.LogoutDate);
                db.AddInParameter(savecommand, "IsOnDuty", System.Data.DbType.Boolean, driveractivity.IsOnDuty);
                db.AddInParameter(savecommand, "DutyOnDate", System.Data.DbType.DateTime, driveractivity.DutyOnDate);
                db.AddInParameter(savecommand, "DutyOffDate", System.Data.DbType.DateTime, driveractivity.DutyOffDate);
                db.AddInParameter(savecommand, "Latitude", System.Data.DbType.String, driveractivity.Latitude);
                db.AddInParameter(savecommand, "Longitude", System.Data.DbType.String, driveractivity.Longitude);
                db.AddOutParameter(savecommand, "NewTokenNo", System.Data.DbType.String, 50);


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
        public bool UpdateCurrentDriverLocation<T>(T item) where T : IContract
        {
            var result = 0;
            var updateDriverCurrentLocation = (UpdateDriverCurrentLocation)(object)item;

            if (currentTransaction == null)
            {
                connection = db.CreateConnection();
                connection.Open();
            }

            var transaction = (currentTransaction == null ? connection.BeginTransaction() : currentTransaction);

            try
            {

                var savecommand = db.GetStoredProcCommand(DBRoutine.UPDATEDRIVERCURRENTLOCATIONVALUES);

                db.AddInParameter(savecommand, "DriverID", System.Data.DbType.String, updateDriverCurrentLocation.DriverID);
                db.AddInParameter(savecommand, "AUTH_TOKEN", System.Data.DbType.String, updateDriverCurrentLocation.AUTH_TOKEN);
                db.AddInParameter(savecommand, "CurrentLatitude", System.Data.DbType.String, updateDriverCurrentLocation.CurrentLatitude);
                db.AddInParameter(savecommand, "CurrentLongitude", System.Data.DbType.String, updateDriverCurrentLocation.CurrentLongitude);
                db.AddInParameter(savecommand, "IsLogIn", System.Data.DbType.Boolean, updateDriverCurrentLocation.IsLogIn);
                db.AddInParameter(savecommand, "IsOnDuty", System.Data.DbType.Boolean, updateDriverCurrentLocation.IsOnDuty);
                db.AddInParameter(savecommand, "Accuracy", System.Data.DbType.Decimal, updateDriverCurrentLocation.Accuracy);
                db.AddInParameter(savecommand, "Bearing", System.Data.DbType.Decimal, updateDriverCurrentLocation.Bearing);
                db.AddInParameter(savecommand, "BookingNo", System.Data.DbType.String, updateDriverCurrentLocation.BookingNo);

                result = db.ExecuteNonQuery(savecommand, transaction);

                if (currentTransaction == null)
                    transaction.Commit();

            }
            catch (Exception ex)
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
        public string GetCustomerDeviceIDByBookingNo(string bookingNo)
        {

            var recordcommand = db.GetStoredProcCommand(DBRoutine.GETCUSTOMERDEVICEIDBYBOOKINGNO, bookingNo);
            var result = db.ExecuteScalar(recordcommand).ToString();
            return result;

        }
        public IContract GetDriverMonitorInCustomer<T>(IContract lookupItem) where T : IContract
        {
            var item = ((DriverMonitorInCustomer)lookupItem);
            var driverMonitorInCustomer = db.ExecuteSprocAccessor(DBRoutine.SELECTDRIVERMONITORINCUSTOMER,
                                                    MapBuilder<DriverMonitorInCustomer>.BuildAllProperties(),
                                                    item.DriverId).FirstOrDefault();

            if (driverMonitorInCustomer == null) return null;

            return driverMonitorInCustomer;
        }
        public string GetFiveLatLongsforDriver(string DriverID, string Token)
        {
            var recordcommand = db.GetStoredProcCommand(DBRoutine.DRIVERLATESTFIVELATLONGS, DriverID, Token);
            var result = db.ExecuteScalar(recordcommand).ToString();
            return result;
        }
        public bool Delete<T>(T item) where T : IContract
        {
            var result = false;
            var driveractivity = (DriverActivity)(object)item;

            var connnection = db.CreateConnection();
            connnection.Open();

            var transaction = connnection.BeginTransaction();

            try
            {
                var deleteCommand = db.GetStoredProcCommand(DBRoutine.DELETEDRIVERACTIVITY);

                db.AddInParameter(deleteCommand, "TokenNo", System.Data.DbType.String, driveractivity.TokenNo);
                db.AddInParameter(deleteCommand, "DriverID", System.Data.DbType.String, driveractivity.DriverId);

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
        //public IContract GetDriverMonitorInCustomer<T>(IContract lookupItem) where T : IContract
        //{
        //    var item = ((DriverMonitorInCustomer)lookupItem);
        //    var driverMonitorInCustomer = db.ExecuteSprocAccessor(DBRoutine.SELECTDRIVERMONITORINCUSTOMER,
        //                                            MapBuilder<DriverMonitorInCustomer>.BuildAllProperties(),
        //                                            item.DriverId).FirstOrDefault();

        //    if (driverMonitorInCustomer == null) return null;

        //    return driverMonitorInCustomer;
        //}

        public object ChangeType(object value, Type type)
        {
            if (value == null && type.IsGenericType) return Activator.CreateInstance(type);
            if (value == null) return null;
            if (type == value.GetType()) return value;
            if (type.IsEnum)
            {
                if (value is string)
                    return Enum.Parse(type, value as string);
                else
                    return Enum.ToObject(type, value);
            }
            if (!type.IsInterface && type.IsGenericType)
            {
                Type innerType = type.GetGenericArguments()[0];
                object innerValue = ChangeType(value, innerType);
                return Activator.CreateInstance(type, new object[] { innerValue });
            }
            if (value is string && type == typeof(Guid)) return new Guid(value as string);
            if (value is string && type == typeof(Version)) return new Version(value as string);
            if (!(value is IConvertible)) return value;
            return Convert.ChangeType(value, type);
        }
        public IContract GetItem<T>(IContract lookupItem) where T : IContract
        {
            //try
            //{
            //    var item = ((DriverActivity)lookupItem);

            //    //var guid = ChangeType(item.TokenNo, typeof(Guid));
            //    //var guid = new Guid(item.TokenNo);

            //    var driveractivityItem = db.ExecuteSprocAccessor(DBRoutine.SELECTDRIVERACTIVITY,
            //                                            MapBuilder<DriverActivity>.BuildAllProperties(),
            //                                            item.TokenNo,item.DriverId).FirstOrDefault();

            //    if (driveractivityItem == null) return null;

            //    return driveractivityItem;
            //}
            //catch (Exception ex)
            //{

            //    throw ex;
            //}

           /* Problem with token uniqueidentifier */
           var item = ((DriverActivity)lookupItem);
           string sSql = " Declare @Token nvarchar(100) = '"+ item.TokenNo  + "'; " +
                         " Declare @DriverID nvarchar(20) = '" + item.DriverId + "';" +
                         " SELECT [TokenNo], [DriverID],VehicleNo, [IsLogIn], [LoginDate], [LogoutDate], [IsOnDuty], [DutyOnDate], " +
                         " [DutyOffDate], [Latitude], [Longitude],CurrentLat,CurrentLong,LogOutLat,LogOutLong " +
                         " FROM [Operation].[DriverActivity] " +
                         " WHERE [TokenNo] =  CAST(@Token AS UNIQUEIDENTIFIER)  " +
                         " AND [DriverID] = @DriverID";
            
            DbCommand dbCmdWrapper = db.GetSqlStringCommand(sSql);
            System.Data.DataSet driverActivity = db.ExecuteDataSet(dbCmdWrapper);

            var daTbl = driverActivity.Tables[0];
            var da = daTbl.AsEnumerable().Select(x => new DriverActivity
            {
                TokenNo = x["TokenNo"].ToString(),
                DriverId = x["DriverId"].ToString(),
                //VehicleNo
                IsLogIn = Convert.ToBoolean(x["IsLogIn"] != DBNull.Value ? x["IsLogIn"] : 0),
                LoginDate = Convert.ToDateTime(x["LoginDate"] != DBNull.Value ? x["LoginDate"] : DateTime.Now),
                LogoutDate = Convert.ToDateTime(x["LogoutDate"] != DBNull.Value ? x["LogoutDate"] : DateTime.Now),
                IsOnDuty = Convert.ToBoolean(x["IsOnDuty"] != DBNull.Value ? x["IsOnDuty"] : 0),
                DutyOnDate = Convert.ToDateTime(x["DutyOnDate"] != DBNull.Value ? x["DutyOnDate"] : DateTime.Now),
                DutyOffDate = Convert.ToDateTime(x["DutyOffDate"] != DBNull.Value ? x["DutyOffDate"] : DateTime.Now),
                Latitude = Convert.ToDecimal(x["Latitude"] != DBNull.Value ? x["Latitude"] : 0),
                Longitude = Convert.ToDecimal(x["Longitude"] != DBNull.Value? x["Longitude"] : 0),
                CurrentLat = Convert.ToDecimal(x["CurrentLat"] != DBNull.Value ? x["CurrentLat"] : 0),
                CurrentLong = Convert.ToDecimal(x["CurrentLong"] != DBNull.Value ? x["CurrentLong"] : 0),
                LogOutLat = Convert.ToDecimal(x["LogOutLat"] != DBNull.Value ? x["LogOutLat"] : 0),
                LogOutLong = Convert.ToDecimal(x["LogOutLong"] != DBNull.Value ? x["LogOutLong"] : 0),
            }).FirstOrDefault();
            
            return da;
        }

        public IContract GetDriverActivityByDriverID<T>(IContract lookupItem) where T : IContract
        {
            var item = ((DriverActivity)lookupItem);

            var driveractivityItem = db.ExecuteSprocAccessor(DBRoutine.SELECTDRIVERACTIVITYBYDRIVERID,
                                                    MapBuilder<DriverActivity>.BuildAllProperties(),
                                                    item.DriverId).FirstOrDefault();

            if (driveractivityItem == null) return null;

            return driveractivityItem;
        }

        #endregion


        public string DriverLogIn(string driverID, string password, string latitude, string longitude)
        {

            var result = 0;

            string tokenNo = "";

            if (currentTransaction == null)
            {
                connection = db.CreateConnection();
                connection.Open();
            }

            var transaction = (currentTransaction == null ? connection.BeginTransaction() : currentTransaction);

            try
            {

                var savecommand = db.GetStoredProcCommand(DBRoutine.DRIVERLOGIN);

                db.AddInParameter(savecommand, "DriverID", System.Data.DbType.String, driverID);
                db.AddInParameter(savecommand, "Password", System.Data.DbType.String, password);
                db.AddInParameter(savecommand, "Latitude", System.Data.DbType.String, latitude);
                db.AddInParameter(savecommand, "Longitude", System.Data.DbType.String, longitude);
                db.AddOutParameter(savecommand, "NewTokenNo", System.Data.DbType.String, 50);

                result = db.ExecuteNonQuery(savecommand, transaction);

                tokenNo = savecommand.Parameters["@NewTokenNo"].Value.ToString();

                if (currentTransaction == null)
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

            return tokenNo;
        }

        public bool DriverActivityUpdate<T>(T item) where T : IContract
        {
            var result = 0;
            var driveractivity = (DriverActivity)(object)item;

            if (currentTransaction == null)
            {
                connection = db.CreateConnection();
                connection.Open();
            }

            var transaction = (currentTransaction == null ? connection.BeginTransaction() : currentTransaction);

            try
            {

                var savecommand = db.GetStoredProcCommand(DBRoutine.UPDATEDRIVERDUTYSTATUS);
                db.AddInParameter(savecommand, "TokenNo", System.Data.DbType.String, driveractivity.TokenNo);
                db.AddInParameter(savecommand, "DriverID", System.Data.DbType.String, driveractivity.DriverId);
                db.AddInParameter(savecommand, "Password", System.Data.DbType.String, null);
                db.AddInParameter(savecommand, "IsOnDuty", System.Data.DbType.Boolean, driveractivity.IsOnDuty);
                db.AddInParameter(savecommand, "IsLogIn", System.Data.DbType.String, driveractivity.IsLogIn);
                db.AddInParameter(savecommand, "Latitude", System.Data.DbType.String, driveractivity.Latitude);
                db.AddInParameter(savecommand, "Longitude", System.Data.DbType.String, driveractivity.Longitude);
                db.AddOutParameter(savecommand, "NewTokenNo", System.Data.DbType.String, 50);


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

    }
}
