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
    public class AddressDAL
    {
        private Database db;
        private DbTransaction currentTransaction = null;
        private DbConnection connection = null;
        /// <summary>
        /// Constructor
        /// </summary>
        public AddressDAL()
        {

            db = DatabaseFactory.CreateDatabase("PickC");

        }

        #region IDataFactory Members

        public List<Address> GetList(string addressLinkID)
        {
            return db.ExecuteSprocAccessor(DBRoutine.LISTADDRESS, MapBuilder<Address>.BuildAllProperties(), addressLinkID).ToList();
        }

        public bool Save<T>(T item, DbTransaction parentTransaction) where T : IContract
        {
            currentTransaction = parentTransaction;
            return Save(item);

        }

        public bool Save<T>(T item) where T : IContract
        {
            var result = 0;
            var address = (Address)(object)item;

            if (currentTransaction == null)
            {
                connection = db.CreateConnection();
                connection.Open();
            }

            var transaction = (currentTransaction == null ? connection.BeginTransaction() : currentTransaction);

            try
            {

                var savecommand = db.GetStoredProcCommand(DBRoutine.SAVEADDRESS);
                db.AddInParameter(savecommand, "AddressId", System.Data.DbType.Int32, address.AddressId);
                db.AddInParameter(savecommand, "AddressLinkID", System.Data.DbType.String, address.AddressLinkId ?? "");
                db.AddInParameter(savecommand, "SeqNo", System.Data.DbType.Int16, address.SeqNo);
                db.AddInParameter(savecommand, "AddressType", System.Data.DbType.String, address.AddressType ?? "");
                db.AddInParameter(savecommand, "Address1", System.Data.DbType.String, address.Address1 ?? "");
                db.AddInParameter(savecommand, "Address2", System.Data.DbType.String, address.Address2 ?? "");
                db.AddInParameter(savecommand, "Address3", System.Data.DbType.String, address.Address3 ?? "");
                db.AddInParameter(savecommand, "Address4", System.Data.DbType.String, address.Address4 ?? "");
                db.AddInParameter(savecommand, "CityName", System.Data.DbType.String, address.CityName ?? "");
                db.AddInParameter(savecommand, "StateName", System.Data.DbType.String, address.StateName ?? "");
                db.AddInParameter(savecommand, "CountryCode", System.Data.DbType.String, address.CountryCode ?? "");
                db.AddInParameter(savecommand, "ZipCode", System.Data.DbType.String, address.ZipCode ?? "");
                db.AddInParameter(savecommand, "TelNo", System.Data.DbType.String, address.TelNo ?? "");
                db.AddInParameter(savecommand, "FaxNo", System.Data.DbType.String, address.FaxNo ?? "");
                db.AddInParameter(savecommand, "MobileNo", System.Data.DbType.String, address.MobileNo ?? "");
                db.AddInParameter(savecommand, "Contact", System.Data.DbType.String, address.Contact ?? "");
                db.AddInParameter(savecommand, "Email", System.Data.DbType.String, address.Email ?? "");
                db.AddInParameter(savecommand, "WebSite", System.Data.DbType.String, address.WebSite ?? "");
                db.AddInParameter(savecommand, "IsBilling", System.Data.DbType.Boolean, address.IsBilling);
                db.AddInParameter(savecommand, "IsActive", System.Data.DbType.Boolean, address.IsActive);
                db.AddInParameter(savecommand, "CreatedBy", System.Data.DbType.String, address.CreatedBy);
                db.AddInParameter(savecommand, "ModifiedBy", System.Data.DbType.String, address.ModifiedBy);


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
            //finally
            //{
            //    transaction.Dispose();
            //    connection.Close();
            //}

            return (result > 0 ? true : false);

        }

        public bool Delete<T>(T item) where T : IContract
        {
            var result = false;
            var address = (Address)(object)item;

            var connnection = db.CreateConnection();
            connnection.Open();

            var transaction = connnection.BeginTransaction();

            try
            {
                var deleteCommand = db.GetStoredProcCommand(DBRoutine.DELETEADDRESS);
                db.AddInParameter(deleteCommand, "AddressId", System.Data.DbType.Int32, address.AddressId);


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
            var item = ((Address)lookupItem);

            var addressItem = db.ExecuteSprocAccessor(DBRoutine.SELECTADDRESS,
                                                    MapBuilder<Address>.BuildAllProperties(),
                                                    item.AddressId).FirstOrDefault();

            if (addressItem == null) return null;

            return addressItem;
        }

        #endregion

    }
}
