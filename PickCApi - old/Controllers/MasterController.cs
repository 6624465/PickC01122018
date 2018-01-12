using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;



namespace PickCApi.Controllers
{
    [RoutePrefix("api/Master")]
    public class MasterController : ApiBase
    {
        [HttpGet]
        [Route("test")]
        public IHttpActionResult Index()
        {
            return Ok("Working");
        }

        [HttpPost]
        [Route("RegisterAccount")]
        public IHttpActionResult RegisterAccount(string name)
        {
            return Ok("Registered Successfully!");
        }

        [HttpGet]
        [Route("test/{userid}")]
        public IHttpActionResult GetAcccountDetails(int userid)
        {
            try
            {
                if (true)
                {
                    var list = new List<string>();

                    list.Add("dPpySiPnyaI:APA91bEh73Py3ejcrZLfIuawhaIJGCtfFftbnD7UJAXr62jactSdUg5pUGSg3fIc6eWjPPwqsApwNyYTmHNawp1-QVRI1D2sE099uryXFGv87wNvOxIvKD3NlX5tLNHamNrOw5AAStAK");
                    list.Add("e2pbnyzJd2A:APA91bGIZQKt1P12Iiq5iVxJyCq1o2SypOExNQ3RAceWNSrxTS7_w4ww-sqtK6ssyfk7UvycmOnELtfxRRoFuthWlub4WYpg05P80273r99Q4syCp5Vt5JGc-KJDPnWIg5ovbuk0bIqg");

                    PushNotification(list, "BK1234", UTILITY.NotifySuccess);
                    return Ok("hi uday");
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
        [Route("OTP/{to}")]
        public IHttpActionResult OTP(string to)
        {
            try
            {
                SendOTP(to);
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

        }

        [HttpGet]
        [Route("BulkOTP/{to}")]
        public IHttpActionResult BulkOTP(string to)
        {
            var lst = to.Split(',');
            try
            {
                foreach (var item in lst)
                {
                    SendOTP1(item);
                }
                
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }


        [HttpGet]
        [Route("google")]
        public IHttpActionResult CallGoogleAPI()
        {
            var frmLatLong = "17.5013876500,78.3603772600";
            var toLatLong = "17.4399295000,78.4982741000";
            var distance = GetTravelTimeBetweenTwoLocations(frmLatLong, toLatLong);
            return Ok();
        }
    }
}
