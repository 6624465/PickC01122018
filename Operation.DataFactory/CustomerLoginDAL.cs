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
//http://stackoverflow.com/questions/15473907/how-to-close-connection-of-microsoft-practices-enterpriselibrary-data-executenon
namespace Operation.DataFactory
{
    public class CustomerLoginDAL
    {
        private Database db;
        private DbTransaction currentTransaction = null;
        private DbConnection connection = null;
        /// <summary>
        /// Constructor
        /// </summary>
        public CustomerLoginDAL()
        {

            db = DatabaseFactory.CreateDatabase("PickC");

        }

        #region IDataFactory Members

        public List<CustomerLogin> GetList()
        {
            return db.ExecuteSprocAccessor(DBRoutine.LISTCUSTOMERLOGIN, MapBuilder<CustomerLogin>.BuildAllProperties()).ToList();
        }

        public bool Save<T>(T item, DbTransaction parentTransaction) where T : IContract
        {
            currentTransaction = parentTransaction;
            return Save(item);
        }

        public bool AuthUser<T>(T item, DbTransaction parentTransaction) where T : IContract
        {
            currentTransaction = parentTransaction;
            return true;
        }
     

        public bool AuthUser<T>(T item) where T : IContract
        {
            var result = 0;
            var customerLogin = (CustomerLogin)(object)item;

            if(currentTransaction == null)
            {
                connection = db.CreateConnection();
                connection.Open();
            }

            var transaction = (currentTransaction == null ? connection.BeginTransaction() : currentTransaction);

            try
            {
                var authcmd = db.GetStoredProcCommand(DBRoutine.AUTHENTICATEUSER);

                db.AddInParameter(authcmd, "TokenNo", System.Data.DbType.String, customerLogin.TokenNo);
                db.AddInParameter(authcmd, "mobileNo", System.Data.DbType.String, customerLogin.MobileNo);

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

        

        public bool Save<T>(T item) where T : IContract
        {
            var result = 0;
            var customerlogin = (CustomerLogin)(object)item;

            if (currentTransaction == null)
            {
                connection = db.CreateConnection();
                connection.Open();
            }

            var transaction = (currentTransaction == null ? connection.BeginTransaction() : currentTransaction);

            try
            {

                var savecommand = db.GetStoredProcCommand(DBRoutine.SAVECUSTOMERLOGIN);

                db.AddInParameter(savecommand, "TokenNo", System.Data.DbType.String, customerlogin.TokenNo);
                db.AddInParameter(savecommand, "MobileNo", System.Data.DbType.String, customerlogin.MobileNo);


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
            var customerlogin = (CustomerLogin)(object)item;

            var connection = db.CreateConnection();
            connection.Open();

            var transaction = connection.BeginTransaction();

            try
            {
                var deleteCommand = db.GetStoredProcCommand(DBRoutine.DELETECUSTOMERLOGIN);

                db.AddInParameter(deleteCommand, "TokenNo", System.Data.DbType.String, customerlogin.TokenNo);

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
            var item = ((CustomerLogin)lookupItem);

            var customerloginItem = db.ExecuteSprocAccessor(DBRoutine.SELECTCUSTOMERLOGIN,
                                                    MapBuilder<CustomerLogin>.BuildAllProperties(),
                                                    item.TokenNo).FirstOrDefault();

            if (customerloginItem == null) return null;

            return customerloginItem;
        }

        public string CustomerLogIn(string mobileNo, string password)
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

                var savecommand = db.GetStoredProcCommand(DBRoutine.CUSTOMERLOGIN);

                db.AddInParameter(savecommand, "MobileNo", System.Data.DbType.String, mobileNo);
                db.AddInParameter(savecommand, "Password", System.Data.DbType.String, password);
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

        #endregion

    }
}
