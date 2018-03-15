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

namespace PickCApi.Areas.Operation.Controllers
{
    [RoutePrefix("api/operation/trip")]
     [ApiAuthFilter]
    public class TripController : ApiBase
    {
        [HttpGet]
        [Route("list")]
        public IHttpActionResult TripList()
        {
            try
            {
                var tripList = new TripBO().GetList();
                if (tripList != null)
                    return Ok(tripList);
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        [HttpPost]
        [Route("totalTrips")]
        public IHttpActionResult TotalTripList(UserData data)
        {
            try
            {
                var tripList = new TripBO().GetTotalTripsList(data);
                if (tripList != null)
                    return Ok(tripList);
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPost]
        [Route("start")]
        public IHttpActionResult StartTrip(Trip trip)
        {
            try
            {
                var result = new TripBO().SaveTrip(trip);
                if (result)
                {
                    var customerObj = new CustomerBO().GetCustomer(new Customer { MobileNo = trip.CustomerMobile });
                    var driverActivity = new DriverActivity
                    {
                        DriverId = HeaderValueByKey("DRIVERID"),
                        TokenNo = HeaderValueByKey("AUTH_TOKEN"),
                        Latitude = Convert.ToDecimal(HeaderValueByKey("LATITUDE")),
                        Longitude = Convert.ToDecimal(HeaderValueByKey("LONGITUDE")),
                        IsLogIn = true,
                        IsOnDuty = true
                    };
                    new DriverActivityBO().DriverActivityUpdate(driverActivity);
                    PushNotification(customerObj.DeviceId, trip.BookingNo, UTILITY.NotifyTripStart);
                    var bookingDetails = new BookingBO().GetList().Where(x => x.BookingNo == trip.BookingNo).FirstOrDefault();
                    var driverDetails = new DriverBO().GetDriver(new Driver { DriverId = bookingDetails.DriverId });
                    if (bookingDetails.CustomerId != bookingDetails.ReceiverMobileNo)
                    {
                        SendDriverDetailsToCustomer(bookingDetails.ReceiverMobileNo, string.Format(UTILITY.SmsNotifyTripStart, bookingDetails.BookingNo, bookingDetails.DriverName, driverDetails.MobileNo, bookingDetails.VehicleNo));
                    }

                    SendDriverDetailsToCustomer(bookingDetails.CustomerId, string.Format(UTILITY.SmsNotifyTripStart, bookingDetails.BookingNo, bookingDetails.DriverName, driverDetails.MobileNo, bookingDetails.VehicleNo));

                    return Ok(new
                    {
                        tripID = trip.TripID,
                        message = UTILITY.SUCCESSMSG
                    });
                }
                else
                    return BadRequest();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPost]
        [Route("end")]
        public IHttpActionResult EndTrip(TripEndDTO tripEndDTO)
        {
            try
            {
                var tripInfo = new TripBO().GetTrip(new Trip { TripID = tripEndDTO.TripId });
                string frmLatLong = tripInfo.Latitude + "," + tripInfo.Longitude.ToString();
                string toLatLong = tripEndDTO.TripEndLat + "," + tripEndDTO.TripEndLong.ToString();
                decimal distance = GetTravelTimeBetweenTwoLocations(frmLatLong, toLatLong).distance;
                tripEndDTO.Token = HeaderValueByKey("AUTH_TOKEN");
                tripEndDTO.DriverId = HeaderValueByKey("DRIVERID");
                var result = new TripBO().EndTrip(tripEndDTO, distance);
                decimal tripAmount = 0.00M;
                if (result)
                {
                    tripInfo = new TripBO().GetTrip(new Trip { TripID = tripEndDTO.TripId });
                    var customerObj = new CustomerBO().GetCustomer(new Customer { MobileNo = tripInfo.CustomerMobile });
                    PushNotification(customerObj.DeviceId, tripInfo.BookingNo, UTILITY.NotifyTripEnd);
                    var bookingDetails = new BookingBO().GetList().Where(x => x.BookingNo == tripInfo.BookingNo).FirstOrDefault();
                    tripAmount = new InvoiceBO().GetInvoiceByBookingNo(tripInfo.BookingNo).TotalAmount;
                    if (bookingDetails.CustomerId != bookingDetails.ReceiverMobileNo)
                    {                        
                        SendDriverDetailsToCustomer(bookingDetails.ReceiverMobileNo, string.Format(UTILITY.SmsNotifyTripEndToReceiver, tripAmount));
                    }

                    SendDriverDetailsToCustomer(bookingDetails.CustomerId, string.Format(UTILITY.SmsNotifyTripEndToCustomer, tripAmount));
                    
                    return Ok(new
                    {
                        tripID = tripEndDTO.TripId,
                        message = UTILITY.SUCCESSMSG
                    });
                }
                else
                    return BadRequest();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpGet]
        [Route("{tripId}")]
        public IHttpActionResult TripByTripID(string tripId)
        {
            try
            {
                var trip = new TripBO().GetTrip(new Trip { TripID = tripId });
                if (trip != null)
                    return Ok(trip);
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpDelete]
        [Route("{tripId}")]
        public IHttpActionResult DeleteTripByID(string tripId)
        {
            try
            {
                var result = new TripBO().DeleteTrip(new Trip { TripID = tripId });
                if (result)
                    return Ok(UTILITY.DELETEMSG);
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        //[HttpGet]
        //[Route("customer/isInTrip")]
        //public IHttpActionResult IsCustomerInTrip()
        //{
        //    try
        //    {
        //        var trip = new TripBO().CustomerCurrentTrip(HeaderValueByKey("MOBILENO"));
        //        return Ok(new {
        //            isintrip = trip != null ? true : false,
        //            bookingno = (trip != null ? trip.BookingNo : "")
        //        });
        //    }
        //    catch (Exception ex)
        //    {
        //        return InternalServerError(ex);
        //    }
        //}

        [HttpGet]
        [Route("driver/isintrip")]
        public IHttpActionResult IsDriverInTrip()
        {
            try
            {
                int Status = 1;
                var trip = new TripBO().DriverCurrentTrip(HeaderValueByKey("DRIVERID"), Status);
                if (trip != null)
                {
                    Status = 0;
                    var afterDestReach = new TripBO().DriverCurrentTrip(HeaderValueByKey("DRIVERID"), Status);
                    if (afterDestReach != null)
                    {
                        return Ok(new
                        {
                            isintrip = true,
                            tripid = afterDestReach.TripID,
                            bookingno = afterDestReach.BookingNo,
                            Message = UTILITY.DESTINATIONREACHEDTRIPENDPENDING
                        });
                        
                    }
                    else
                    {
                        return Ok(new
                        {
                            isintrip = true,
                            tripid = trip.TripID,
                            bookingno = trip.BookingNo,
                            Message=UTILITY.TRIPSTARTEDTRIPENDPENDING
                        });
                    }
                }
                else
                {
                    return Ok(new
                    {
                        isintrip = false,
                        tripid = "",
                        bookingno = "",
                        Message=""
                    });
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        [HttpGet]
        [Route("userDataDashBoard")]
        public IHttpActionResult BookingCount()
        {
            try
            {
                var count = new TripBO().GetuserDataDashBoard();
                return Ok(count);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpGet]
        [Route("CustomerStatus")]
        public IHttpActionResult CustomerStatus()
        {
            try
            {
                var result = new TripBO().GetCustomerStatusList();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPost]
        [Route("CustomerStatusSearch")]
        public IHttpActionResult CustomerStatus(RegButNotBookedSearch obj)
        {
            try
            {
                obj.DateFrom = obj.DateFrom.Value.ToLocalTime();
                obj.DateTo = obj.DateTo.Value.ToLocalTime();
                var result = new TripBO().GetCustomerStatusList(obj);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        [HttpGet]
        [Route("CancelledListByCustomer")]
        public IHttpActionResult getCancellationList()
        {
            try
            {
                var result = new TripBO().getCancelledList();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        [HttpGet]
        [Route("CancelledListByDriver")]
        public IHttpActionResult getCancellationListByDriver()
        {
            try
            {
                var result = new TripBO().getCancelledListByDriver();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        [HttpGet]
        [Route("GetTotalTripDetailsOnDailyBasis")]
        public IHttpActionResult GetTotalTripDailyBasis()
        {
            try
            {
                var result = new TripBO().getTripDetailsDailyBasis();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }


    }
}
