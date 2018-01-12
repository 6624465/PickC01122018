using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using Operation.Contract;
using Operation.BusinessFactory;
using Operation.DataFactory;
using PickCApi.Core;

namespace PickCApi.Areas.Operation.Controllers
{
    [RoutePrefix("api/operation/tripmonitor")]
    [ApiAuthFilter]
    public class TripMonitorController : ApiController
    {
        [HttpGet, Route("list")]
        public IHttpActionResult List()
        {
            try
            {
                var list = new TripMonitorBO().GetList();
                return Ok(list);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
