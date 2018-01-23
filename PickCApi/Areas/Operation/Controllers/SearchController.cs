using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Operation.BusinessFactory;

using PickC.Services.DTO;
using PickCApi.Core;

namespace PickCApi.Areas.Operation.Controllers
{
    [RoutePrefix("api/operation/search")]
    [ApiAuthFilter]
    public class SearchController : ApiBase
    {
        [HttpPost]
        [Route("currentbooking")]
        public IHttpActionResult CurrentBookingSearch(BookingDTO bookingDTO)
        {
            try
            {
                var bookingResult = new SearchBO().SearchBookings(
                    bookingDTO.BookingNo, bookingDTO.BookingDate,
                    bookingDTO.VehicleGroup, bookingDTO.VehicleType,
                    bookingDTO.CustomerName, bookingDTO.VehicleNumber);

                return Ok(bookingResult);
            }
            catch (Exception ex)
            {

                return InternalServerError(ex);
            }
        }

        [HttpPost]
        [Route("bookingbydate")]
        public IHttpActionResult BookingByDate(BookingSearchDTO booking)
        {
            try
            {
                var bookingresult = new SearchBO().BookingByDate(booking.dates.fromDate,booking.dates.toDate);
                return Ok(bookingresult);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

        }
        [HttpGet]
        [Route("list")]
        public IHttpActionResult CurrentBookingList()
        {
            try
            {
                var BookingList = new SearchBO().GetList();

                if (BookingList != null)
                    return Ok(BookingList);
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        [HttpPost]
        [Route("list/driverbyname/{status}")]
        public IHttpActionResult CurrentBookingByStatus(int? status)
        {
            try
            {
                var bookingresult = new SearchBO().CurrentBookingByStatus(status);
                return Ok(bookingresult);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

        }
        [HttpPost]
        [Route("bookingHistory")]
        public IHttpActionResult BookingHistorySearch(BookingSearchsDTO bookingDTO)
        {
            try
            {
                
                var bookingResult = new SearchBO().SearchBookingsHistory(
                    bookingDTO.DateFrom.ToLocalTime(),
                    bookingDTO.DateTo.ToLocalTime());

                return Ok(bookingResult);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

    }
}
