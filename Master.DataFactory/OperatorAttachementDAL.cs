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
  public  class OperatorAttachementDAL
    {
        private Database db;
        private DbTransaction currentTransaction = null;
        private DbConnection connection = null;
        /// <summary>
        /// Constructor
        /// </summary>
        public OperatorAttachementDAL()
        {

            db = DatabaseFactory.CreateDatabase("PickC");

        }

        #region IDataFactory Members
        public bool SaveList<T>(List<T> items, DbTransaction parentTransaction) where T : IContract
        {
            var result = true;

            if (items.Count == 0)
                result = true;

            foreach (var item in items)
            {
                result = Save(item, parentTransaction);
                if (result == false) break;
            }
          return result;

        }
        public bool Save<T>(T item, DbTransaction parentTransaction) where T : IContract
        {
            currentTransaction = parentTransaction;
            return Save(item);

        }
        public bool Save<T>(T item)
        {
            var result = false;
            var operatorAttachment = (OperatorAttachment)(object)item;
            var connection = db.CreateConnection();
            connection.Open();

            var transaction = connection.BeginTransaction();
            try
            {
                var saveCommand = db.GetStoredProcCommand(DBRoutine.SAVEOPERATORATTACHMENTS);
                db.AddInParameter(saveCommand, "AttachmentId", System.Data.DbType.String,operatorAttachment.attachmentId);
                db.AddInParameter(saveCommand, "OperatorID", System.Data.DbType.String, operatorAttachment.operatorId);
                db.AddInParameter(saveCommand, "LookupCode", System.Data.DbType.String, operatorAttachment.lookupCode);
                db.AddInParameter(saveCommand, "ImagePath", System.Data.DbType.String, operatorAttachment.imagePath);

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
        #endregion
    }
}
