﻿using Master.BusinessFactory;
using Operation.BusinessFactory;
using Operation.Contract;
using PickCApi.Areas.Operation.DTO;
using PickCApi.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace PickCApi.Areas.Operation.Controllers
{
    [RoutePrefix("api/operation/booking")]
    [ApiAuthFilter]
    public class BookingController : ApiBase
    {
        [HttpGet]
        [Route("list")]
        public IHttpActionResult BookingHistoryList()
        {
            try
            {
                var bookingList = new BookingBO().GetBookingHistoryList();
                if (bookingList != null)
                    return Ok(bookingList);
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        //[HttpGet]
        //[Route("bookingHistoryListbyCustomerMobileNo/{MobileNo}")]
        //public IHttpActionResult BookingHistoryListbyCustomerMobileNo(string MobileNo)
        //{
        //    try
        //    {
        //        var bookingList = new BookingBO().GetBookingHistoryList().Where(x => x.CustomerID == MobileNo);
        //        if (bookingList != null)
        //            return Ok(bookingList);
        //        else
        //            return NotFound();
        //    }
        //    catch (Exception ex)
        //    {

        //        return InternalServerError(ex);
        //    }
        //}
        [HttpGet]
        [Route("list/customerbyname/{status?}")]

        public IHttpActionResult GetDriverBySearch(int? status)
        {
            try
            {
                var result = new BookingBO().GetCustomerBySearch(status);
                if (result != null)
                    return Ok(result);
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

        }
        [HttpGet]
        [Route("list/{mobileNo}")]
        public IHttpActionResult BookingList(string mobileNo)
        {
            try
            {
                var bookingList = new BookingBO().GetListByMobileNo(mobileNo);
                if (bookingList != null)
                    return Ok(bookingList);
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        [HttpGet]
        [Route("bookinglist/{BookingNo}")]
        public IHttpActionResult BookingListbyBookingNo(string BookingNo)
        {
            try
            {
                var bookingList = new BookingBO().GetListByBookingNo(BookingNo);
                if (bookingList != null)
                    return Ok(bookingList);
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        //[HttpPost]
        //[Route("save")]
        //public IHttpActionResult SaveBooking(Booking booking)
        //{
        //    try
        //    {
        //        var result = new BookingBO().SaveBooking(booking);
        //        if (result)
        //        {
        //            Booking bookings = new BookingBO().GetBooking(new Booking
        //            {
        //                BookingNo = booking.BookingNo
        //            });
        //            var driverList = new BookingBO().GetNearTrucksDeviceID(bookings.BookingNo,
        //                UTILITY.radius,
        //                bookings.VehicleType,
        //                bookings.VehicleGroup,
        //                bookings.Latitude,
        //                bookings.Longitude);//UTILITY.radius
        //            if (driverList.Count > 0)
        //            {
        //                PushNotification(driverList.Select(x => x.DeviceID).ToList<string>(),
        //                    booking.BookingNo, UTILITY.NotifyNewBooking);
        //                return Ok(new
        //                {
        //                    bookingNo = booking.BookingNo,
        //                    message = UTILITY.SUCCESSMSG
        //                });
        //            }
        //            else
        //            {
        //                var CancelBooking = new BookingBO().DeleteBooking(new Booking { BookingNo = booking.BookingNo });
        //                if (CancelBooking)
        //                    return Ok(new { bookingNo="",message = UTILITY.NotifyCustomer, });
        //                else
        //                    return Ok(new { bookingNo = "",message = UTILITY.NotifyCustomerFail });
        //            }
        //        }
        //        else
        //            return BadRequest();
        //    }
        //    catch (Exception ex)
        //    {
        //        return InternalServerError(ex);
        //    }
        //}

        //[HttpGet]
        //[Route("isConfirm/{bookingNo}")]
        //public IHttpActionResult IsConfirmBooking(string bookingNo)
        //{
        //    try
        //    {
        //        var booking = new BookingBO().GetBooking(new Booking
        //        {
        //            BookingNo = bookingNo
        //        });

        //        var driverInfo = new Driver();
        //        var driverActivity = new DriverActivity();
        //        if (booking.IsConfirm)
        //        {
        //            driverInfo = new DriverBO().GetDriver(new Driver { DriverID = booking.DriverID });
        //            driverActivity = new DriverActivityBO().GetDriverActivityByDriverID(new DriverActivity { DriverID = booking.DriverID });
        //        }

        //        if (booking != null)
        //            return Ok(new
        //            {
        //                isConfirm = booking.IsConfirm,
        //                driverId = driverInfo.DriverID ?? "",
        //                vehicleNo = driverInfo.VehicleNo ?? "",
        //                driverName = driverInfo.DriverName ?? "",
        //                driverImage = "",
        //                MobileNo = driverInfo.MobileNo ?? "",
        //                latitude = driverActivity.CurrentLat,
        //                longitude = driverActivity.CurrentLong,
        //                OTP = booking.OTP,
        //                VehicleType = booking.VehicleType
        //            });
        //        else
        //            return NotFound();
        //    }
        //    catch (Exception ex)
        //    {
        //        return InternalServerError(ex);
        //    }
        //}

        [HttpGet]
        [Route("{bookingNo}")]
        public IHttpActionResult BookingByBookingNo(string bookingNo)
        {
            try
            {
                var booking = new BookingBO().GetBooking(new Booking
                {
                    BookingNo = bookingNo
                });

                if (booking != null)
                    return Ok(booking);
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPost]
        [Route("delete")]
        public IHttpActionResult DeleteByBookingNo(DeleteBookingDTO deleteBookingDTO)
        {
            try
            {
                var result = new BookingBO().DeleteBooking(new Booking { BookingNo = deleteBookingDTO.BookingNo });
                if (result)
                {
                    string GetDriverDeviceIDByBookingNo = new BookingBO().GetDriverDeviceIDByBookingNo(deleteBookingDTO.BookingNo);
                    if (!string.IsNullOrWhiteSpace(GetDriverDeviceIDByBookingNo))
                    {
                        PushNotification(GetDriverDeviceIDByBookingNo, deleteBookingDTO.BookingNo, UTILITY.NotifyBookingCancelledByUser);
                        return Ok(UTILITY.DELETEMSG);
                    }
                    else
                        return Ok(UTILITY.DELETEMSG);
                }
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        /* only for driver */
        [HttpGet]
        [Route("driver")]
        public IHttpActionResult GetNearBookingsForDriver()
        {
            try
            {
                var bookingsList = new BookingBO().GetNearBookingsForDriver(
                    new Guid(HeaderValueByKey(UTILITY.HEADERKEYS.AUTH_TOKEN.ToString())),
                    HeaderValueByKey(UTILITY.HEADERKEYS.DRIVERID.ToString()),
                    Convert.ToDecimal(HeaderValueByKey(UTILITY.HEADERKEYS.LATITUDE.ToString())),
                    Convert.ToDecimal(HeaderValueByKey(UTILITY.HEADERKEYS.LONGITUDE.ToString())),
                    UTILITY.radius
                    );

                if (bookingsList != null)
                    return Ok(bookingsList);
                else
                    return Ok(new List<Booking>());
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        /* only for driver */

        [HttpPost]
        [Route("cancel")]
        public IHttpActionResult AfterPickupreachCancelBookingByDriver(BookingCancelDTO bookingCancelDTO)
        {
            try
            {
                var result = new BookingBO().BookingCancelledByDriver(
                    HeaderValueByKey("AUTH_TOKEN"),
                    bookingCancelDTO.driverID,
                    bookingCancelDTO.vehicleNo,
                    bookingCancelDTO.bookingNo,
                    bookingCancelDTO.cancelRemarks,
                    bookingCancelDTO.istripstarted,
                    bookingCancelDTO.IsLoadingUnloading);
                if (result)
                {
                    PushNotification(new BookingBO().GetCustomerDeviceIDByBookingNo(bookingCancelDTO.bookingNo),
                      bookingCancelDTO.bookingNo,
                      UTILITY.NotifyCancelledByDriver);
                    return Ok(new { Status = UTILITY.SUCCESSMESSAGE });
                }
                else
                    return Ok(new { Status = UTILITY.FAILEDMESSAGE });
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        [HttpPost]
        [Route("Reject")]
        [ApiAuthFilter]
        public IHttpActionResult RejectBookingByDriverInNotification(BookingCancelDTO bookingCancelDTO)
        {
            try
            {
                var result = new BookingBO().BookingRejectedByDriver(
                    HeaderValueByKey("AUTH_TOKEN"),
                    bookingCancelDTO.driverID,
                    bookingCancelDTO.vehicleNo,
                    bookingCancelDTO.bookingNo,
                    bookingCancelDTO.cancelRemarks,
                    bookingCancelDTO.istripstarted,
                    bookingCancelDTO.IsLoadingUnloading);
                if (result)
                {
                    PushNotification(new BookingBO().GetCustomerDeviceIDByBookingNo(bookingCancelDTO.bookingNo),
                      bookingCancelDTO.bookingNo,
                      UTILITY.NotifyCancelledByDriver);
                    return Ok(new { Status = UTILITY.SUCCESSMESSAGE });
                }
                else
                    return Ok(new { Status = UTILITY.FAILEDMESSAGE });
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        //[HttpPost]
        //[Route("Drivercancellations")]
        //public IHttpActionResult CancelBookingByDrivercancellations(BookingCancelDTO bookingCancelDTO)
        //{
        //    try
        //    {
        //        var result = new BookingBO().BookingCancelledByDriver(
        //            HeaderValueByKey("AUTH_TOKEN"),
        //            bookingCancelDTO.driverID,
        //            bookingCancelDTO.vehicleNo,
        //            bookingCancelDTO.bookingNo,
        //            bookingCancelDTO.cancelRemarks,
        //            bookingCancelDTO.istripstarted);
        //        if (result == true)
        //            return Ok(new { Status = UTILITY.SUCCESSMESSAGE });
        //        else
        //            return Ok(new { Status = UTILITY.FAILEDMESSAGE });
        //        //return Ok(result ? UTILITY.SUCCESSMSG : UTILITY.FAILEDMSG);
        //    }
        //    catch (Exception ex)
        //    {
        //        return InternalServerError(ex);
        //    }
        //}
        [HttpPost]
        [Route("pickupreachdatetime")]
        public IHttpActionResult SavePickupReachDateTime(PickupReachDateTimeDTO obj)
        {
            try
            {
                var result = new BookingBO().SavePickupReachDateTime(obj.bookingNo, obj.PickupReachDateTime);
                if (result)
                    PushNotification(new BookingBO().GetCustomerDeviceIDByBookingNo(obj.bookingNo), obj.bookingNo, UTILITY.NotifyPickUpReachDateTime);

                var bookingDetails = new BookingBO().GetBooking(new Booking { BookingNo = obj.bookingNo });
                SendDriverDetailsToCustomer(bookingDetails.CustomerId, string.Format(UTILITY.SmsNotifyPickUpReachDateTime, bookingDetails.VehicleNo));

                return Ok(result ? UTILITY.SUCCESSMSG : UTILITY.FAILEDMSG);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPost]
        [Route("destinationreachdatetime")]
        public IHttpActionResult SaveDestinationReachDateTime(DestinationReachDateTimeDTO obj)
           {
            try
            {
                var result = new BookingBO().SaveDestinationReachDateTime(obj.bookingNo, obj.DestinationReachDateTime);
                if (result)
                    PushNotification(new BookingBO().GetCustomerDeviceIDByBookingNo(obj.bookingNo), obj.bookingNo, UTILITY.NotifyDestinationReachDateTime);

                var bookingDetails = new BookingBO().GetBooking(new Booking { BookingNo = obj.bookingNo });
                if (bookingDetails.CustomerId != bookingDetails.ReceiverMobileNo)
                {
                    SendDriverDetailsToCustomer(bookingDetails.ReceiverMobileNo, string.Format(UTILITY.SmsDestinationReachedDateTime, bookingDetails.VehicleNo));
                }
                SendDriverDetailsToCustomer(bookingDetails.CustomerId, string.Format(UTILITY.SmsDestinationReachedDateTime, bookingDetails.VehicleNo));
                return Ok(result ? UTILITY.SUCCESSMSG : UTILITY.FAILEDMSG);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        //[HttpGet]
        //[Route("drivergeOposition/{driverId}")]
        //public IHttpActionResult GetDriverGeoPosition(string driverID)
        //{
        //    try
        //    {
        //        var driverActivity = new DriverActivityBO().GetDriverActivityByDriverID(new DriverActivity { DriverID = driverID });

        //        return Ok(new
        //        {
        //            CurrentLat = driverActivity.CurrentLat,
        //            CurrentLong = driverActivity.CurrentLong
        //        });
        //    }
        //    catch (Exception ex)
        //    {
        //        return InternalServerError(ex);
        //    }
        //}
        
        /* vijay: this is called only by driver. at the time of customer payment itself we are 
           calling this functionality internally. to make independent
        */
        [HttpGet]
        [Route("DriverReceivedConfirm/{BookingNo}")]
        public IHttpActionResult DriverReceivedConfirmCheck(string BookingNo)
        {
            /* calling below BO from customer side directly */
            //var result = new BookingBO().CustomerPaymentUpdate(BookingNo);
            if (true)
            {
                PushNotification(new BookingBO().GetCustomerDeviceIDByBookingNoByPaymentReceiveConfirm(BookingNo),
                       BookingNo,
                       UTILITY.NotifyDriverpaymentReceived);
                var invoiceAmount = new InvoiceBO().GetInvoice(new Invoice { BookingNo= BookingNo });
                var bookingDetails = new BookingBO().GetList().Where(x => x.BookingNo == BookingNo).FirstOrDefault();
                if (bookingDetails.CustomerId != bookingDetails.ReceiverMobileNo)
                {
                    string DriverDetails =string.Format("Payment for {0} of rupees {1} done successfully." ,bookingDetails.BookingNo, invoiceAmount.TotalAmount);
                    SendDriverDetailsToCustomer(bookingDetails.ReceiverMobileNo, DriverDetails);
                }
                    return Ok(new { UTILITY.NotifyDriverpaymentReceived });
            }
            else
                return Ok(new { UTILITY.FAILEDMESSAGE });
        }

        [HttpGet]
        [Route("bookingHistoryList/{MobileNo}")]
        public IHttpActionResult BookingHistoryListbyMobileNo(string MobileNo)
        {
            try
            {
                var bookingHistoryList = new BookingBO().GetBookingListByMobileNo(MobileNo);
                if (bookingHistoryList != null)
                    return Ok(bookingHistoryList);
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpGet]
        [Route("PickupTripStartbyBookingNo/{BookingNo}")]
        public IHttpActionResult PickupTripStartbyTripID(string BookingNo)
        {
            try
            {
                PushNotification(new BookingBO().GetCustomerDeviceIDByBookingNo(BookingNo),
                       BookingNo,UTILITY.NotifyCustomerPickupStart);
                //var bookingDetails = new BookingBO().GetList().Where(x => x.BookingNo == BookingNo).FirstOrDefault();
                //if (bookingDetails.CustomerId != bookingDetails.ReceiverMobileNo)
                //{
                //    SendDriverDetailsToCustomer(bookingDetails.ReceiverMobileNo, UTILITY.NotifyCustomerPickupStart);
                //}
                var BookingConfirm = new BookingBO().CustomerCurrentConfirmTrip(HeaderValueByKey("MOBILENO"));
                var Isintrip = BookingConfirm != null ? true : false;
                if (Isintrip)
                {
                    return Ok(new { BookingNo = BookingConfirm.BookingNo, OTP = BookingConfirm.OTP });
                }
                else
                {
                    return Ok(new { BookingNo = "", OTP = "" });
                }
                //return Ok(UTILITY.NotifyCustomerPickupStart);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        [HttpGet]
        [Route("UpdateDriverBusyStatus")]
        public IHttpActionResult ChangeDriverIsBusyStatus()
        {
            try
            {
                var driverActivity = new DriverActivity
                {
                    DriverId = HeaderValueByKey("DRIVERID"),
                    TokenNo = HeaderValueByKey("AUTH_TOKEN")
                };
                if (new DriverBO().UpdateDriverBusyStatus(driverActivity.DriverId, driverActivity.TokenNo))
                {
                    return Ok(true);
                }
                else
                {
                    return Ok(false);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
