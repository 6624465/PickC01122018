using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Master.Contract
{
    public class TripInvoice : IContract
    {
        public string InvoiceNo {get;set;}
        public string BookingNo{ get;set;}
        public string InvoiceDate{get;set;}
        public string CustomerMobileNo{get;set;}
        public string CustomerName{ get; set;}
        public string TripID{ get;set;}
        public string BaseKM{  get; set;}
        public string BaseFare{ get; set;}
        public string DistanceKM { get;  set; }
        public string DistanceFare{ get; set;}
        public short TravelTime{  get;  set; }
        public string TravelTimeFare { get; set; }
        public string LoadingUnLoadingCharges{ get; set; }
        public string WaitingCharges{ get; set;}
        public string OtherCharges{      get;set; }
        public string TotalDistanceKm{  get; set;}
        public string DriverID{ get; set; }
        public string DriverName{    get;  set; }
        public string StartDate{   get; set; }
        public string EndDate{get; set; }
        public string StartTime{ get; set; }
        public string EndTime { get; set; }
        public string LocationFrom{  get;  set;}
        public string LocationTo{  get; set; }
        public string TotalAmount{ get; set;}
        public string GSTTax {   get; set; }
        public string LoadingUnloading{get; set;}
        public string PaymentType{  get;  set; }
        public string TotalBillAmount {  get;   set;}
        public string TotalFare{ get;   set;  }
        public string VehicleType{   get; set;}
        public string VehicleGroup{ get;  set;}
        public string VehicleNo{ get; set;}
        public string VehicleMaker{ get; set;}
        public string FromLatitute  {  get;   set; }
        public string FromLongitude{      get;     set;  }
        public string ToLatitute { get; set; }
        public string ToLongitude{ get;   set; }
        //public string Tax1
        //{
        //    get;
        //    set;
        //}
    }
}
