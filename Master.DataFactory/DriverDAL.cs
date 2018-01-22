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
    public class DriverDAL
    {
        private Database db;
        private DbTransaction currentTransaction = null;
        private DbConnection connection = null;
        /// <summary>
        /// Constructor
        /// </summary>
        public DriverDAL()
        {

            db = DatabaseFactory.CreateDatabase("PickC");

        }

        #region IDataFactory Members

        public List<Driver> GetList()
        {
            return db.ExecuteSprocAccessor(DBRoutine.LISTDRIVER, MapBuilder<Driver>.BuildAllProperties()).ToList();
        }
        public List<Driver> GetList(short status)
        {
            return db.ExecuteSprocAccessor(DBRoutine.LISTDRIVER, MapBuilder<Driver>.BuildAllProperties()).ToList();
        }
        public List<DriverDetails> GetDriversDetailList()
        {
            return db.ExecuteSprocAccessor(DBRoutine.DRIVERDETAILLIST, MapBuilder<DriverDetails>.BuildAllProperties()).ToList();
        }
        public List<Driver> GetOperatorWiseDriverAttachList(string MobileNo)
        {
            return db.ExecuteSprocAccessor(DBRoutine.LISTDRIVEROPERATORWISEATTACH, MapBuilder<Driver>.BuildAllProperties(),MobileNo).ToList();
        }
        public List<Driver> GetOperatorWiseDriversList(string MobileNo)
        {
            return db.ExecuteSprocAccessor(DBRoutine.LISTOFTOTALDRIVEROPERATORWISE, MapBuilder<Driver>.BuildAllProperties(), MobileNo).ToList();
        }

        public bool Save<T>(T item, DbTransaction parentTransaction) where T : IContract
        {
            currentTransaction = parentTransaction;
            return Save(item);

        }

        public bool Save<T>(T item) where T : IContract
        {
            var result = 0;
            var driver = (Driver)(object)item;

            if (currentTransaction == null)
            {
                connection = db.CreateConnection();
                connection.Open();
            }

            var transaction = (currentTransaction == null ? connection.BeginTransaction() : currentTransaction);

            try
            {

                var savecommand = db.GetStoredProcCommand(DBRoutine.SAVEDRIVER);
                db.AddInParameter(savecommand, "DriverID", System.Data.DbType.String, driver.DriverId);
                db.AddInParameter(savecommand, "DriverName", System.Data.DbType.String, driver.DriverName);
                db.AddInParameter(savecommand, "Password", System.Data.DbType.String, driver.Password);
                db.AddInParameter(savecommand, "VehicleNo", System.Data.DbType.String, "");
                db.AddInParameter(savecommand, "FatherName", System.Data.DbType.String, driver.FatherName);
                db.AddInParameter(savecommand, "DateOfBirth", System.Data.DbType.DateTime, driver.DateOfBirth);
                db.AddInParameter(savecommand, "PlaceOfBirth", System.Data.DbType.String, driver.PlaceOfBirth);
                db.AddInParameter(savecommand, "Gender", System.Data.DbType.Int16, driver.Gender);
                db.AddInParameter(savecommand, "MaritialStatus", System.Data.DbType.Int16, driver.MaritialStatus);
                db.AddInParameter(savecommand, "MobileNo", System.Data.DbType.String, driver.MobileNo);
                db.AddInParameter(savecommand, "PhoneNo", System.Data.DbType.String, driver.PhoneNo);
                db.AddInParameter(savecommand, "PANNo", System.Data.DbType.String, driver.PANNo);
                db.AddInParameter(savecommand, "AadharCardNo", System.Data.DbType.String, driver.AadharCardNo);
                db.AddInParameter(savecommand, "LicenseNo", System.Data.DbType.String, driver.LicenseNo);
                //db.AddInParameter(savecommand, "Status", System.Data.DbType.Boolean, driver.Status);
                db.AddInParameter(savecommand, "CreatedBy", System.Data.DbType.String, driver.CreatedBy);
                db.AddInParameter(savecommand, "ModifiedBy", System.Data.DbType.String, driver.ModifiedBy);
                db.AddInParameter(savecommand, "Nationality", System.Data.DbType.String, driver.Nationality ?? "Indian");
                db.AddInParameter(savecommand, "OperatorID", System.Data.DbType.String, driver.OperatorId);
                //db.AddInParameter(savecommand, "DeviceID", System.Data.DbType.String, driver.DeviceID);
     
                db.AddInParameter(savecommand, "MobileMake", System.Data.DbType.String, driver.MobileMake);
                db.AddInParameter(savecommand, "ModelNo", System.Data.DbType.String, driver.ModelNo);
                db.AddInParameter(savecommand, "DateofIssue", System.Data.DbType.DateTime, driver.DateofIssue);
                db.AddInParameter(savecommand, "DateofReturn", System.Data.DbType.DateTime, driver.DateofReturn);
                db.AddOutParameter(savecommand, "NewDocumentNo", System.Data.DbType.String, 50);


                result = db.ExecuteNonQuery(savecommand, transaction);
                
                if (result > 0)
                {
                    var newDocumentNo = savecommand.Parameters["@NewDocumentNo"].Value.ToString();
                    // var newDocumentNo = db.GetParameterValue(savecommand, "NewDocumentNo").ToString();
                    if (driver.driverAttachment != null && driver.driverAttachment.Count > 0)
                    {
                        foreach (var driverAttachment in driver.driverAttachment)
                        {
                            driverAttachment.DriverId = driver.OperatorId;
                            driverAttachment.AttachmentId = driver.OperatorId + driverAttachment.LookupCode;
                        }
                        result = new DriverAttachementDAL().SaveList(driver.driverAttachment, transaction) == true ? 1 : 0;
                    }
                    //if (driver.operatorDriverList != null&& driver.operatorDriverList.Count > 0)
                    //{
                    //    result = new DriverAttachementDAL().SaveList(driver.driverAttachment, transaction) == true ? 1 : 0;
                    //}
                    if (driver.AddressList != null && driver.AddressList.Count > 0)
                    {
                        foreach (var addressItem in driver.AddressList)
                        {
                            addressItem.AddressLinkId = newDocumentNo;
                        }


                        driver.AddressList.ForEach(x =>
                        {
                            result = new AddressDAL().Save(x, transaction) == true ? 1 : 0;
                        });
                    }
                    if (currentTransaction == null)
                        transaction.Commit();
                }
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
        public bool SaveDriverReferral<T>(T item, DbTransaction parentTransaction) where T : IContract
        {
            currentTransaction = parentTransaction;
            return SaveDriverReferral(item);

        }

        public bool SaveDriverReferral<T>(T item) where T : IContract
        {
            var result = 0;
            var driverReferral = (DriverReferral)(object)item;

            if (currentTransaction == null)
            {
                connection = db.CreateConnection();
                connection.Open();
            }

            var transaction = (currentTransaction == null ? connection.BeginTransaction() : currentTransaction);

            try
            {

                var savecommand = db.GetStoredProcCommand(DBRoutine.SAVEREFERRALDRIVER);
                db.AddInParameter(savecommand, "ReferralId", System.Data.DbType.Int64, driverReferral.ReferralId);
                db.AddInParameter(savecommand, "Name", System.Data.DbType.String, driverReferral.Name);
                db.AddInParameter(savecommand, "Mobile", System.Data.DbType.String, driverReferral.Mobile);
                db.AddInParameter(savecommand, "EmailID", System.Data.DbType.String, driverReferral.EmailID);
                db.AddInParameter(savecommand, "DriverID", System.Data.DbType.String, driverReferral.DriverID);
                db.AddInParameter(savecommand, "Status", System.Data.DbType.Boolean, driverReferral.Status);
                db.AddInParameter(savecommand, "CreatedBy", System.Data.DbType.String, driverReferral.CreatedBy ?? "ADMIN");
                db.AddInParameter(savecommand, "ModifiedBy", System.Data.DbType.String, driverReferral.ModifiedBy ?? "ADMIN");
                //db.AddInParameter(savecommand, "OTP", System.Data.DbType.String, driverReferral.OTP);
                //db.AddInParameter(savecommand, "IsOTPVerified", System.Data.DbType.Boolean, driverReferral.IsOTPVerified);
                //db.AddInParameter(savecommand, "OTPSendDate", System.Data.DbType.DateTime, driverReferral.OTPSendDate);
                //db.AddInParameter(savecommand, "OTPVerifiedDate", System.Data.DbType.DateTime, driverReferral.OTPVerifiedDate);
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
        public bool UpdateDriverPassword<T>(T item) where T : IContract
        {
            var result = false;
            var driverpassword = (DriverPassword)(object)item;

            var connnection = db.CreateConnection();
            connnection.Open();

            var transaction = connnection.BeginTransaction();

            try
            {
                var deleteCommand = db.GetStoredProcCommand(DBRoutine.DRIVERUPDATEPASSWORD);
                db.AddInParameter(deleteCommand, "DriverID", System.Data.DbType.String,driverpassword.DriverID);
                db.AddInParameter(deleteCommand, "Password", System.Data.DbType.String, driverpassword.Password);
                db.AddInParameter(deleteCommand, "NewPassword", System.Data.DbType.String, driverpassword.NewPassword);
              
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
        public bool Delete<T>(T item) where T : IContract
        {
            var result = false;
            var driver = (Driver)(object)item;

            var connnection = db.CreateConnection();
            connnection.Open();

            var transaction = connnection.BeginTransaction();

            try
            {
                var deleteCommand = db.GetStoredProcCommand(DBRoutine.DELETEDRIVER);
                db.AddInParameter(deleteCommand, "DriverID", System.Data.DbType.String, driver.DriverId);

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
            var item = ((Driver)lookupItem);

            var driverItem = db.ExecuteSprocAccessor(DBRoutine.SELECTDRIVER,
                                                    MapBuilder<Driver>
                                                    .MapAllProperties()
                                                    .DoNotMap(x => x.Nationality)
                                                    .Build(),item.DriverId).FirstOrDefault();

            if (driverItem == null) return null;


            driverItem.AddressList = new AddressDAL().GetList(driverItem.DriverId);

            return driverItem;
        }
        public IContract GetDriverRating<T>(IContract lookupItem) where T : IContract
        {
            var item = ((DriverRating)lookupItem);

            DriverRating driverItem = db.ExecuteSprocAccessor(DBRoutine.SELECTDRIVERAVERAGERATING,
                                                    MapBuilder<DriverRating>
                                                    .MapAllProperties().Build(),item.DriverId).FirstOrDefault();

            if (driverItem == null) return null;
               return driverItem;
        }
        public List<DriverEarningPaymentType> GetDriverTripAmountbyPaymentType(string DriverID,DateTime FromDate,DateTime ToDate)
        {
            return db.ExecuteSprocAccessor(DBRoutine.GETDRIVERTRIPAMOUNTBYPAYMENTTYPE,MapBuilder<DriverEarningPaymentType>.MapAllProperties().Build(),DriverID,FromDate,ToDate).ToList();
        }
        public List<DriverWiseDailyAmountPaymentTypeList> DriverTripAmountbyPaymentTypeListOfTrips(string DriverID, DateTime FromDate, DateTime ToDate, int PaymentType)
        {
            return db.ExecuteSprocAccessor(DBRoutine.DRIVERWISEDAILYAMOUNTPAYMENTTYPELIST, MapBuilder<DriverWiseDailyAmountPaymentTypeList>.MapAllProperties().Build(), DriverID, FromDate, ToDate, PaymentType).ToList();
        }
        public List<DriverAttachmentListStatus> GetDriverBySearch(string status)
        {

            List<DriverAttachmentListStatus> list = db.ExecuteSprocAccessor(DBRoutine.DRIVERLISTBYLOGINSTATUS,
                                                       MapBuilder<DriverAttachmentListStatus>.BuildAllProperties(), status).ToList();
            return list;
        }

        #endregion
        //public List<Driver> GetDriverByName<T>(IContract lookupItem) where T : IContract
        //{
        //    var item = ((Driver)lookupItem);

        //    List<Driver> list = db.ExecuteSprocAccessor(DBRoutine.GETDRIVERBYNAME,
        //                                               MapBuilder<Driver>.BuildAllProperties(), item.Status).ToList();
        //    return list;
        //}

        public bool SaveAttachment(DriverAttachmentsDTO attachment)
        {
            var result = false;
            var connection = db.CreateConnection();
            connection.Open();

            var transaction = connection.BeginTransaction();
            try
            {
                var saveCommand = db.GetStoredProcCommand(DBRoutine.SAVEATTACHMENTS);
                db.AddInParameter(saveCommand, "AttachmentId", System.Data.DbType.String, attachment.attachmentId);
                db.AddInParameter(saveCommand, "DrvierID", System.Data.DbType.String, attachment.driverId);
                db.AddInParameter(saveCommand, "LookupCode", System.Data.DbType.String, attachment.lookupCode);
                db.AddInParameter(saveCommand, "ImagePath", System.Data.DbType.String, attachment.imagePath);

                result = Convert.ToBoolean(db.ExecuteNonQuery(saveCommand, transaction));

                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw ex;
            }
            return result;
        }
        public bool UpdateDriverDevice(string driverID, string deviceID)
        {
            var result = false;

            var connnection = db.CreateConnection();
            connnection.Open();

            var transaction = connnection.BeginTransaction();

            try
            {
                var deleteCommand = db.GetStoredProcCommand(DBRoutine.DRIVERUPDATEDEVICEID);
                db.AddInParameter(deleteCommand, "DriverID", System.Data.DbType.String, driverID);
                db.AddInParameter(deleteCommand, "DeviceID", System.Data.DbType.String, deviceID);



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
        public int GetTripCount(string MobileNo)
        {
            int result = 0;

            var connnection = db.CreateConnection();
            connnection.Open();

            var transaction = connnection.BeginTransaction();

            try
            {
                var tripCount = db.GetStoredProcCommand(DBRoutine.OPERATORTRIPCOUNT);
                db.AddInParameter(tripCount, "MobileNo", System.Data.DbType.String, MobileNo);
                result =Convert.ToInt32(db.ExecuteScalar(tripCount, transaction));

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
        public decimal GetTripAmount(string MobileNo)
        {
            decimal result = 0m;

            var connnection = db.CreateConnection();
            connnection.Open();

            var transaction = connnection.BeginTransaction();

            try
            {
                var tripAmount = db.GetStoredProcCommand(DBRoutine.OPERATORTRIPAMOUNT);
                db.AddInParameter(tripAmount, "MobileNo", System.Data.DbType.String, MobileNo);
                result = Convert.ToDecimal(db.ExecuteScalar(tripAmount, transaction));

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
        public int GetTripCountbyDriverID(string DriverID,DateTime FromDate,DateTime ToDate)
        {
            int result = 0;

            var connnection = db.CreateConnection();
            connnection.Open();

            var transaction = connnection.BeginTransaction();

            try
            {
                var tripCount = db.GetStoredProcCommand(DBRoutine.DRIVERTRIPCOUNTBYDRIVERID);
                db.AddInParameter(tripCount, "DriverID", System.Data.DbType.String, DriverID);
                db.AddInParameter(tripCount, "FromDate", System.Data.DbType.String, FromDate);
                db.AddInParameter(tripCount, "ToDate", System.Data.DbType.String, ToDate);
                result = Convert.ToInt32(db.ExecuteScalar(tripCount, transaction));

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
        public decimal GetTripAmountbyDriverID(string DriverID,DateTime FromDate, DateTime ToDate)
        {
            decimal result = 0m;

            var connnection = db.CreateConnection();
            connnection.Open();

            var transaction = connnection.BeginTransaction();

            try
            {
                var tripAmount = db.GetStoredProcCommand(DBRoutine.DRIVERTRIPAMOUNTDAILYWISE);
                db.AddInParameter(tripAmount, "DriverID", System.Data.DbType.String, DriverID);
                db.AddInParameter(tripAmount, "FromDate", System.Data.DbType.String, FromDate);
                db.AddInParameter(tripAmount, "ToDate", System.Data.DbType.String, ToDate);
                result = Convert.ToDecimal(db.ExecuteScalar(tripAmount, transaction));

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
        public List<TripCE> GetTripCountandEarnings(string MobileNo,DateTime fromdate, DateTime todate)
        {
            return db.ExecuteSprocAccessor(DBRoutine.OPERATORTRIPCOUNTAMOUNTBYDATES, MapBuilder<TripCE>.BuildAllProperties(), MobileNo, fromdate, todate).ToList();
        }
        public List<DriverTodayTripList> GetTodayListOfTrips(string DriverID,DateTime fromdate, DateTime todate)
        {
            return db.ExecuteSprocAccessor(DBRoutine.DRIVERTODAYLISTOFTRIPS, MapBuilder<DriverTodayTripList>.BuildAllProperties(), DriverID,fromdate,todate).ToList();
        }
        public List<TripCElist> GetTripCountandEarningsList(string MobileNo, DateTime fromdate, DateTime todate)
        {
            return db.ExecuteSprocAccessor(DBRoutine.OPERATORTRIPCOUNTAMOUNTLISTBYDATES, MapBuilder<TripCElist>.BuildAllProperties(), MobileNo, fromdate, todate).ToList();
        }
        public List<TripCElist> GetDailyTripEarningsList(string MobileNo)
        {
            return db.ExecuteSprocAccessor(DBRoutine.OPERATORTRIPEARNINGLISTDAILY, MapBuilder<TripCElist>.MapAllProperties().DoNotMap(x=>x.LocationFrom).DoNotMap(x=>x.LocationTo).DoNotMap(x=>x.VehicleType).Build(), MobileNo).ToList();
        }
        public List<TripCountList> GetDailyTripCountList(string MobileNo)
        {
            return db.ExecuteSprocAccessor(DBRoutine.OPERATORTRIPCOUNTLISTDAILY, MapBuilder<TripCountList>.BuildAllProperties(), MobileNo).ToList();
        }
        public bool SaveDriverRating<T>(T item, DbTransaction parentTransaction) where T : IContract
        {
            currentTransaction = parentTransaction;
            return SaveDriverRating(item);

        }

        public bool SaveDriverRating<T>(T item) where T : IContract
        {
            var result = 0;
            DriverRating driverRating = (DriverRating)(object)item;

            if (currentTransaction == null)
            {
                connection = db.CreateConnection();
                connection.Open();
            }

            var transaction = (currentTransaction == null ? connection.BeginTransaction() : currentTransaction);

            try
            {

                var savecommand = db.GetStoredProcCommand(DBRoutine.SAVEDRIVERRATING);
                db.AddInParameter(savecommand, "BookingNo", System.Data.DbType.String, driverRating.BookingNo);
                db.AddInParameter(savecommand, "DriverID", System.Data.DbType.String, driverRating.DriverId);
                db.AddInParameter(savecommand, "Rating", System.Data.DbType.Int32, driverRating.Rating);
                db.AddInParameter(savecommand, "Remarks", System.Data.DbType.String, driverRating.Remarks);

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
        public IContract GetDriverTripInvoice<T>(IContract lookupItem) where T : IContract
        {
            var item = ((DriverTripInvoice)lookupItem);

            var driverItem = db.ExecuteSprocAccessor(DBRoutine.DRIVERTRIPINVOICE,
                                                    MapBuilder<DriverTripInvoice>.BuildAllProperties(),
                                                    item.BookingNo).FirstOrDefault();

            if (driverItem == null) return null;

            return driverItem;
        }

    }
}
