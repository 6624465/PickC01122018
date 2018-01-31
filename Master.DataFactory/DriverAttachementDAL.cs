
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Master.Contract;

namespace Master.DataFactory
{
    public class DriverAttachementDAL
    {
        private Database db;
        private DbTransaction currentTransaction = null;
        private DbConnection connection = null;
        /// <summary>
       public DriverAttachementDAL()
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
        var driverAttachment = (DriverAttachment)(object)item;
        var connection = db.CreateConnection();
        connection.Open();

        var transaction = connection.BeginTransaction();
        try
        {
            var saveCommand = db.GetStoredProcCommand(DBRoutine.SAVEDRIVERATTACHMENTS);
            db.AddInParameter(saveCommand, "AttachmentId", System.Data.DbType.String, driverAttachment.AttachmentId);
            db.AddInParameter(saveCommand, "DriverID", System.Data.DbType.String, driverAttachment.DriverId);
            db.AddInParameter(saveCommand, "LookupCode", System.Data.DbType.String, driverAttachment.LookupCode);
            db.AddInParameter(saveCommand, "ImagePath", System.Data.DbType.String, driverAttachment.ImagePath);

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
        public List<DriverAttachment> GetList(string DriverID)
        {
            return db.ExecuteSprocAccessor(DBRoutine.SELECTDRIVERATTACHMENTSLIST,
                MapBuilder<DriverAttachment>.BuildAllProperties(), DriverID).ToList();
        }
        #endregion
    }
}
