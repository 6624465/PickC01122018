using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;

using PickC.Services;
using PickC.Internal.ViewModals;
using PickC.Services.DTO;
using Operation.Contract;
using Operation.BusinessFactory;

namespace PickC.Internal.Areas.Internal.Controllers
{
    [WebAuthFilter]
    [PickCEx]
    public class DashboardController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            return View(await GetTripMonitorData());
        }

        [HttpGet]
        public async Task<JsonResult> GetTripMonitorInfo()
        {
            return Json(await GetTripMonitorData(), JsonRequestBehavior.AllowGet);
        }

        public async Task<List<TripMonitorVm>> GetTripMonitorData()
        {
            var tripMonitorList = await new TripMonitorService(AUTHTOKEN, p_mobileNo)
                                            .TripMonitorListAsync();

            var tripMonitorData = new List<TripMonitorVm>();
            if (tripMonitorList != null)
            {
                for (var i = 0; i < tripMonitorList.Count; i++)
                {
                    var tripMonitor = new TripMonitorVm();
                    tripMonitor.address = new Address
                    {
                        address = "",
                        lat = tripMonitorList[i].Latitude,
                        lng = tripMonitorList[i].Longitude,
                    };
                    tripMonitor.title = tripMonitorList[i].DriverID + " - " + tripMonitorList[i].TripID;

                    tripMonitorData.Add(tripMonitor);
                }
            }


            return tripMonitorData;
        }

        public async Task<ActionResult> GetDriversList()
        {
            var driverList = await new DriverService(AUTHTOKEN, p_mobileNo).DriversListAsync();
            var tripMonitor = await GetTripMonitorData();

            var driverMonitorVm = new DriverMonitorVm()
            {
                driverList = driverList,
                tripMonitorVmList = tripMonitor
            };

            return View(driverMonitorVm);
        }

        //[HttpGet]
        //public async Task<ActionResult> CurrentBookings(BookingDTO search)
        //{
        //    var currentbookings = await new SearchService(AUTHTOKEN, p_mobileNo).SearchCurrentBookingAsync(search);
        //    var bookingSearchVM = new BookingSearchDTO();
        //    bookingSearchVM.booking = currentbookings;

        //    return View("CurrentBookings", bookingSearchVM);
        //}
        [HttpGet]
        public async Task<ActionResult> CurrentBookings()
        {
            var currentbookings = await new SearchService(AUTHTOKEN, p_mobileNo).BookingListAsync();
            var bookingSearchVM = new BookingSearchDTO();
            bookingSearchVM.booking = currentbookings;
            return View(bookingSearchVM);
        }
        public async Task<JsonResult> GetCustomerBySearch(int? status)
        {
            var customerlist = await new SearchService(AUTHTOKEN, p_mobileNo).GetCustomerBySearch(status);
            return Json(customerlist, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> CurrentBookings(BookingSearchDTO booking)
        {
            var currentbooking = await new SearchService(AUTHTOKEN, p_mobileNo).SearchBookingByDateAsync(booking);
            var bookingSearchVM = new BookingSearchDTO();
            bookingSearchVM.booking = currentbooking;

            return View("CurrentBookings", bookingSearchVM);

        }
        [HttpGet]
        public async Task<ActionResult> BookingHistory(BookingHistoryDTO search)
        {
            var bookingHistory = await new SearchService(AUTHTOKEN, p_mobileNo).SearchBookingHistoryAsync(search);
            var bookingSearchVM = new BookingHistoryDTO();
            bookingSearchVM.bookingHistory = bookingHistory;
            return View("BookingHistory", bookingSearchVM);
        }
        //[HttpPost]
        //public async Task<ActionResult> BookingsHistory(BookingHistoryDTO search)
        //{
        //    var bookingHistory = await new SearchService(AUTHTOKEN, p_mobileNo).SearchBookingHistoryAsync(search);
        //    var bookingSearchVM = new BookingHistoryDTO();
        //    bookingSearchVM.bookingHistory = bookingHistory;
        //    return View("BookingHistory", bookingSearchVM);
        //}
        [HttpGet]
        public ActionResult UserApp()
        {
            return View("UserApp");
        }
        [HttpGet]
        public ActionResult PaymentHistory()
        {
            return View("PaymentHistory");
        }
        public ActionResult PendingAmount()
        {
            return View("PendingAmount");
        }
        public async Task<JsonResult> GetDriverBySearch(bool? status=null)
        {
          
            var driverlist = await new DriverService(AUTHTOKEN, p_mobileNo).GetDriverBySearch(status);
            return Json(driverlist, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> GetDriverDetails(string id) {
            var driverlist = await new DriverService(AUTHTOKEN, p_mobileNo).DriverInfoAsync(id);
            return Json(driverlist, JsonRequestBehavior.AllowGet);
        }
    }
}