using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Master.Contract
{
    public class DriverInvoice : IContract
    {
        public string DriverName { get; set; }
        public string DriverID { get; set; }
        public string InvoiceNumber { get; set; }
        public string InvoiceID { get; set; }
        public string InvoiceDate { get; set; }
        public Int16 VehicleType { get; set; }
        public string VehicleID { get; set; }
        public string VehicleCategory { get; set; }
    }
}