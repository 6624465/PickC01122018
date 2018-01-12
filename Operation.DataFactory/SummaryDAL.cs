using Microsoft.Practices.EnterpriseLibrary.Data;
using Operation.Contract;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PickC.Services.DTO;

namespace Operation.DataFactory
{
    public class SummaryDAL
    {
        private Database db;
        private DbTransaction currentTransaction = null;
        private DbConnection connection = null;

        public SummaryDAL()
        {
            db = DatabaseFactory.CreateDatabase("PickC");
        }

        public IContract GetSummary<T>(string DriverID) where T:Summary
        {
            return db.ExecuteSprocAccessor(DBRoutine.DRIVERSUMMARY,MapBuilder<Summary>.BuildAllProperties(), DriverID).FirstOrDefault();
        }

        public List<DriverPayments> GetPayments(string DriverID)
        {
            return db.ExecuteSprocAccessor(DBRoutine.DRIVERPAYMENTS,MapBuilder<DriverPayments>.BuildAllProperties(), DriverID).ToList();
        }
        public PaymentHistory getPaymentDetails(DateTime datefrom,DateTime dateto)
        {
            var payment = new PaymentHistory();
            payment.customerdetails = new List<CustomerDetails>();
            payment.pickCCommissiondetails = new List<pickCCommissionDetails>();
            payment.driverCommissiondetails = new List<DriverCommissionDetails>();
            payment.customerdetails = db.ExecuteSprocAccessor(DBRoutine.TOTALAMOUNTRECEIVEDFROMCUSTOMER, MapBuilder<CustomerDetails>.BuildAllProperties(), datefrom,dateto).ToList();
            payment.pickCCommissiondetails = db.ExecuteSprocAccessor(DBRoutine.PICKCCOMMISSION, MapBuilder<pickCCommissionDetails>.BuildAllProperties(),datefrom,dateto).ToList();
            payment.driverCommissiondetails  = db.ExecuteSprocAccessor(DBRoutine.DRIVERCOMMISSION, MapBuilder<DriverCommissionDetails>.BuildAllProperties(),datefrom, dateto).ToList();
            return payment;
        }
        public  List<DriverPendingCommission> getDriverCommision()
        {
            return db.ExecuteSprocAccessor(DBRoutine.DRIVERCOMMISIONPENDINGTOPICKC, MapBuilder<DriverPendingCommission>.BuildAllProperties()).ToList();
        }
    }
}
