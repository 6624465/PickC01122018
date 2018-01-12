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
  public class BankDetailsDAL
    {
        private Database db;
        private DbTransaction currentTransaction = null;
        private DbConnection connection = null;
        /// <summary>
        /// Constructor
        /// </summary>
        public BankDetailsDAL()
        {

            db = DatabaseFactory.CreateDatabase("PickC");

        }
        #region IDataFactory Members

        public List<BankDetails> GetList(string OperatorBankID)
        {
            return db.ExecuteSprocAccessor(DBRoutine.SELECTBANKDETAILS, MapBuilder<BankDetails>.BuildAllProperties(), OperatorBankID).ToList();
        }
        public bool Save<T>(T item, DbTransaction parentTransaction) where T : IContract
        {
            currentTransaction = parentTransaction;
            return Save(item);

        }
        public bool Save<T>(T item)
        {
            var result = 0;
            var bankDetailsList = (BankDetails)(object)item;

            if (currentTransaction == null)
            {
                connection = db.CreateConnection();
                connection.Open();
            }

            var transaction = (currentTransaction == null ? connection.BeginTransaction() : currentTransaction);

            try
            {
                var savecommand = db.GetStoredProcCommand(DBRoutine.SAVEBANKDETAILS);
                db.AddInParameter(savecommand, "OperatorBankID", System.Data.DbType.String, bankDetailsList.OperatorBankID);
                db.AddInParameter(savecommand, "BankName", System.Data.DbType.String, bankDetailsList.BankName);
                db.AddInParameter(savecommand, "Branch", System.Data.DbType.String, bankDetailsList.Branch);
                db.AddInParameter(savecommand, "AccountNumber", System.Data.DbType.String, bankDetailsList.AccountNumber);
                db.AddInParameter(savecommand, "AccountType", System.Data.DbType.String, bankDetailsList.AccountType);
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
            return (result > 0 ? true : false);

        }
    }
    #endregion
}

