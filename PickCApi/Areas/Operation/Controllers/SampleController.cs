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
using Newtonsoft.Json;
using System.IO;
using System.Text;

namespace PickCApi.Areas.Operation.Controllers
{
    [RoutePrefix("api/Sample")]
    public class SampleController : ApiBase
    {
        [HttpGet]
        [Route("CustomerNotification")]
        //[ApiAuthFilter]
        public IHttpActionResult CustomerNotification()
        {
            IHttpActionResult result;
            try
            {
                //string to = "cSYVGYkYWa8:APA91bF_dT5duPfzJIeOxxdB9a0PJpQ3Z0GMK_2Y8wxrfYeofiBc5PNYOVIkaxiRxFhPz_-TeSbH4KXdsgYb2pdkl2DLhgpfWJInNwPqGFRFnHNylxkzCGlJXyECmJRvg7LE5saYlZyY";
                string to = "dK1-98qPlXk:APA91bFAkVFxXZVgUyEsog_GjyItv6QLjXkSiljCTULHvSMSOzGicNfySq0UKanO7wn5zFt0wUSfIEDRWxAjvjTCL9yQhAeyfXxEYGuhutDjPwYEkIwIkJNqW2ZwMB57a68lFafqaRKv";
                string bookingNo = "this is testing ";
                string body = "this is testing @vijay";
                try
                {
                    string arg = "AAAAky-1GIw:APA91bEpg5U9nehVX6Ar12D9UHjok2BYpW5ivG-5zSaJHVHcZTMCfm2bfyZMJnBbkUEIV1qein1CBojk2ZhR0Ut6w-FWgOTpC5WpMXzicGBNADOHDAv550_X5PYKf3rv5-mA7fRlTPr7";
                    string arg2 = "632160589964";
                    WebRequest webRequest = WebRequest.Create(string.Format("https://fcm.googleapis.com/fcm/send?ts={0}", DateTime.Now.Ticks));
                    webRequest.Method = "post";
                    webRequest.ContentType = "application/json";
                    string s = JsonConvert.SerializeObject(new
                    {
                        to = to,
                        data = new
                        {
                            bookingNo,
                            body
                        },
                        webpush = new
                        {
                            headers = new
                            {
                                Urgency = "high"
                            }
                        }
                    });
                    byte[] bytes = Encoding.UTF8.GetBytes(s);
                    webRequest.Headers.Add(string.Format("Authorization: key={0}", arg));
                    webRequest.Headers.Add(string.Format("Sender: id={0}", arg2));
                    webRequest.ContentLength = (long)bytes.Length;
                    using (Stream requestStream = webRequest.GetRequestStream())
                    {
                        requestStream.Write(bytes, 0, bytes.Length);
                        using (WebResponse response = webRequest.GetResponse())
                        {
                            using (Stream responseStream = response.GetResponseStream())
                            {
                                using (StreamReader streamReader = new StreamReader(responseStream))
                                {
                                    string content = streamReader.ReadToEnd();
                                    result = this.Ok<string>(content);
                                    return result;
                                }
                            }
                        }
                    }
                }
                catch (Exception arg_13E_0)
                {
                    string arg_143_0 = arg_13E_0.Message;
                }
                result = this.Ok<string>(UTILITY.MESSAGESENT);
            }
            catch (Exception exception)
            {
                result = this.InternalServerError(exception);
            }
            return result;
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
