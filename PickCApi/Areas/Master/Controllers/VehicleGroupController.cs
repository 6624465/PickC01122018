using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using Master.Contract;
using Master.BusinessFactory;
using PickCApi.Core;

namespace PickCApi.Areas.Master.Controllers
{
    [RoutePrefix("api/master/vehiclegroup")]
    //[ApiAuthFilter]
    public class VehicleGroupController : ApiController
    {
        [HttpGet]
        [Route("list")]
        public IHttpActionResult GetVehicleGroupList()
        {
            try
            {
                var vehiclegrouplist = new LookUpBO().GetVehicleGroupList();
                if (vehiclegrouplist != null)
                    return Ok(vehiclegrouplist);
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpGet]
        [Route("{location}/list")]
        public IHttpActionResult GetVehicleGroupList(int location)
        {
            try
            {
                var vehiclegrouplist = new LookUpBO().GetVehicleGroupList();
                if (vehiclegrouplist != null)
                    return Ok(vehiclegrouplist);
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
