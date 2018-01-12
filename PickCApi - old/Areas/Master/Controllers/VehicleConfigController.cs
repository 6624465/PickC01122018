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
    [RoutePrefix("api/master/vehicleconfig")]
    [ApiAuthFilter]
    public class VehicleConfigController : ApiController
    {
        [HttpPost]
        [Route("save")]
        public IHttpActionResult SaveVehicleConfig(VehicleConfig vehicleConfig)
        {
            try
            {
                var result = new VehicleConfigBO().SaveVehicleConfig(vehicleConfig);
                if (result)
                    return Ok(UTILITY.SUCCESSMSG);
                else
                    return BadRequest();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpGet]
        [Route("{vehicleType}")]
        public IHttpActionResult GetVehicleConfig(short vehicleType)
        {
            try
            {
                var vehicleConfig = new VehicleConfigBO()
                                        .GetVehicleConfig(new VehicleConfig
                                        {
                                            VehicleType = vehicleType
                                        });

                if (vehicleConfig != null)
                    return Ok(vehicleConfig);
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpDelete]
        [Route("{vehicleType}")]
        public IHttpActionResult DeleteVehicleConfig(short vehicleType)
        {
            try
            {
                var result = new VehicleConfigBO()
                                    .DeleteVehicleConfig(new VehicleConfig
                                    {
                                        VehicleType = vehicleType
                                    });
                if (result)
                    return Ok(UTILITY.DELETEMSG);
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpGet]
        [Route("list")]
        public IHttpActionResult GetVehicleConfigList()
        {
            try
            {
                var vehicleConfigList = new VehicleConfigBO().GetList();
                if (vehicleConfigList != null)
                    return Ok(vehicleConfigList);
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
