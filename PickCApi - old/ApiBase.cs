using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Configuration;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using PickCApi.Areas.Operation.DTO;
using System.Web.Routing;
using System.Web.Mvc;
using Operation.Contract;

namespace PickCApi
{
    public class ApiBase : ApiController
    {
        public string HeaderValueByKey(string key)
        {
            IEnumerable<string> headerValues;
            var headerVal = string.Empty;
            if (Request.Headers.TryGetValues(key, out headerValues))
            {
                headerVal = headerValues.FirstOrDefault();
            }
            return headerVal;
        }

        public void PushNotification(string toDeviceId, string bookingNo, string message)
        {
            try
            {
                string applicationID = ConfigurationManager.AppSettings["appApplicationKey"];
                string senderId = ConfigurationManager.AppSettings["appSenderId"];

                WebRequest tRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");
                tRequest.Method = "post";
                tRequest.ContentType = "application/json";
                var data = new
                {
                    to = toDeviceId,
                    //notification = new
                    //{
                    //    body = message,
                    //    title = "Alert",
                    //    sound = "Enabled"
                    //},
                    data = new
                    {
                        bookingNo = bookingNo,
                        body = message
                    }
                };

                var json = JsonConvert.SerializeObject(data);
                Byte[] byteArray = Encoding.UTF8.GetBytes(json);
                tRequest.Headers.Add(string.Format("Authorization: key={0}", applicationID));
                tRequest.Headers.Add(string.Format("Sender: id={0}", senderId));
                tRequest.ContentLength = byteArray.Length;
                using (Stream dataStream = tRequest.GetRequestStream())
                {
                    dataStream.Write(byteArray, 0, byteArray.Length);
                    using (WebResponse tResponse = tRequest.GetResponse())
                    {
                        using (Stream dataStreamResponse = tResponse.GetResponseStream())
                        {
                            using (StreamReader tReader = new StreamReader(dataStreamResponse))
                            {
                                String sResponseFromServer = tReader.ReadToEnd();
                                string str = sResponseFromServer;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string str = ex.Message;
            }
        }
        public void PushNotification(List<string> receipents, string bookingNo, string message)
        {
            try
            {
                string applicationID = ConfigurationManager.AppSettings["appApplicationKey"];
                string senderId = ConfigurationManager.AppSettings["appSenderId"];

                WebRequest tRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");
                tRequest.Method = "post";
                tRequest.ContentType = "application/json";
                var data = new
                {
                    registration_ids = receipents,
                    //notification = new
                    //{
                    //    body = message,
                    //    title = "Alert",
                    //    sound = "Enabled"
                    //},
                    data = new
                    {
                        bookingNo = bookingNo,
                        body = message
                    }
                };

                var json = JsonConvert.SerializeObject(data);
                Byte[] byteArray = Encoding.UTF8.GetBytes(json);
                tRequest.Headers.Add(string.Format("Authorization: key={0}", applicationID));
                tRequest.Headers.Add(string.Format("Sender: id={0}", senderId));
                tRequest.ContentLength = byteArray.Length;
                using (Stream dataStream = tRequest.GetRequestStream())
                {
                    dataStream.Write(byteArray, 0, byteArray.Length);
                    using (WebResponse tResponse = tRequest.GetResponse())
                    {
                        using (Stream dataStreamResponse = tResponse.GetResponseStream())
                        {
                            using (StreamReader tReader = new StreamReader(dataStreamResponse))
                            {
                                String sResponseFromServer = tReader.ReadToEnd();
                                string str = sResponseFromServer;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string str = ex.Message;
            }
        }

        public string Nokeylist = "0123456789";
        public string GenerateOTP()
        {
            var password = "";
            Random r = new Random();
            int keyLength = Nokeylist.Length;
            for (var i = 0; i < 4; i++)
            {
                password += Nokeylist[r.Next(0, keyLength)];
            }
            return password;
        }
        public bool SendDriverPassword(string To, string Password)
        {
            return new smsGenerator().ConfigSms(To, Password, "PickC Administrator");
        }
        public bool SendOTP(string To, string OTP)
        {
            return new smsGenerator().ConfigSms(To, string.Format(UTILITY.SmsOTP, OTP));
        }

        public bool SendOTP(string To)
        {
            return new smsGenerator().ConfigSms(To, string.Format(UTILITY.SmsOTP, GenerateOTP()));
        }
        public bool SendOTP1(string To)
        {
            return new smsGenerator().ConfigSms(To, UTILITY.SmsConfirmTrip);
        }
        public bool SendOTP2(string To)
        {
            return new smsGenerator().ConfigSms(To, string.Format(UTILITY.SmsConfirmBooking, "BK160800001"));
        }
        public DriverLatLong GetLatLongsValues(string latlong)
        {
            string API_KEY = ConfigurationManager.AppSettings["latlongGoogleAPIKey"];

            string API_URL = "https://roads.googleapis.com/v1/snapToRoads?path=" + latlong + "&interpolate=true&key=" + API_KEY;
            return GetAPI(API_URL);
        }
        public DriverLatLong GetAPI(string url)
        {
            WebRequest request = WebRequest.Create(url);
            DriverLatLong _response = new DriverLatLong();
            using (WebResponse response = request.GetResponse())
            {
                using (var sr = new StreamReader(response.GetResponseStream()))
                {
                    var obj = JsonConvert.DeserializeObject<RootObject>(sr.ReadToEnd());

                    _response.rootObject = obj;
                    return _response;
                }
                //return Ok();
            }

        }
        public TripEstimateResponse GetTravelTimeBetweenTwoLocations(string frmLatLong, string toLatLong)
        {
            string API_KEY = ConfigurationManager.AppSettings["googleAPIKEY"];
            string API_URL = "https://maps.googleapis.com/maps/api/distancematrix/json?units=imperial&origins="
                + frmLatLong + "&destinations=" + toLatLong + "&key=" + API_KEY;

            return GoogleWebApi(API_URL);
        }
        public TripEstimateResponse GoogleWebApi(string Url)
        {
            WebRequest request = WebRequest.Create(Url);
            TripEstimateResponse _response = new TripEstimateResponse();
            using (WebResponse response = request.GetResponse())
            {
                decimal distance = 0.00M;
                decimal duration = 0.00M;
                using (var sr = new StreamReader(response.GetResponseStream()))
                {
                    var obj = JsonConvert.DeserializeObject<GoogleApiResponse>(sr.ReadToEnd());
                    if (obj.rows.Count() > 0)
                    {
                        if (obj.rows[0].elements.Count > 0)
                        {
                            if (obj.rows[0].elements[0].distance != null)
                            {
                                distance = (obj.rows[0].elements[0].distance.value / 1000);
                            }

                            if (obj.rows[0].elements[0].duration != null)
                            {
                                duration = (obj.rows[0].elements[0].duration.value / 60);
                            }
                        }
                    }
                }

                _response.amount = 0.00M;
                _response.distance = distance;
                _response.duration = duration;
                return _response;
            }
        }

        public static T CreateController<T>(RouteData routeData = null) where T : Controller, new()
        {

            T controller = new T();
            HttpContextBase wrapper;
            if (System.Web.HttpContext.Current != null)
                wrapper = new HttpContextWrapper(System.Web.HttpContext.Current);
            else
                throw new InvalidOperationException(
                    "Can't create Controller Context if no " +
                    "active HttpContext instance is available.");

            if (routeData == null)
                routeData = new RouteData();

            if (!routeData.Values.ContainsKey("controller") &&
                !routeData.Values.ContainsKey("Controller"))
                routeData.Values.Add("controller",
                                     controller.GetType()
                                               .Name.ToLower().Replace("controller", ""));

            controller.ControllerContext = new ControllerContext(wrapper, routeData, controller);
            return controller;
        }

        public string RenderViewToString(ControllerContext context,
                                   string viewPath,
                                   object model = null,
                                   bool partial = false)
        {
            ViewEngineResult viewEngineResult = null;
            if (partial)
                viewEngineResult = ViewEngines.Engines.FindPartialView(context, viewPath);
            else
                viewEngineResult = ViewEngines.Engines.FindView(context, viewPath, null);

            if (viewEngineResult == null)
                throw new FileNotFoundException("View cannot be found.");


            var view = viewEngineResult.View;
            context.Controller.ViewData.Model = model;

            string result = null;

            using (var sw = new StringWriter())
            {
                var ctx = new ViewContext(context, view,
                                            context.Controller.ViewData,
                                            context.Controller.TempData,
                                            sw);
                view.Render(ctx, sw);
                result = sw.ToString();
            }

            return result;
        }
    }
    //public class Location
    //{
    //    public double latitude { get; set; }
    //    public double longitude { get; set; }
    //}

    //public class SnappedPoint
    //{
    //    public Location location { get; set; }
    //    public int originalIndex { get; set; }
    //    public string placeId { get; set; }
    //}

    //public class RootObject
    //{
    //    public List<SnappedPoint> snappedPoints { get; set; }
    //}
    public class GoogleApiResponse
    {
        public List<string> destination_addresses { get; set; }
        public List<string> origin_addresses { get; set; }

        public List<Rows> rows { get; set; }

        public string status { get; set; }
    }

    public class Rows
    {
        public List<Element> elements { get; set; }
    }

    public class Element
    {
        public KeyValue distance { get; set; }

        public KeyValue duration { get; set; }

        public string status { get; set; }
    }

    public class KeyValue
    {
        public string text { get; set; }

        public int value { get; set; }
    }
}
