using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PickC.Services.DTO
{
    public class PaymentHistory
    {
        //public DateTime? Datefrom { get; set; }
        //public DateTime? Dateto { get; set; }
        public Paymentsearch paymentsearch { get; set; }
        public List<DriverCommissionDetails> driverCommissiondetails { get; set; }
        public List<pickCCommissionDetails> pickCCommissiondetails { get; set; }
        public List<CustomerDetails> customerdetails { get; set; }
        public DailyPaymentHistory dailyPaymentHistory { get; set; }
    }
    public class Paymentsearch
    {
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
    }
    public class DriverCommissionDetails
    {
        public string InvoiceNo { get; set; }
        public string DriverName { get; set; }
        public string VehicleNo { get; set; }
        public string TripId { get; set; }
        public decimal DriverCommission { get; set; }

    }
    public class pickCCommissionDetails
    {
        public string InvoiceNo { get; set; }
        public DateTime InvoiceDate { get; set; }
        public decimal InvoiceAmount { get; set; }
        public decimal GST { get; set; }
        public decimal PickcCommision { get; set; }

    }
    public class CustomerDetails
    {
        public string InvoiceNo { get; set; }
        public DateTime InvoiceDate { get; set; }
        public decimal InvoiceAmount { get; set; }
        public decimal GST { get; set; }
        public decimal TotalAmount { get; set; }
    }
    public class DailyPaymentHistory
    {
        public decimal CustomerAmount { get; set; }
        public decimal PickcAmount { get; set; }
        public decimal DriverAmount { get; set; }
    }
   
}
