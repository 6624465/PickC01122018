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
    public class CustomerDAL
    {
        private Database db;
        private DbTransaction currentTransaction = null;
        private DbConnection connection = null;
        /// <summary>
        /// Constructor
        /// </summary>
        public CustomerDAL()
        {

            db = DatabaseFactory.CreateDatabase("PickC");

        }

        #region IDataFactory Members

        public List<Customer> GetList()
        {
            return db.ExecuteSprocAccessor(DBRoutine.LISTCUSTOMER, MapBuilder<Customer>.BuildAllProperties()).ToList();
        }

        public bool Save<T>(T item, DbTransaction parentTransaction) where T : IContract
        {
            currentTransaction = parentTransaction;
            return Save(item);

        }

        public bool Save<T>(T item) where T : IContract
        {
            var result = 0;
            var customer = (Customer)(object)item;

            if (currentTransaction == null)
            {
                connection = db.CreateConnection();
                connection.Open();
            }

            var transaction = (currentTransaction == null ? connection.BeginTransaction() : currentTransaction);

            try
            {

                var savecommand = db.GetStoredProcCommand(DBRoutine.SAVECUSTOMER);
                db.AddInParameter(savecommand, "MobileNo", System.Data.DbType.String, customer.MobileNo);
                db.AddInParameter(savecommand, "Password", System.Data.DbType.String, customer.Password);
                db.AddInParameter(savecommand, "Name", System.Data.DbType.String, customer.Name);
                db.AddInParameter(savecommand, "EmailID", System.Data.DbType.String, customer.EmailId);
                db.AddInParameter(savecommand, "OTP", System.Data.DbType.String, customer.OTP);
                db.AddInParameter(savecommand, "IsOTPVerified", System.Data.DbType.Boolean, customer.IsOTPVerified);
                db.AddInParameter(savecommand, "OTPSendDate", System.Data.DbType.DateTime, customer.OTPSendDate);
                db.AddInParameter(savecommand, "OTPVerifiedDate", System.Data.DbType.DateTime, customer.OTPVerifiedDate);

                //db.AddInParameter(savecommand, "DeviceID", System.Data.DbType.String, customer.DeviceID);



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
        public bool SaveImageDriverDetails<T>(T item) where T : IContract
        {
            var result = 0;
            var driverImageRegister = (DriverImageRegister)(object)item;

            if (currentTransaction == null)
            {
                connection = db.CreateConnection();
                connection.Open();
            }

            var transaction = (currentTransaction == null ? connection.BeginTransaction() : currentTransaction);

            try
            {

                var savecommand = db.GetStoredProcCommand(DBRoutine.SAVEDRIVERIMAGEREGISTER);
                db.AddInParameter(savecommand, "DriverID", System.Data.DbType.String, driverImageRegister.DriverId);
                db.AddInParameter(savecommand, "DriverName", System.Data.DbType.String, driverImageRegister.DriverName);
                db.AddInParameter(savecommand, "MobileNo", System.Data.DbType.String, driverImageRegister.MobileNo);
                db.AddInParameter(savecommand, "CreatedBy", System.Data.DbType.String, driverImageRegister.CreatedBy?? "ADMIN");
                db.AddInParameter(savecommand, "ModifiedBy", System.Data.DbType.String, driverImageRegister.ModifiedBy?? "ADMIN");

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
            var customer = (Customer)(object)item;

            var connnection = db.CreateConnection();
            connnection.Open();

            var transaction = connnection.BeginTransaction();

            try
            {
                var deleteCommand = db.GetStoredProcCommand(DBRoutine.DELETECUSTOMER);
                db.AddInParameter(deleteCommand, "MobileNo", System.Data.DbType.String, customer.MobileNo);


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
            var item = ((Customer)lookupItem);

            var customerItem = db.ExecuteSprocAccessor(DBRoutine.SELECTCUSTOMER,
                                                    MapBuilder<Customer>.BuildAllProperties(),
                                                    item.MobileNo).FirstOrDefault();

            if (customerItem == null) return null;

            return customerItem;
        }

        #endregion

        public bool UpdateCustomerDevice(string mobileNo, string deviceID)
        {
            var result = false;

            var connnection = db.CreateConnection();
            connnection.Open();

            var transaction = connnection.BeginTransaction();

            try
            {
                var deleteCommand = db.GetStoredProcCommand(DBRoutine.CUSTOMERUPDATEDEVICEID);
                db.AddInParameter(deleteCommand, "MobileNo", System.Data.DbType.String, mobileNo);
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



        public bool UpdateCustomerPassword<T>(T item) where T : IContract
        {
            var result = false;
            var customerpassword = (CustomerPassword)(object)item;

            var connnection = db.CreateConnection();
            connnection.Open();

            var transaction = connnection.BeginTransaction();

            try
            {
                var deleteCommand = db.GetStoredProcCommand(DBRoutine.CUSTOMERUPDATEPASSWORD);
                db.AddInParameter(deleteCommand, "MobileNo", System.Data.DbType.String, customerpassword.MobileNo);
                db.AddInParameter(deleteCommand, "Password", System.Data.DbType.String, customerpassword.Password);
                db.AddInParameter(deleteCommand, "NewPassword", System.Data.DbType.String, customerpassword.NewPassword);
                db.AddInParameter(deleteCommand, "OTP", System.Data.DbType.String, customerpassword.OTP);
                db.AddInParameter(deleteCommand, "OTPVerifiedDate", System.Data.DbType.String, customerpassword.OTPVerifiedDate);


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
        public IEnumerable<CustomerBillDetails> GetCustomerPaymentDetails(string BookingNo)
        {
            return db.ExecuteSprocAccessor(DBRoutine.CUSTOMERBILLDETAILS, MapBuilder<CustomerBillDetails>.BuildAllProperties(), BookingNo).ToList();
        }

        public IContract GetTripInvoice<T>(IContract lookupItem) where T : IContract
        {
            var item = ((TripInvoice)lookupItem);

            var customerItem = db.ExecuteSprocAccessor(DBRoutine.TRIPINVOICELIST,
                                                    MapBuilder<TripInvoice>.BuildAllProperties(),
                                                    item.BookingNo).FirstOrDefault();

            if (customerItem == null) return null;

            return customerItem;
        }
        public string GetCustomerPaymentsCheck(string MobileNo)
        {
            var result = "";

            var connection = db.CreateConnection();
            connection.Open();

            var transaction = connection.BeginTransaction();

            try
            {
                var CustomerPaymentsCheck = db.GetStoredProcCommand(DBRoutine.CUSTOMERINVOICEPAYMENTSCHECK);
                db.AddInParameter(CustomerPaymentsCheck, "CustomerMobileNo", System.Data.DbType.String, MobileNo);
                result = (string)db.ExecuteScalar(CustomerPaymentsCheck, transaction);
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
        public IContract GetCompanyTripInvoice<T>(IContract lookupItem) where T : IContract
        {
            var item = ((CompanyTripInvoice)lookupItem);

            var customerItem = db.ExecuteSprocAccessor(DBRoutine.COMPANYTRIPINVOICE,
                                                    MapBuilder<CompanyTripInvoice>.BuildAllProperties(),
                                                    item.BookingNo).FirstOrDefault();

            if (customerItem == null) return null;

            return customerItem;
        }
        public IEnumerable<TripEstimateForCustomer> GetTripEstimateForCustomer(int VehicleType, int VehicleGroup, decimal distance, int LdUdCharges, decimal duration)
        {
            return db.ExecuteSprocAccessor(DBRoutine.TRIPESTIMATEFORCUSTOMER, MapBuilder<TripEstimateForCustomer>.BuildAllProperties(), VehicleType, VehicleGroup, distance, LdUdCharges, duration).ToList();
        }

        public bool SaveContactUs<T>(T item) where T : IContract
        {
            var result = 0;
            var contactUs = (ContactUs)(object)item;

            if (currentTransaction == null)
            {
                connection = db.CreateConnection();
                connection.Open();
            }

            var transaction = (currentTransaction == null ? connection.BeginTransaction() : currentTransaction);

            try
            {

                var savecommand = db.GetStoredProcCommand(DBRoutine.SAVECUSTOMERCONTACTUS);
                db.AddInParameter(savecommand, "Name", System.Data.DbType.String, contactUs.Name);
                db.AddInParameter(savecommand, "Email", System.Data.DbType.String, contactUs.Email);
                db.AddInParameter(savecommand, "MobileNo", System.Data.DbType.String, contactUs.MobileNo);
                db.AddInParameter(savecommand, "Message", System.Data.DbType.String, contactUs.Message);
                db.AddInParameter(savecommand, "Subject", System.Data.DbType.String, contactUs.Subject);
                db.AddInParameter(savecommand, "Type", System.Data.DbType.String, contactUs.Type);
                db.AddInParameter(savecommand, "CreatedBy", System.Data.DbType.String, contactUs.CreatedBy ?? "ADMIN");

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

        public List<CustomerInfo> GetCustomersDetails()
        {
            return db.ExecuteSprocAccessor(DBRoutine.CUSTOMERLISTWITHBOOKING, MapBuilder<CustomerInfo>.BuildAllProperties()).ToList();
        }
    }
}
