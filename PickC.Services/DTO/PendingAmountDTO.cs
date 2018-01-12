using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Master.Contract;

namespace PickC.Services.DTO
{
  public class PendingAmountDTO
    {
        public List<PendingAmount> pendingAmount { get; set; }
        //public List<PendingCommissionToPickC> pendingCommissiontoPickC { get; set; }
        //public List<DriverCommissionToPickC> driverCommissiontoPickC { get; set; }
        public SearchDates dates { get; set; }

        public class SearchDates
        {
            public DateTime DateFrom { get; set; }
            public DateTime DateTo { get; set; }
        }
        //public class PendingCommissionToPickC
        //{
        //    public int BookingNo { get; set; }
        //    public DateTime BookingDate { get; set; }
        //    public string VehicleType { get; set; }
        //    public string VehicleNo { get; set; }
        //    public string LocationFrom { get; set; }
        //    public string LocationTo { get; set; }
        //    public string CancellationRemarks { get; set; }
        //    public Decimal CancellationAmount { get; set; }
        //}
        //public class DriverCommissionToPickC
        //{
        //    public string DriverName { get; set; }
        //    public string VehicleNo { get; set; }
        //    public int BookingNo { get; set; }
        //    public DateTime BookingDate { get; set; }
        //    public Decimal TripAmount { get; set; }
        //    public string PaymentType { get; set; }
        //    public Decimal CommissionAmount { get; set; }
        //}
    }
}
