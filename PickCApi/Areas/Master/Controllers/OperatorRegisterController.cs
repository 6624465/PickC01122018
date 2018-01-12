using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using Master.Contract;
using Master.BusinessFactory;
using Operation.BusinessFactory;
using Operation.Contract;
using PickCApi.Core;

using PickCApi.Areas.Master.DTO;
namespace PickCApi.Areas.Master.Controllers
{
    [RoutePrefix("api/master/logOperator")]
    public class OperatorRegisterController  : ApiBase
    {
        [HttpPost]
        [Route("login")]
        public IHttpActionResult Login(Operator op)
        {
            try
            {
                var token = new OperatorLogInBO().OperatorLogIn(op.MobileNo, op.Password);
                return Ok(new
                {
                    token = token
                });
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        //[HttpPost]
        //[Route("login")]
        //public IHttpActionResult Login(string MobileNo, string Password)
        //{
        //    try
        //    {
        //        var token = new OperatorLogInBO().OperatorLogIn(MobileNo, Password);

        //        if (!string.IsNullOrWhiteSpace(token))
        //        {
        //            var _OPerator = new CustomerBO().GetCustomer(new Customer { MobileNo = MobileNo });
        //            if (_OPerator.IsOTPVerified)
        //                return Ok(new
        //                {
        //                    token = token
        //                });
        //            else
        //                return Ok("OTP not Verified...!");
        //        }

        //        else
        //            return NotFound();
        //    }
        //    catch (Exception ex)
        //    {
        //        return InternalServerError(ex);
        //    }
        //}
    }
}
