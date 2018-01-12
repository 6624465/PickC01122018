using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using PickCApi.Core;
using Master.Contract;
using Master.BusinessFactory;

namespace PickCApi.Areas.Master.Controllers
{
    [RoutePrefix("api/master/vehicletype")]
    //[ApiAuthFilter]
    public class VehicleTypeController : ApiController
    {
        [HttpGet]
        [Route("list")]
        public IHttpActionResult GetVehicleTypeList()
        {
            try
            {
                var typelist = new LookUpBO().GetVehicleTypeList();
                if (typelist != null)
                    return Ok(typelist);
                else
                    return NotFound();

            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

    }
}
