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
  public class OperatorLogInDAL
    {
        private Database db;
        private DbTransaction currentTransaction = null;
        private DbConnection connection = null;
        /// <summary>
        /// Constructor
        /// </summary>
        public OperatorLogInDAL()
        {

            db = DatabaseFactory.CreateDatabase("PickC");

        }

        #region IDataFactory Members
        public string OperatorLogIn(string mobileNo, string password)
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

                var savecommand = db.GetStoredProcCommand(DBRoutine.OPERATORLOGIN);

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
        public bool AuthUser<T>(T item, DbTransaction parentTransaction) where T : IContract
        {
            currentTransaction = parentTransaction;
            return true;
        }

        public bool AuthUser<T>(T item) where T : IContract
        {
            var result = 0;
            var customerLogin = (OperatorLogIn)(object)item;

            if (currentTransaction == null)
            {
                connection = db.CreateConnection();
                connection.Open();
            }

            var transaction = (currentTransaction == null ? connection.BeginTransaction() : currentTransaction);

            try
            {
                var authcmd = db.GetStoredProcCommand(DBRoutine.OPERATORAUTHENTICATEUSER);

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

                throw;
            }
            finally
            {
                transaction.Dispose();
                connection.Close();
            }

            return (result > 0 ? true : false);
        }
        public List<OperatorMonitor> GetOperatorDriverMonitor(string MobileNo)
        {
            return db.ExecuteSprocAccessor(DBRoutine.OPERATORTRIPMONITOR, MapBuilder<OperatorMonitor>.BuildAllProperties(), MobileNo).ToList();
        }
        public bool DeleteOperatorLogIn<T>(T item) where T : IContract
        {
            var result = false;
            var operatorLogin = (OperatorLogIn)(object)item;

            var connection = db.CreateConnection();
            connection.Open();

            var transaction = connection.BeginTransaction();

            try
            {
                var deleteCommand = db.GetStoredProcCommand(DBRoutine.DELETEOPERATORLOGIN);

                db.AddInParameter(deleteCommand, "TokenNo", System.Data.DbType.String, operatorLogin.TokenNo);

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

        #endregion

    }
}
