using System.Net;
using System.Net.Http;
using System.Web.Http;

using Master.Contract;
using Master.BusinessFactory;
using PickCApi.Core;
using PickCApi.Areas.Master.DTO;
using PickC.Services.DTO;
using System.Linq;
using System;
using System.Web;
namespace PickCApi.Areas.Master.Controllers
{
    [RoutePrefix("api/master/ADdriver")]
    //[ApiAuthFilter]
    public class ADdriverController : ApiBase
    { 
        [HttpPost]
        //[Route("changepassword/{driverID}")]
        [Route("changepassword")]
        [ApiAuthFilter]
        public IHttpActionResult ChangePassword(DriverPassword driverPassword)
        {
            try
            {
                var result = new DriverBO().UpdateDriverPassword(driverPassword);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Ok(false);
            }
        }
        [HttpPost]
        [Route("updateDriver")]
        [ApiAuthFilter]
        public IHttpActionResult UpdateDriver(Driver driver)
        {
            try
            {
                var DRIVERID = HttpContext.Current.Request.Headers["DRIVERID"];
                var driverBO = new DriverBO();
                var driverObj = driverBO.GetDriver(new Driver {DriverId= DRIVERID });
                if (driverObj != null)
                {
                    driverObj.DriverName = driver.DriverName;
                    driverObj.Password = driver.Password;
                    driverObj.ModifiedBy = "ADMIN";

                    var result = driverBO.SaveDriver(driverObj);
                    if (result)
                        return Ok(UTILITY.SUCCESSMSG);
                    else
                        return BadRequest();
                }
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        [HttpGet]
        [Route("getDriver")]
        [ApiAuthFilter]
        public IHttpActionResult GetDriver()
        {
            try
            {
                var driverBO = new DriverBO();
                var DRIVERID = HttpContext.Current.Request.Headers["DRIVERID"];
                var driverObj = driverBO.GetDriver(new Driver { DriverId = DRIVERID });
                if (driverObj != null)
                {
                    return Ok(driverObj);
                }
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        [HttpGet]
        [Route("DriverListOfTrips/{FromDate}/{ToDate}")]
        [ApiAuthFilter]
        public IHttpActionResult DriverTodayListOfTrips(DateTime FromDate, DateTime ToDate)
        {
            try
            {
                var DRIVERID = HttpContext.Current.Request.Headers["DRIVERID"];
                var TodayListOfTrips = new DriverBO().GetTodayListOfTrips(DRIVERID,FromDate,ToDate);
                return Ok(TodayListOfTrips);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpGet]
        [Route("DriverTripCount/{FromDate}/{ToDate}")]
        [ApiAuthFilter]
        public IHttpActionResult TodayTripCount(DateTime FromDate,DateTime ToDate)
        {
            try
            {
                var DRIVERID = HttpContext.Current.Request.Headers["DRIVERID"];
                int tripCount = new DriverBO().GetTripCountbyDriverID(DRIVERID,FromDate,ToDate);
                return Ok(new { tripCount });
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        [HttpGet]
        [Route("DriverTripAmount/{FromDate}/{ToDate}")]
        [ApiAuthFilter]
        public IHttpActionResult TodayTripAmount(DateTime FromDate, DateTime ToDate)
        {
            try
            {
                var DRIVERID = HttpContext.Current.Request.Headers["DRIVERID"];
                decimal tripAmount = new DriverBO().GetTripAmountbyDriverID(DRIVERID,FromDate,ToDate);
                return Ok(new { tripAmount });
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        [HttpGet]
        [Route("GetDriverTripAmountbyPaymentType/{FromDate}/{ToDate}")]
        [ApiAuthFilter]
        public IHttpActionResult GetDriverTripAmountbyPaymentType(DateTime FromDate, DateTime ToDate)
        {
            try
            {
                var driverBO = new DriverBO();
                var DRIVERID = HttpContext.Current.Request.Headers["DRIVERID"];
                var driverObj = driverBO.GetDriverTripAmountbyPaymentType(DRIVERID,FromDate,ToDate);
                if (driverObj != null)
                {
                    return Ok(driverObj );
                }
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        [HttpGet]
        [Route("DriverTripAmountbyPaymentTypeList/{FromDate}/{ToDate}/{PaymentType}")]
        [ApiAuthFilter]
        public IHttpActionResult DriverTripAmountbyPaymentTypeListOfTrips(DateTime FromDate, DateTime ToDate, int PaymentType)
        {
            try
            {
                var DRIVERID = HttpContext.Current.Request.Headers["DRIVERID"];
                var TodayListOfTrips = new DriverBO().DriverTripAmountbyPaymentTypeListOfTrips(DRIVERID, FromDate, ToDate, PaymentType);
                return Ok(TodayListOfTrips);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        [HttpPost]
        [Route("DriverReferral")]
        [ApiAuthFilter]
        public IHttpActionResult DriverReferral(DriverReferral driverReferral)
        {
            try
            {
                var result = new DriverBO().SaveDriverReferral(driverReferral);
                if (result)
                {
                    SendOTP(driverReferral.Mobile,"You have been Referred for PickC Services!");
                    return Ok(new{ Status = UTILITY.SUCCESSMESSAGE });
                }
                else
                    return BadRequest();
                //return Ok(result);
            }
            catch (Exception ex)
            {
                return Ok(false);
            }
        }
    }
}
