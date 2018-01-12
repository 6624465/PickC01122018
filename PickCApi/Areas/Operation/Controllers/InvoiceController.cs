using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PickCApi.Core;

using Operation.Contract;
using Operation.BusinessFactory;

namespace PickCApi.Areas.Operation.Controllers
{
    [RoutePrefix("api/operation/invoice")]
    [ApiAuthFilter]
    public class InvoiceController : ApiBase
    {
        [Route("{bookingno}")]
        public IHttpActionResult GetInvoice(string bookingno)
        {
            try
            {
                var invoiceObj = new InvoiceBO().GetInvoiceByBookingNo(bookingno);
                return Ok(invoiceObj);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
