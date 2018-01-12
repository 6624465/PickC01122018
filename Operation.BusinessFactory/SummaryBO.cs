using Operation.Contract;
using Operation.DataFactory;
using PickC.Services.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Operation.BusinessFactory
{
    
   public class SummaryBO
    {
        private SummaryDAL summaryDAL;
        public SummaryBO()
        {
            summaryDAL = new SummaryDAL();
        }

        public Summary GetSummary(string driverId) {
            return (Summary)summaryDAL.GetSummary<Summary>(driverId);
        }

        public List<DriverPayments> GetPayments(string driverID) {
            return summaryDAL.GetPayments(driverID);
        }
        public PaymentHistory PaymentDetails(DateTime datefrom,DateTime dateto)
        {
            return summaryDAL.getPaymentDetails(datefrom, dateto);
        }
        public List<DriverPendingCommission> getDriverCommision()
        {
            return summaryDAL.getDriverCommision();
        }
    }
}
