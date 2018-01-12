using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Master.Contract
{
    public class DriverTripInvoice : IContract
    {
        public DriverTripInvoice()
        {

        }
        public string InvoiceNo{ get; set;}
        public string BookingNo{   get;set; }
        public string InvoiceDate{get;set;}
        public string DriverID{ get;set;}
        public string DriverName{get;set; }
        public string PANNo { get; set; }
        public decimal TotalAmount { get;set;}
        public decimal TripAmount { get; set;}
        public decimal TaxAmount{ get; set;}
        public decimal DriverTripAmount{ get; set; }
        public decimal DriverGST{  get;  set; }
        public decimal DriverTotalAmount { get; set; }
        public string VehicleGroup{ get; set; }
        public string VehicleType{  get; set; }
        public string VehicleNo{get; set;}
        public string CustomerMobileNo{ get;set; }
        public string CustomerName{get; set;}
        public string FromLocation { get; set; }
        public string ToLocation{ get;set; }

    }
}
