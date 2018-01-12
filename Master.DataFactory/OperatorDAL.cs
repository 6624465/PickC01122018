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
using Operation.DataFactory;


namespace Master.DataFactory
{
    public class OperatorDAL
    {
        private Database db;
        private DbTransaction currentTransaction = null;
        private DbConnection connection = null;
        /// <summary>
        /// Constructor
        /// </summary>
        public OperatorDAL()
        {

            db = DatabaseFactory.CreateDatabase("PickC");

        }
        public List<Operator> GetList()
        {
            return db.ExecuteSprocAccessor(DBRoutine.LISTOPERATOR, MapBuilder<Operator>.BuildAllProperties()).ToList();
        }
        public bool Save<T>(T item, DbTransaction parentTransaction) where T : IContract
        {
            currentTransaction = parentTransaction;
            return Save(item);

        }

        public bool Save<T>(T item) where T : IContract
        {
            var result = 0;
            var operatorItem  = (Operator)(object)item;

            if (currentTransaction == null)
            {
                connection = db.CreateConnection();
                connection.Open();
            }

            var transaction = (currentTransaction == null ? connection.BeginTransaction() : currentTransaction);

            try
            {
                var savecommand = db.GetStoredProcCommand(DBRoutine.SAVEOPERATOR);
                db.AddInParameter(savecommand, "OperatorID", System.Data.DbType.String, operatorItem.OperatorID ?? "");
                db.AddInParameter(savecommand, "OperatorName", System.Data.DbType.String, operatorItem.OperatorName);
                db.AddInParameter(savecommand, "Password", System.Data.DbType.String, operatorItem.Password);
                db.AddInParameter(savecommand, "FatherName", System.Data.DbType.String, operatorItem.FatherName);
                db.AddInParameter(savecommand, "DateOfBirth", System.Data.DbType.DateTime, operatorItem.DateOfBirth);
                db.AddInParameter(savecommand, "PlaceOfBirth", System.Data.DbType.String, operatorItem.PlaceOfBirth);
                db.AddInParameter(savecommand, "Gender", System.Data.DbType.Int16, operatorItem.Gender);
                db.AddInParameter(savecommand, "MaritialStatus", System.Data.DbType.Int16, operatorItem.MaritialStatus);
                db.AddInParameter(savecommand, "MobileNo", System.Data.DbType.String, operatorItem.MobileNo);
                db.AddInParameter(savecommand, "PhoneNo", System.Data.DbType.String, operatorItem.PhoneNo);
                db.AddInParameter(savecommand, "PANNo", System.Data.DbType.String, operatorItem.PANNo);
                db.AddInParameter(savecommand, "AadharCardNo", System.Data.DbType.String, operatorItem.AadharCardNo);
                db.AddInParameter(savecommand, "CreatedBy", System.Data.DbType.String, operatorItem.CreatedBy);
                db.AddInParameter(savecommand, "ModifiedBy", System.Data.DbType.String, operatorItem.ModifiedBy);
                db.AddInParameter(savecommand, "Nationality", System.Data.DbType.String, operatorItem.Nationality ?? "Indian");
                db.AddOutParameter(savecommand, "NewDocumentNo", System.Data.DbType.String, 50);
                result = db.ExecuteNonQuery(savecommand, transaction);
                if (result > 0)
                {
                    operatorItem.OperatorID = savecommand.Parameters["@NewDocumentNo"].Value.ToString();

                    if (operatorItem.operatorAttachment != null && operatorItem.operatorAttachment.Count > 0)
                    {
                        foreach (var operatorAttachment in operatorItem.operatorAttachment)
                        {
                            operatorAttachment.operatorId = operatorItem.OperatorID;
                            operatorAttachment.attachmentId = operatorItem.OperatorID + operatorAttachment.lookupCode;
                        }
                        result = new OperatorAttachementDAL().SaveList(operatorItem.operatorAttachment, transaction) == true ? 1 : 0;
                    }
                    if (operatorItem.BankDetails != null && operatorItem.BankDetails.Count > 0)
                    {
                        operatorItem.BankDetails.ForEach(x =>
                        {   x.OperatorBankID = operatorItem.OperatorID;
                            result = new BankDetailsDAL().Save(x, transaction) == true ? 1 : 0;
                        });
                    }

                    if (operatorItem.OperatorDriverList != null && operatorItem.OperatorDriverList.Count > 0)
                    {
                        operatorItem.OperatorDriverList.ForEach(x =>
                        {
                            x.OperatorDriverId = operatorItem.OperatorID;
                            result = new OperatorDriverDAL().Save(x, transaction) == true ? 1 : 0;
                        });
                    }

                    if (operatorItem.OperatorVehicle != null && operatorItem.OperatorVehicle.Count > 0)
                    {
                        operatorItem.OperatorVehicle.ForEach(x =>
                        {
                            x.OperatorVehicleID = operatorItem.OperatorID;
                            result = new OperatorVehicleDAL().Save(x, transaction) == true ? 1 : 0;
                        });
                    }
                    if (operatorItem.AddressList != null && operatorItem.AddressList.Count > 0)
                    {
                        //foreach (var addressItem in operatorItem.AddressList)
                        //{
                        //    addressItem.AddressLinkID = operatorItem.OperatorID;
                        //}
                        operatorItem.AddressList.ForEach(x =>
                        { x.AddressLinkId= operatorItem.OperatorID;
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
        public bool UpdateOperatorPassword<T>(T item) where T : IContract
        {
            var result = false;
            var operatorpassword = (OperatorPasssword)(object)item;

            var connnection = db.CreateConnection();
            connnection.Open();

            var transaction = connnection.BeginTransaction();

            try
            {
                var deleteCommand = db.GetStoredProcCommand(DBRoutine.OPERATORUPDATEPASSWORD);
                db.AddInParameter(deleteCommand, "MobileNo", System.Data.DbType.String, operatorpassword.MobileNo);
                db.AddInParameter(deleteCommand, "Password", System.Data.DbType.String, operatorpassword.Password);
                db.AddInParameter(deleteCommand, "NewPassword", System.Data.DbType.String, operatorpassword.NewPassword);

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
        public bool SaveOperatorNotifications<T>(T item, DbTransaction parentTransaction) where T : IContract
        {
            currentTransaction = parentTransaction;
            return SaveOperatorNotifications(item);

        }

        public bool SaveOperatorNotifications<T>(T item) where T : IContract
        {
            var result = 0;
            var operatorNotifications = (OperatorNotifications)(object)item;

            if (currentTransaction == null)
            {
                connection = db.CreateConnection();
                connection.Open();
            }

            var transaction = (currentTransaction == null ? connection.BeginTransaction() : currentTransaction);

            try
            {

                var savecommand = db.GetStoredProcCommand(DBRoutine.SAVEOPERATORNOTIFICATIONS);
                db.AddInParameter(savecommand, "DriverID", System.Data.DbType.String, operatorNotifications.DriverID);
                db.AddInParameter(savecommand, "DriverLoginLogoutStatus", System.Data.DbType.String, operatorNotifications.DriverLoginLogoutStatus);
                db.AddInParameter(savecommand, "DriverOnOffDuty", System.Data.DbType.String, operatorNotifications.DriverOnOffDuty);
                db.AddInParameter(savecommand, "DailyIncentiveUpdate", System.Data.DbType.String, operatorNotifications.DailyIncentiveUpdate);
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
        public IContract GetItem<T>(IContract lookupItem) where T : IContract
        {
            var item = ((Operator)lookupItem);

            var operatorItem = db.ExecuteSprocAccessor(DBRoutine.SELECTOPERATOR,
                                                    MapBuilder<Operator>
                                                    .MapAllProperties()
                                                    .DoNotMap(x => x.Nationality).Build(),
                                                    item.MobileNo).FirstOrDefault();

            if (operatorItem == null) return null;


            operatorItem.AddressList = new AddressDAL().GetList(operatorItem.OperatorID);
            operatorItem.OperatorDriverList = new OperatorDriverDAL().GetSelectList(operatorItem.OperatorID);
            operatorItem.OperatorVehicle = new OperatorVehicleDAL().GetOperatorVehicleListById(operatorItem.OperatorID);
            operatorItem.BankDetails = new BankDetailsDAL().GetList(operatorItem.OperatorID);
            return operatorItem;
        }
        public Operator GetOperatorDetails(string OperatorID)
        {
            var operatorItem = db.ExecuteSprocAccessor(DBRoutine.SELECTOPERATORBYOPERATORID,
                                                   MapBuilder<Operator>
                                                    .MapAllProperties()
                                                    .DoNotMap(x => x.Nationality).Build(),
                                                    OperatorID).FirstOrDefault();

            operatorItem.AddressList = new AddressDAL().GetList(operatorItem.OperatorID);
            operatorItem.OperatorDriverList = new OperatorDriverDAL().GetSelectList(operatorItem.OperatorID);
            operatorItem.OperatorVehicle = new OperatorVehicleDAL().GetOperatorVehicleListById(OperatorID);
            operatorItem.BankDetails = new BankDetailsDAL().GetList(operatorItem.OperatorID);

            return operatorItem;
        }

        public bool Delete<T>(T item) where T : IContract
        {
            var result = false;
            var OPerator = (Operator)(object)item;

            var connection = db.CreateConnection();
            connection.Open();

            var transaction = connection.BeginTransaction();

            try
            {
                var deleteCommand = db.GetStoredProcCommand(DBRoutine.DELETEPERATOR);
                db.AddInParameter(deleteCommand, "OperatorID", System.Data.DbType.String, OPerator.OperatorID);

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
        public bool DetachOperatorwisedrivervehicleattachedlist<T>(T item) where T : IContract
        {
            var result = false;
            var operatorWiseDriverVehicleAttachedTodayList = (OperatorWiseDriverVehicleAttachedTodayList)(object)item;

            var connection = db.CreateConnection();

            connection.Open();

            var transaction = connection.BeginTransaction();

            try
            {
                var deleteCommand = db.GetStoredProcCommand(DBRoutine.DETACHOPERATORWISEDRIVERVEHICLEATTACHEDLIST);
                db.AddInParameter(deleteCommand, "DriverName", System.Data.DbType.String, operatorWiseDriverVehicleAttachedTodayList.DriverName);
                db.AddInParameter(deleteCommand, "VehicleattachedNo", System.Data.DbType.String, operatorWiseDriverVehicleAttachedTodayList.VehicleattachedNo);
                db.AddInParameter(deleteCommand, "OperatorMobileNo", System.Data.DbType.String, operatorWiseDriverVehicleAttachedTodayList.OperatorMobileNo);
                db.AddInParameter(deleteCommand, "DriverMobileNo", System.Data.DbType.String, operatorWiseDriverVehicleAttachedTodayList.DriverMobileNo);
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

        public int IsOperatorExixts(string operatorID)
        {
            try
            {

                var result = Convert.ToInt32(db.ExecuteScalar(db.GetStoredProcCommand(DBRoutine.OPERATORVALIDCHECK, operatorID)));

                //if (result > 0)
                //    return 1;
                //else
                //    return 0;
                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<OperatorBankDetails> GetOperatorWiseBankList(string MobileNo)
        {
            return db.ExecuteSprocAccessor(DBRoutine.LISTBANKDETAILSOPERATORWISE, MapBuilder<OperatorBankDetails>.BuildAllProperties(), MobileNo).ToList();
        }
        public List<OperatorWiseDriverVehicleAttachedTodayList> GetOperatorWiseDriverVehicleAttachedTodayList(string MobileNo)
        {
            return db.ExecuteSprocAccessor(DBRoutine.LISTOPERATORWISEDRIVERVEHICLEATTACHEDTODAYLIST, MapBuilder<OperatorWiseDriverVehicleAttachedTodayList>.BuildAllProperties(), MobileNo).ToList();
        }
        #region IDataFactory Members
        public List<Operator> GetOperatorList()
        {
            return db.ExecuteSprocAccessor(DBRoutine.LISTOPERATOR, MapBuilder<Operator>.BuildAllProperties()).ToList();
        }
        
    }
    #endregion
}
