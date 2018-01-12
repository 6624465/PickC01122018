using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using Operation.Contract;
using Operation.BusinessFactory;

using Master.Contract;
using Master.BusinessFactory;

using PickCApi.Core;
using PickCApi.Areas.Operation.DTO;
using PickC.Services.DTO;

namespace PickCApi.Areas.Master.Controllers
{
    [RoutePrefix("api/operator/Driver")]
    [ApiAuthFilter]
    public class OperatorDriversController : ApiBase
    {
        // GET: Operation/OperatorVehicle
        [HttpGet]
        [Route("list")]
        public IHttpActionResult DriverList()
        {
            try
            {
                var DriverList = new OperatorDriverBO().GetList();
                if (DriverList != null)
                    return Ok(DriverList);
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

        }
        [HttpGet]
        [Route("VehicleNolist")]
        public IHttpActionResult VehicleNoList()
        {
            try
            {
                var DriverList = new OperatorDriverBO().GetVehicleNoList();
                if (DriverList != null)
                    return Ok(DriverList);
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

        }
        [HttpGet]
        [Route("VehicleNolist/{OPeratorID}")]
        public IHttpActionResult VehicleNoList(string OPeratorID)
        {
            try
            {
                var DriverList = new OperatorDriverBO().GetVehicleNoList(OPeratorID);
                if (DriverList != null)
                    return Ok(DriverList);
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

        }
        [Route("save"), HttpPost]
        public IHttpActionResult SaveOperatorDriver(OperatorDriverList operatorDriverList)
        {
            try
            {
                var result = new OperatorDriverBO().SaveOperatorDriver(operatorDriverList);
                return Ok(result ? UTILITY.SUCCESSMSG : UTILITY.FAILEDMSG);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        [HttpGet]
        [Route("totalList")]
        public IHttpActionResult OperatorDrivertotalList()
        {
            try
            {
                var OperatorDriverList = new OperatorDriverBO().GetOperatorTotalDriverList();
                if (OperatorDriverList != null)
                    return Ok(OperatorDriverList);
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
