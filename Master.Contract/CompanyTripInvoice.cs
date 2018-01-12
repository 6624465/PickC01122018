using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Master.Contract
{
   public class CompanyTripInvoice:IContract
    {
        public CompanyTripInvoice(){}
        public string InvoiceNo{ get; set;}
        public string BookingNo{ get;set;}
        public string InvoiceDate{get;set; }
        public string DriverID{get;set;}
        public string DriverName{ get;set;}
        public decimal TotalAmount{get; set;}
        public decimal TripAmount{get;set;}
        public decimal TaxAmount{get;set;}
        public decimal CompanyTripAmount{ get;set;}
        public decimal GST{get; set;}
        public decimal CGST{ get; set;}
        public decimal CompanyTotalAmount{get; set;}
        public string VehicleGroup {get;set; }
        public string VehicleType{ get;  set;}
        public string VehicleNo{get; set;}
        public string CustomerMobileNo{get;set;}
        public string CustomerName{get;set;}
        public string FromLocation{ get;set;}
        public string ToLocation{ get; set;}
    }
}
