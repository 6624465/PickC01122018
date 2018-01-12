using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Master.Contract
{
    public class InvoiceDTO : IContract
    {
        public InvoiceDTO()
        {

        }
        public TripInvoice TripInvoice { get; set; }
        public DriverTripInvoice DriverTripInvoice { get; set; }
        public CompanyTripInvoice CompanyTripInvoice { get; set; }
    }
}
