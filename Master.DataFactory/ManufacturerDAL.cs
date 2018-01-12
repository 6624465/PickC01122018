using Microsoft.Practices.EnterpriseLibrary.Data;
using PickC.Services.DTO;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Master.DataFactory
{
    public class ManufacturerDAL
    {
        private Database db;
        private DbTransaction currentTransaction = null;
        private DbConnection connection = null;

        public ManufacturerDAL()
        {
            db = DatabaseFactory.CreateDatabase();
        }

        public bool Save(DriverManufacturerDTO manufacturer)
        {
            var result = false;

            if (currentTransaction == null) {
                connection = db.CreateConnection();
                connection.Open();
            }

            var transaction = (currentTransaction == null ? connection.BeginTransaction() : currentTransaction);
            try
            {
                var saveCommand = db.GetStoredProcCommand(DBRoutine.SAVEMANUFACTURER);

                db.AddInParameter(saveCommand, "Manufacturer",System.Data.DbType.String,manufacturer.Manufacturer);
                db.AddInParameter(saveCommand, "MakeType", System.Data.DbType.String, manufacturer.MakeType);
                db.AddInParameter(saveCommand, "Capacity", System.Data.DbType.Decimal, manufacturer.Capacity);

                result = Convert.ToBoolean(db.ExecuteNonQuery(saveCommand, transaction));
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
            return result;
        }
    }
}
