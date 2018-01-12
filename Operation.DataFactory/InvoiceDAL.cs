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
    public class InvoiceDAL
    {
        private Database db;
        private DbTransaction currentTransaction = null;
        private DbConnection connection = null;
        /// <summary>
        /// Constructor
        /// </summary>
        public InvoiceDAL()
        {

            db = DatabaseFactory.CreateDatabase("PickC");

        }

        #region IDataFactory Members

        public List<Invoice> GetList()
        {
            return db.ExecuteSprocAccessor(DBRoutine.LISTINVOICE, MapBuilder<Invoice>.BuildAllProperties()).ToList();
        }




        public bool Save<T>(T item, DbTransaction parentTransaction) where T : IContract
        {
            currentTransaction = parentTransaction;
            return Save(item);

        }

        public bool Save<T>(T item) where T : IContract
        {
            var result = 0;
            var invoice = (Invoice)(object)item;

            if (currentTransaction == null)
            {
                connection = db.CreateConnection();
                connection.Open();
            }

            var transaction = (currentTransaction == null ? connection.BeginTransaction() : currentTransaction);

            try
            {

                var savecommand = db.GetStoredProcCommand(DBRoutine.SAVEINVOICE);
                db.AddInParameter(savecommand, "InvoiceNo", System.Data.DbType.String, invoice.InvoiceNo);
                db.AddInParameter(savecommand, "TripID", System.Data.DbType.String, invoice.TripID);
                db.AddInParameter(savecommand, "InvoiceDate", System.Data.DbType.DateTime, invoice.InvoiceDate);
                db.AddInParameter(savecommand, "TripAmount", System.Data.DbType.Decimal, invoice.TripAmount);
                db.AddInParameter(savecommand, "TaxAmount", System.Data.DbType.Decimal, invoice.TaxAmount);
                db.AddInParameter(savecommand, "TotalAmount", System.Data.DbType.Decimal, invoice.TotalAmount);
                db.AddInParameter(savecommand, "PaymentType", System.Data.DbType.Int16, invoice.PaymentType);
                db.AddInParameter(savecommand, "PaidAmount", System.Data.DbType.Decimal, invoice.PaidAmount);
                db.AddInParameter(savecommand, "IsMailSent", System.Data.DbType.Boolean, invoice.IsMailSent);
                db.AddOutParameter(savecommand, "NewInvoiceNo", System.Data.DbType.String, 50);


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
            var invoice = (Invoice)(object)item;

            var connnection = db.CreateConnection();
            connnection.Open();

            var transaction = connnection.BeginTransaction();

            try
            {
                var deleteCommand = db.GetStoredProcCommand(DBRoutine.DELETEINVOICE);
                db.AddInParameter(deleteCommand, "InvoiceNo", System.Data.DbType.String, invoice.InvoiceNo);
                db.AddInParameter(deleteCommand, "TripID", System.Data.DbType.String, invoice.TripID);


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
            var item = ((Invoice)lookupItem);

            var invoiceItem = db.ExecuteSprocAccessor(DBRoutine.SELECTINVOICE,
                                                    MapBuilder<Invoice>.BuildAllProperties(),
                                                    item.InvoiceNo, item.TripID).FirstOrDefault();

            if (invoiceItem == null) return null;

            return invoiceItem;
        }


        public Invoice GetInvoiceByBookingNo(string bookingNo) 
        {
            

            var invoiceItem = db.ExecuteSprocAccessor(DBRoutine.SELECTINVOICEBYBOOKINGNO,
                                                    MapBuilder<Invoice>.BuildAllProperties(),
                                                    bookingNo).FirstOrDefault();

            if (invoiceItem == null) return null;

            return invoiceItem;
        }


        

        #endregion

    }
}
