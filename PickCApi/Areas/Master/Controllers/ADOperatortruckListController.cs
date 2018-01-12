using System.Net;
using System.Net.Http;
using System.Web.Http;

using Master.Contract;
using Master.BusinessFactory;
using PickCApi.Core;
using PickCApi.Areas.Master.DTO;
using PickC.Services.DTO;
using System;
using System.Web;
namespace PickCApi.Areas.Master.Controllers
{
    [RoutePrefix("api/master/ADtruck")]
    [OperatorAPIAuthFilter]
    public class ADOperatortruckListController : ApiBase
    {
        [HttpGet]
        [Route("list")]
        public IHttpActionResult TruckList()
        {
            try
            {
                var MOBILENO = HttpContext.Current.Request.Headers["MOBILENO"];
                var truckList = new TruckBO().GetList(MOBILENO);

                if (truckList != null)
                    return Ok(truckList);
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        [HttpGet]
        [Route("listbyType/{vehicleType}")]
        public IHttpActionResult TruckListTypeWise(int vehicleType)
        {
            try
            {
                var truckBo = new TruckBO();
                var MOBILENO = HttpContext.Current.Request.Headers["MOBILENO"];
                var truckList = truckBo.GetListByType(vehicleType, MOBILENO);
                if (truckList != null)
                    return Ok(truckList);
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        [HttpPost]
        [Route("OperatorDriver")]
        public IHttpActionResult AttachOperatorDriver(OperatorDriverList operatorDriverlist)
        {
            try
            {
                var result = new OperatorDriverBO().SaveOperatorDriver(operatorDriverlist);
                return Ok(result);
            }
            catch(Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        [HttpPost]
        [Route("OperatorDriverTruckAttachment")]
        public IHttpActionResult UpdateOperatorDriverTruckAttachment(OperatorDriverTruckAttachment operatorDriverTruckAttachment)
        {
            try
            {
                var result = new OperatorDriverBO().UpdateOperatorDriverTruckAttachment(operatorDriverTruckAttachment);
                if (result)
                    return Ok(new { Status = UTILITY.SUCCESSMESSAGE });
                else
                    return Ok(new { Status = UTILITY.FAILEDMESSAGE });
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }       
    }
}
