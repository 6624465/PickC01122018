using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Configuration;

using Operation.Contract;
using Operation.BusinessFactory;

using Master.Contract;
using Master.BusinessFactory;

using PickCApi.Areas.Operation.DTO;
using PickCApi.Core;
using RestSharp;
using System.Web;

namespace PickCApi.Areas.Operation.Controllers
{
    [RoutePrefix("api/operation/driveractivity")]
    public class DriverActivityController : ApiBase
    {
        [HttpGet]
        [Route("list")]
        [ApiAuthFilter]
        public IHttpActionResult DriverActivities()
        {
            try
            {
                var activities = new DriverActivityBO().GetList();
                if (activities != null)
                    return Ok(activities);
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPost]
        [Route("save")]
        [ApiAuthFilter]
        public IHttpActionResult SaveDriverActivity(DriverActivity driverActivity)
        {
            try
            {
                var result = new DriverActivityBO().SaveDriverActivity(driverActivity);
                return Ok(result ? UTILITY.SUCCESSMSG : UTILITY.FAILEDMSG);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPost]
        [Route("login")]
        public IHttpActionResult DriverLoginIn(DriverLoginDTO driverLoginDTO)
        {
            try
            {
                var token = new DriverActivityBO().DriverLogIn(driverLoginDTO.driverID, driverLoginDTO.password, driverLoginDTO.latitude, driverLoginDTO.longitude);
                if (!string.IsNullOrWhiteSpace(token))
                    return Ok(new
                    {
                        token = token
                    });
                else
                    return Ok(new { Status = UTILITY.FAILEDMESSAGE });
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [Route("logout")]
        [ApiAuthFilter]
        public IHttpActionResult DriverLogOut()
        {
            try
            {
                var driverActivity = new DriverActivity
                {
                    DriverId = HeaderValueByKey("DRIVERID"),
                    TokenNo = HeaderValueByKey("AUTH_TOKEN"),
                    Latitude = Convert.ToDecimal(HeaderValueByKey("LATITUDE")),
                    Longitude = Convert.ToDecimal(HeaderValueByKey("LONGITUDE")),
                    IsLogIn = false,
                    IsOnDuty = false
                };

                var result = new DriverActivityBO().DriverActivityUpdate(driverActivity);
                return Ok(result ? UTILITY.LOGOUT : UTILITY.FAILEDMSG);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        [HttpGet]
        [Route("checkOTP/{BookingNo}/{OTP}")]
        [ApiAuthFilter]
        public IHttpActionResult CheckMobile(string BookingNo, string OTP)
        {
            try
            {
                bool result = false;
                var bookingList = new BookingBO().GetList();
                if (bookingList != null)
                {
                    result = bookingList.Where(x => x.BookingNo == BookingNo && x.OTP == OTP).ToList().Count() > 0;
                    if (result == true)
                        return Ok(new { Status = UTILITY.SUCCESSMESSAGE });
                    else
                        return Ok(new { Status = UTILITY.FAILEDMESSAGE });
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
        [Route("dutystatus/{status}/{isintrip}/{tripID}")]
        [ApiAuthFilter]
        public IHttpActionResult UpdateDriverDutyStatus(bool status, bool isintrip, string tripID)
        {
            try
            {
                var driverActivity = new DriverActivity
                {
                    DriverId = HeaderValueByKey("DRIVERID"),
                    TokenNo = HeaderValueByKey("AUTH_TOKEN"),
                    Latitude = Convert.ToDecimal(HeaderValueByKey("LATITUDE")),
                    Longitude = Convert.ToDecimal(HeaderValueByKey("LONGITUDE")),
                    IsLogIn = true,
                    IsOnDuty = status
                };

                var driverActivityObj = new DriverActivity();
                if (isintrip)
                {
                    driverActivityObj = new DriverActivityBO().GetDriverActivity(new DriverActivity
                    {
                        TokenNo = HeaderValueByKey("AUTH_TOKEN"),
                        DriverId = HeaderValueByKey("DRIVERID")
                    });

                    var frmLatLong = driverActivityObj.CurrentLat.ToString() + "," + driverActivityObj.CurrentLong.ToString();
                    var toLatLong = driverActivity.Latitude + "," + driverActivity.Longitude;
                    var distance = GetTravelTimeBetweenTwoLocations(frmLatLong, toLatLong).distance;

                    new TripBO().TripUpdateTravelledDistance(tripID, distance);

                }
                var result = new DriverActivityBO().DriverActivityUpdate(driverActivity);

                return Ok(result ? UTILITY.SUCCESSMSG : UTILITY.FAILEDMSG);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        [HttpGet]
        [Route("CheckDriverVehicle")]
        [ApiAuthFilter]
        public IHttpActionResult CheckDriverVehicle()
        {
            try
            {
                var DRIVERID = HttpContext.Current.Request.Headers["DRIVERID"];
                var OperatorDriverList = new OperatorDriverBO().GetOperatorDriverList(DRIVERID);
                string vehicleattachedNo = OperatorDriverList.Select(s => s.VehicleattachedNo).FirstOrDefault();
                if (string.IsNullOrWhiteSpace(vehicleattachedNo))
                {
                    return Ok(new { Status = UTILITY.FAILEDMESSAGE, Message = "Sorry there is No Vehicle attched to your Profile!" });
                }
                else
                    return Ok(new { Status = UTILITY.SUCCESSMESSAGE });
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        [HttpGet]
        [Route("checkDriverPassword/{DriverID}/{password}")]
        [ApiAuthFilter]
        public IHttpActionResult CheckPasswordForDriver(string DriverID, string password)
        {
            try
            {
                var Driver = new DriverBO().GetDriver(new Driver { DriverId = DriverID });
                string driverPassword = Driver.Password;
                if (driverPassword != "" && driverPassword == password)
                {
                    return Ok(UTILITY.SUCCESSMSG);
                }
                else
                    return Ok(UTILITY.FAILEDMESSAGE);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        [HttpGet]
        [Route("forgotpassword/{DriverID}")]
        public IHttpActionResult ForgotpasswordForDriver(string DriverID)
        {
            bool result = false;
            var driver = new DriverBO().GetDriver(new Driver { DriverId = DriverID });
            if (driver != null)
            {
                result = SendDriverPassword(driver.MobileNo, driver.Password);
                if (result)
                {
                    return Ok("Driver Password Is Sent to Registered MobileNo!");
                }
                return Ok(UTILITY.FAILEDMESSAGE);
            }
            else
                return NotFound();
        }
        /* only for user */
        //[HttpPost]
        //[Route("user")]
        //[ApiAuthFilter]
        //public IHttpActionResult GetTrucksInRange(TrucksInRangeDTO trucksInRangeDTO)
        //{
        //    try
        //    {
        //        var trucks = new BookingBO().GetTrucksInRange(
        //            HeaderValueByKey("MOBILENO"),
        //            trucksInRangeDTO.Latitude,
        //            trucksInRangeDTO.Longitude,
        //            UTILITY.radius, trucksInRangeDTO.VehicleGroup, trucksInRangeDTO.VehicleType);
        //        if (trucks != null)
        //            return Ok(trucks);
        //        else
        //            return Ok(new List<NearTrucksInRange>());
        //    }
        //    catch (Exception ex)
        //    {
        //        return InternalServerError(ex);
        //    }
        //}
        /* only for user */

        [HttpGet]
        [Route("{BookingNo}/{vehicleNo}")]
        [ApiAuthFilter]
        public IHttpActionResult ConfirmBooking(string BookingNo, string vehicleNo)
        {
            try
            {
                var bookingObj = new BookingBO().GetBooking(new Booking { BookingNo = BookingNo });
                if (!bookingObj.IsCancel)
                {
                    string CustomerOTP = GenerateOTP();
                    var result = new BookingBO().BookingConfirmByDriver(HeaderValueByKey("DRIVERID"), HeaderValueByKey("AUTH_TOKEN"), vehicleNo, BookingNo, CustomerOTP);
                    if (result)
                    {
                        PushNotification(new BookingBO().GetCustomerDeviceIDByBookingNo(BookingNo), BookingNo, UTILITY.NotifySuccess);
                        SendOTP(bookingObj.CustomerId, CustomerOTP);
                        var booking = new BookingBO().GetBooking(new Booking
                        {
                            BookingNo = BookingNo
                        });
                        var driverList = new BookingBO().GetNearTrucksDeviceID(booking.BookingNo,
                            UTILITY.radius,
                            booking.VehicleType,
                            booking.VehicleGroup,
                            booking.Latitude,
                            booking.Longitude);

                        if (driverList.Count > 0)
                        {
                            var driverItem = driverList.Where(x => x.VehicleNo == vehicleNo).FirstOrDefault();
                            if (driverItem != null)
                                driverList.Remove(driverItem);

                            PushNotification(driverList.Select(x => x.DeviceId).ToList<string>(),
                                booking.BookingNo, UTILITY.NotifyBookingAcceptedByOtherDriver);
                        }
                    }


                    return Ok(new
                    {
                        status = result
                    });
                }
                else
                {
                    var driver = new DriverBO().GetDriver(new Driver { DriverId = HeaderValueByKey("DRIVERID") });
                    PushNotification(driver.DeviceId,
                                BookingNo, UTILITY.NotifyBookingCancelledByUser);

                    return Ok(UTILITY.NotifyBookingCancelledByUser);
                }
            }
            catch (Exception ex)
            {
                if (ex.Message == "Booking is Already Confirmed by Other Driver!")
                {
                    var bookingInfo = new BookingBO().GetBooking(new Booking { BookingNo = BookingNo });
                    if (bookingInfo.DriverId == HeaderValueByKey("DRIVERID"))
                        return Ok("Booking is Already Confirmed by you..!");
                    else
                        return Ok("Booking is Already Confirmed by Other Driver..!");
                }
                else
                {
                    return InternalServerError(ex);
                }
            }
        }

        [HttpPost]
        [Route("UpdateDriverCurrentLocation")]
        [ApiAuthFilter]
        public IHttpActionResult UpdateCurrentDriverLocation()
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
                    IsOnDuty = true
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
        [Route("DriverLatestFiveLatlong")]
        [ApiAuthFilter]
        public IHttpActionResult DriverLatestFiveLatlong()
        {
            var DriverID = HeaderValueByKey("DRIVERID");
            var AUTH_TOKEN = HeaderValueByKey("AUTH_TOKEN");
            var latlongs = new DriverActivityBO().GetFiveLatLongsforDriver(DriverID, AUTH_TOKEN).TrimEnd('|');
            var result = GetLatLongsValues(latlongs);
            return Ok(new { result });
        }
    }
}
