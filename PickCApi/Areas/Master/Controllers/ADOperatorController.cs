using System.Net;
using System.Net.Http;
using System.Web.Http;

using Master.Contract;
using Operation.Contract;
using Master.BusinessFactory;
using Operation.BusinessFactory;
using PickCApi.Core;
using PickCApi.Areas.Master.DTO;
using PickC.Services.DTO;
using System.Linq;
using System;
using System.Web;
using System.Collections.Generic;

namespace PickCApi.Areas.Master.Controllers
{
    [RoutePrefix("api/master/ADoperator")]
   // [OperatorAPIAuthFilter]
    public class ADOperatorController : ApiBase
    {
        [HttpGet]
        [Route("list")]
        [OperatorAPIAuthFilter]
        public IHttpActionResult DriverList()
        {
            try
            {
                var driverList = new DriverBO().GetList();

                if (driverList != null)
                    return Ok(driverList);
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        [HttpGet]
        [Route("TripCount")]
        [OperatorAPIAuthFilter]
        public IHttpActionResult TodayTripCount()
        {
            try
            {
                var MOBILENO = HttpContext.Current.Request.Headers["MOBILENO"];
                int tripCount = new DriverBO().GetTripCount(MOBILENO);
                return Ok(new {tripCount });
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        [HttpGet]
        [Route("TripAmount")]
        [OperatorAPIAuthFilter]
        public IHttpActionResult TodayTripAmount()
        {
            try
            {
                var MOBILENO = HttpContext.Current.Request.Headers["MOBILENO"];
                decimal tripAmount = new DriverBO().GetTripAmount(MOBILENO);
                return Ok(new {tripAmount });
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        [HttpGet]
        [Route("TripCE/{FromDate}/{ToDate}")]
        [OperatorAPIAuthFilter]
        public IHttpActionResult TripCountEarnings(DateTime FromDate, DateTime ToDate)
        {
            try
            {
                var MOBILENO = HttpContext.Current.Request.Headers["MOBILENO"];
                var tripCountEarnings = new DriverBO().GetTripCountEarnings(MOBILENO,FromDate,ToDate);
                return Ok(tripCountEarnings);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        [HttpGet]
        [Route("TripCEList")]
        [OperatorAPIAuthFilter]
        public IHttpActionResult TripCountEarningsList()
        {
            try
            {
                var MOBILENO = HttpContext.Current.Request.Headers["MOBILENO"];
                DateTime FromDate = Convert.ToDateTime(HttpContext.Current.Request.Headers["FromDate"]);
                DateTime ToDate = Convert.ToDateTime(HttpContext.Current.Request.Headers["ToDate"]);
                var tripCountEarningsList = new DriverBO().GetTripCountEarningsList(MOBILENO, FromDate, ToDate);
                if (tripCountEarningsList != null)
                    return Ok(tripCountEarningsList);
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpGet]
        [Route("TripEarningList")]
        [OperatorAPIAuthFilter]
        public IHttpActionResult TripEarningsList()
        {
            try
            {
                var MOBILENO = HttpContext.Current.Request.Headers["MOBILENO"];
                var tripCountEarningsList = new DriverBO().GetTripEarningsList(MOBILENO);
                if (tripCountEarningsList != null)
                    return Ok(tripCountEarningsList);
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

        }
        [HttpGet]
        [Route("TripCountList")]
        [OperatorAPIAuthFilter]
        public IHttpActionResult TripCountList()
        {
            try
            {
                var MOBILENO = HttpContext.Current.Request.Headers["MOBILENO"];
                var tripCountList = new DriverBO().GetTripCountsList(MOBILENO);
                if (tripCountList != null)
                    return Ok(tripCountList);
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

        }
        
        [HttpGet]
        [Route("mobile/{mobile}")]
        public IHttpActionResult CheckMobile(string mobile)
        {
            try
            {
                bool result = false;
               // var mobile = HttpContext.Current.Request.Headers["MOBILENO"];
                var operatorList = new OperatorBO().GetList();

                if (operatorList != null)
                {
                    result = operatorList.Where(x => x.MobileNo == mobile).ToList().Count() > 0;
                    return Ok(result);
                }
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        [HttpPost]
        [Route("forgotpassword/{MobileNo}")]
        public IHttpActionResult Forgotpassword(string MobileNo)
        {
            ForgotPassword forgotPassword = new ForgotPassword();
            //var MobileNo = HttpContext.Current.Request.Headers["MOBILENO"];
            var OPerator = new OperatorBO().GetOperator(new Operator { MobileNo = MobileNo });
            if (OPerator != null)
            {
                OPerator.Password = forgotPassword.NewPassword;
                new OperatorBO().SaveOperator(OPerator);

                return Ok("Password updated...!");
            }
            else
                return NotFound();
        }
        [HttpPost]
        [Route("changepassword")]
        [OperatorAPIAuthFilter]
        public IHttpActionResult ChangePassword(OperatorPasssword operatorPasssword)
        {
            try
            {
                var result = new OperatorBO().UpdateOperatorPassword(operatorPasssword);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Ok(false);
            }
        }
        [HttpGet]
        [Route("operatorDetails/{mobileNo}")]
        [OperatorAPIAuthFilter]
        public IHttpActionResult OperatorDetails(string mobileNo)
        {
            try
            {
                var operatorBO = new OperatorBO();
                var operatorObj = operatorBO.GetOperator(new Operator { MobileNo = mobileNo });
                if (operatorObj != null)
                {
                    return Ok(operatorObj);
                }
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        [HttpPost]
        [Route("updateOperaor/{mobileNo}")]
        [OperatorAPIAuthFilter]
        public IHttpActionResult UpdateOperator(string mobileNo, Operator OPerator)
        {
            try
            {
                var operatorBO = new OperatorBO();
                var operatorObj = operatorBO.GetOperator(new Operator { MobileNo = mobileNo });
                if (operatorObj != null)
                {
                    operatorObj.OperatorName = OPerator.OperatorName;
                    operatorObj.Password = OPerator.Password;

                    var result = operatorBO.SaveOperator(operatorObj);
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
        [HttpPost]
        [Route("OperatorNotifications")]
        [OperatorAPIAuthFilter]
        public IHttpActionResult SaveOperatorNotifications(OperatorNotifications operatorNotifications)
        {
            try
            {
                var result = new OperatorBO().SaveOperatorNotifications(operatorNotifications);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        [HttpGet]
        [Route("logout")]
        [OperatorAPIAuthFilter]
        public IHttpActionResult Logout()
        {
            try
            {
                IEnumerable<string> headerValues;
                var token = string.Empty;
                if (Request.Headers.TryGetValues("AUTH_TOKEN", out headerValues))
                {
                    token = headerValues.FirstOrDefault();
                }
                var result = new OperatorLogInBO()
                    .DeleteOperatorLogIn(new OperatorLogIn { TokenNo = token });
                if (result)
                    return Ok(UTILITY.LOGOUT);
                else
                    return BadRequest();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        [HttpGet]
        [Route("Monitor/{MobileNo}")]
        [OperatorAPIAuthFilter]
        public IHttpActionResult OperatorDriverMonitor(string MobileNo)
        {
            try
            {
                var operatorObj =new OperatorLogInBO().GetOperatorDriverMonitor(MobileNo);
                if (operatorObj != null)
                {
                    return Ok(operatorObj);
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
        [Route("Operatordriverattachlist")]
        [OperatorAPIAuthFilter]
        public IHttpActionResult OperatorWiseDriverAttachList()
        {
            try
            {
                var MOBILENO = HttpContext.Current.Request.Headers["MOBILENO"];
                var driverList = new DriverBO().OperatorWiseDriverAttachList(MOBILENO);
                if (driverList != null)
                    return Ok(driverList);
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        [HttpGet]
        [Route("Operatortotaldriverslist")]
        [OperatorAPIAuthFilter]
        public IHttpActionResult OperatorWiseDriversList()
        {
            try
            {
                var MOBILENO = HttpContext.Current.Request.Headers["MOBILENO"];
                var driverList = new DriverBO().GetOperatorWiseDriversList(MOBILENO);
                if (driverList != null)
                    return Ok(driverList);
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        [HttpGet]
        [Route("Operatorbanklist")]
        [OperatorAPIAuthFilter]
        public IHttpActionResult OperatorWisebankList()
        {
            try
            {
                var MOBILENO = HttpContext.Current.Request.Headers["MOBILENO"];
                var driverList = new OperatorBO().GetOperatorWiseBankList(MOBILENO);
                if (driverList != null)
                    return Ok(driverList);
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        [HttpGet]
        [Route("Operatorwisedrivervehicleattachedlist")]
        [OperatorAPIAuthFilter]
        public IHttpActionResult OperatorWiseDriverVehicleAttachedTodayList()
        {
            try
            {
                var MOBILENO = HttpContext.Current.Request.Headers["MOBILENO"];
                var driverList = new OperatorBO().GetOperatorWiseDriverVehicleAttachedTodayList(MOBILENO);
                if (driverList != null)
                    return Ok(new { Status = UTILITY.SUCCESSMESSAGE, driverList });
                else
                    return Ok(new { Status = UTILITY.FAILEDMESSAGE });
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        [HttpPost]
        [Route("DetachOperatorwisedrivervehicleattachedlist")]
        [OperatorAPIAuthFilter]
        public IHttpActionResult DetachOperatorwisedrivervehicleattachedlist(OperatorWiseDriverVehicleAttachedTodayList operatorWiseDriverVehicleAttachedTodayList)
        {
            try
            {
                var result = new OperatorBO().DetachOperatorwisedrivervehicleattachedlist(operatorWiseDriverVehicleAttachedTodayList);
                if (result==true)
                    return Ok(new { Status = UTILITY.SUCCESSMESSAGE});
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