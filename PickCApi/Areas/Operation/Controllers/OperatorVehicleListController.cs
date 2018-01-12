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
namespace PickCApi.Areas.Operation.Controllers
{
    [RoutePrefix("api/operator/Vehicle")]
    [ApiAuthFilter]
    public class OperatorVehicleListController : ApiBase
    {
        // GET: Operation/OperatorVehicle
        [HttpGet]
        [Route("list")]
        public IHttpActionResult OperatorVehicleList()
        {
            try
            {
                var bookingList = new OperatorVehicleBO().GetList();
                if (bookingList != null)
                    return Ok(bookingList);
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

        }
        [HttpGet]
        [Route("CategoryList")]
        public IHttpActionResult CategoryList()
        {
            try
            {
                var categoryList = new OperatorVehicleBO().GetCategoryList();
                if (categoryList != null)
                    return Ok(categoryList);
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

        }
        [HttpGet]
        [Route("ModelList")]
        public IHttpActionResult OperatorModelList()
        {
            try
            {
                var bookingList = new OperatorVehicleBO().GetModelList();
                if (bookingList != null)
                    return Ok(bookingList);
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        [Route("save"), HttpPost]
        public IHttpActionResult SaveOperatorVehicle(OperatorVehicle operatorVehicle)
        {
            try
            {
                var result = new OperatorVehicleBO().SaveOperatorVehicleDTO(operatorVehicle);
                return Ok(result ? UTILITY.SUCCESSMSG : UTILITY.FAILEDMSG);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        [HttpGet]
        [Route("totalList")]
        public IHttpActionResult OperatorVehicletotalList()
        {
            try
            {
                var OperatorVehicletotalList = new OperatorVehicleBO().GetOperatorVehicleList();
                if (OperatorVehicletotalList != null)
                    return Ok(OperatorVehicletotalList);
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