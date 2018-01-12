using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Master.BusinessFactory;
using Master.Contract;
using Operation.BusinessFactory;

using PickCApi.Core;
using PickCApi.Areas.Operation.DTO;
using PickC.Services.DTO;
using Operation.Contract;

namespace PickCApi.Areas.Operation.Controllers
{
    [RoutePrefix("api/Sample")]
    public class SampleController : ApiBase
    {
        [HttpGet]
        [Route("CustomerNotification")]
        [ApiAuthFilter]
        public IHttpActionResult CustomerNotification()
        {
            try
            {
                PushNotification("cCZh6Au2TEw:APA91bEIMpiM2M6z6PBkac_cFGO5dvilC2V60urUWRNnHFQ1GPno4_FVIAzyTr6OanLw6OQrmSnsBOFqnwT4ZJIFByFKIpG-t4o22BCyr89Vqc2dk6LY6ulwGWuG0oUunB-BCBpD2rZA", "Bj234y3784y8", "Request from Customer to Driver");
                return Ok(UTILITY.MESSAGESENT);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        [HttpPost]
        [Route("UpdateDriverCurrentLocation")]
        [ApiAuthFilter]
        public IHttpActionResult UpdateCurrentDriverLocation(AccuracyRate acc)
        {
            try
            {

                var updateDriverCurrentLocation = new UpdateDriverCurrentLocation
                {
                    DriverID = HeaderValueByKey("DRIVERID"),
                    AUTH_TOKEN = HeaderValueByKey("AUTH_TOKEN"),
                    CurrentLatitude = Convert.ToDecimal(HeaderValueByKey("LATITUDE")),
                    CurrentLongitude = Convert.ToDecimal(HeaderValueByKey("LONGITUDE")),
                    IsLogIn = true,
                    IsOnDuty = true,
                    Accuracy = acc.accuracy,
                    Bearing = acc.bearing
                };
                var result = new DriverActivityBO().UpdateCurrentDriverLocation(updateDriverCurrentLocation);
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

        [HttpGet]
        [Route("driverlatestfivelatlong")]

        public IHttpActionResult driverlatestfivelatlong()
        {
            var driverid = HeaderValueByKey("DRIVERID");
            var auth_token = HeaderValueByKey("AUTH_TOKEN");
            var latlongs = new DriverActivityBO().GetFiveLatLongsforDriver(driverid, auth_token).TrimEnd('|');
            var result = GetLatLongsValues(latlongs);
            return Ok(new { result });
        }
        [HttpGet]
        [Route("DriverNotification")]
        public IHttpActionResult DriverNotification()
        {
            try
            {
                PushNotification("d03CKYQnZi8:APA91bEa8ISkvdt3dlL43GETkkMWhh3mZ-7HmBxF8HZD2Ju7OoG6LAdHYqFVTernZuw6eFkioC9UU6lEQJbog8nsyHd40iqxWHFd0ic4M4jKjZJ8YO89hCSAqufwMX9cPgbMacl1Ueid", "Bj234y3784y8", "Request accepted from Driver");
                return Ok(UTILITY.ACCEPTREQUEST);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

    }
}
