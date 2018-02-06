using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using PickC.Services;
using PickC.Internal2.ViewModals;
using PickC.Services.DTO;
using Operation.Contract;
using Operation.BusinessFactory;
using Master.Contract;

namespace PickC.Internal2.Controllers
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
					tripMonitor.address = new ViewModals.Address
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

		public async Task<ActionResult> GetDriversList(string status = "ALL")
		{
            //var status = "ALL";
            ViewBag.Status = status;

            var driverList = await new DriverService(AUTHTOKEN, p_mobileNo).GetDriverBySearch(status);
			//var tripMonitor = await GetTripMonitorData();

            var tripMonitorData = new List<TripMonitorVm>();
            if (driverList != null)
            {
                for (var i = 0; i < driverList.Count; i++)
                {
                    if (driverList[i].CurrentLat != 0 && driverList[i].CurrentLong != 0)
                    {
                        var tripMonitor = new TripMonitorVm();
                        tripMonitor.address = new ViewModals.Address
                        {
                            address = "",
                            lat = driverList[i].CurrentLat.ToString(),
                            lng = driverList[i].CurrentLong.ToString(),
                        };
                        tripMonitor.title = driverList[i].DriverId;
                        tripMonitorData.Add(tripMonitor);
                    }
                }
            }

            var driverMonitorVm = new DriverMonitorVm()
			{
				driverList = driverList,
				tripMonitorVmList = tripMonitorData
            };

			return View(driverMonitorVm);
			//return View();
		}

		[HttpPost]
		public async Task<ActionResult> CurrentBookings(BookingDTO search)
		{
			var currentbookings = await new SearchService(AUTHTOKEN, p_mobileNo).SearchCurrentBookingAsync(search);
			var bookingSearchVM = new BookingSearchDTO();
			bookingSearchVM.booking = currentbookings;

			var tripMonitorData = new List<TripMonitorVm>();
			if (currentbookings != null)
			{
				for (var i = 0; i < currentbookings.Count; i++)
				{
					var tripMonitor = new TripMonitorVm();
					tripMonitor.address = new ViewModals.Address
					{
						address = "",
						lat = currentbookings[i].Latitude.ToString(),
						lng = currentbookings[i].Longitude.ToString(),
					};
					tripMonitor.title = currentbookings[i].DriverId;
					tripMonitorData.Add(tripMonitor);
				}

				ViewBag.trips = tripMonitorData;
			}
			return View("SearchBookingHistory", bookingSearchVM);
		}
		[HttpGet]
		public async Task<ActionResult> CurrentBookings()
		{
			var Driver = (await new OperatorDriverService(AUTHTOKEN, p_mobileNo).GetDriverList()).Select(x => new SelectValueText() { Value = x.DriverID, Text = x.DriverName }).ToList();
			var VehicleNo = (await new OperatorDriverService(AUTHTOKEN, p_mobileNo).GetvehicleNoList()).Select(x => new SelectValueText() { Value = x.VehicleRegistrationNo, Text = x.VehicleRegistrationNo }).ToList();
			var VehicleType = (await new OperatorVehicleService(AUTHTOKEN, p_mobileNo).GetOperatorVehicleList()).Select(x => new SelectValueText() { Value = x.LookupId.ToString(), Text = x.LookupCode }).ToList();
			var VehicleCategory = (await new OperatorVehicleService(AUTHTOKEN, p_mobileNo).GetOperatorVehicleCategoryList()).Select(x => new SelectValueText { Value = x.LookupId.ToString(), Text = x.LookupCode }).ToList();
			//var currentbookings = await new SearchService(AUTHTOKEN, p_mobileNo).BookingListAsync();
			var currentbookings = await new SearchService(AUTHTOKEN, p_mobileNo).GetCustomerBySearch(0);
			var bookingSearchVM = new BookingSearchDTO();
			bookingSearchVM.booking = currentbookings;
			//var tripMonitor = await GetTripMonitorData();
			//ViewBag.trips = tripMonitor;
			var tripMonitorData = new List<TripMonitorVm>();
			if (currentbookings != null)
			{
				for (var i = 0; i < currentbookings.Count; i++)
				{
					var tripMonitor = new TripMonitorVm();
					tripMonitor.address = new ViewModals.Address
					{
						address = "",
						lat = currentbookings[i].Latitude.ToString(),
						lng = currentbookings[i].Longitude.ToString(),
					};
					tripMonitor.title = currentbookings[i].DriverId;
					tripMonitor.BookingNo = currentbookings[i].BookingNo;
					tripMonitor.DriverName = Driver.AsEnumerable().Where(x => x.Value == currentbookings[i].DriverId).Select(x => x.Text).ToString();
					tripMonitor.DriverName = getName(Driver, currentbookings[i].DriverId);
					tripMonitor.LocationFrom = currentbookings[i].LocationFrom;
					tripMonitor.LocationTo = currentbookings[i].LocationTo;
					tripMonitor.VehicleNo = getName(VehicleNo,currentbookings[i].VehicleNo);
					tripMonitor.VehicleType = getSName(VehicleType, currentbookings[i].VehicleType);//(await new OperatorVehicleService(AUTHTOKEN, p_mobileNo).GetOperatorVehicleList()).Where(x => x.LookupId == currentbookings[i].VehicleType).Select(x => x.LookupCode).ToString(); //currentbookings[i].VehicleTypeDescription;
					tripMonitor.VehicleCategory = getSName(VehicleCategory, currentbookings[i].VehicleGroup);//(await new OperatorVehicleService(AUTHTOKEN, p_mobileNo).GetOperatorVehicleCategoryList()).Where(x => x.LookupId == currentbookings[i].VehicleGroup).Select(x => x.LookupCode).ToString();
					tripMonitor.ETA = "";
					tripMonitor.TripStartTime = "";
					tripMonitor.ArrivalTime = "";
					tripMonitor.WaitingTime = "";
					tripMonitor.Status = currentbookings[i].Status;
					tripMonitorData.Add(tripMonitor);
				}

				ViewBag.trips = tripMonitorData;
			}
			return View(bookingSearchVM);
		}

		private string getName(List<SelectValueText> List, string id)
		{
			var result = List.SingleOrDefault(s => s.Value == id);
			if (result != null)
			{
				return result.Text;
			}
			return "";
		}
		private string getSName(List<SelectValueText> List, short id)
		{
			var result = List.SingleOrDefault(s => Convert.ToInt16(s.Value) == id);
			if (result != null)
			{
				return result.Text;
			}
			return "";
		}

		


		[HttpPost]
		public async Task<ActionResult> GetCustomerBySearch(int? status)
		{
			ViewBag.Status = status;
			var Driver = (await new OperatorDriverService(AUTHTOKEN, p_mobileNo).GetDriverList()).Select(x => new SelectValueText() { Value = x.DriverID, Text = x.DriverName }).ToList();
			var VehicleNo = (await new OperatorDriverService(AUTHTOKEN, p_mobileNo).GetvehicleNoList()).Select(x => new SelectValueText() { Value = x.VehicleRegistrationNo, Text = x.VehicleRegistrationNo }).ToList();
			var VehicleType = (await new OperatorVehicleService(AUTHTOKEN, p_mobileNo).GetOperatorVehicleList()).Select(x => new SelectValueText() { Value = x.LookupId.ToString(), Text = x.LookupCode }).ToList();
			var VehicleCategory = (await new OperatorVehicleService(AUTHTOKEN, p_mobileNo).GetOperatorVehicleCategoryList()).Select(x => new SelectValueText { Value = x.LookupId.ToString(), Text = x.LookupCode }).ToList();

			var currentbookings = await new SearchService(AUTHTOKEN, p_mobileNo).GetCustomerBySearch(status);
			var bookingSearchVM = new BookingSearchDTO();
			bookingSearchVM.booking = currentbookings;
			var tripMonitorData = new List<TripMonitorVm>();
			if (currentbookings != null)
			{
				for (var i = 0; i < currentbookings.Count; i++)
				{
					var tripMonitor = new TripMonitorVm();
					tripMonitor.address = new ViewModals.Address
					{
						address = "",
						lat = currentbookings[i].Latitude.ToString(),
						lng = currentbookings[i].Longitude.ToString(),
					};
					tripMonitor.title = currentbookings[i].DriverId.ToUpper() + " - " + currentbookings[i].BookingNo.ToUpper();
					tripMonitor.BookingNo = currentbookings[i].BookingNo;
					tripMonitor.DriverName = Driver.AsEnumerable().Where(x => x.Value == currentbookings[i].DriverId).Select(x => x.Text).ToString();
                    tripMonitor.DriverName = currentbookings[i].DriverName;
					tripMonitor.LocationFrom = currentbookings[i].LocationFrom;
					tripMonitor.LocationTo = currentbookings[i].LocationTo;
					tripMonitor.VehicleNo = getName(VehicleNo,currentbookings[i].VehicleNo);
					tripMonitor.VehicleType = getSName(VehicleType, currentbookings[i].VehicleType);//(await new OperatorVehicleService(AUTHTOKEN, p_mobileNo).GetOperatorVehicleList()).Where(x => x.LookupId == currentbookings[i].VehicleType).Select(x => x.LookupCode).ToString(); //currentbookings[i].VehicleTypeDescription;
					tripMonitor.VehicleCategory = getSName(VehicleCategory, currentbookings[i].VehicleGroup);//(await new OperatorVehicleService(AUTHTOKEN, p_mobileNo).GetOperatorVehicleCategoryList()).Where(x => x.LookupId == currentbookings[i].VehicleGroup).Select(x => x.LookupCode).ToString();
					tripMonitor.ETA = "";
					tripMonitor.TripStartTime = "";
					tripMonitor.ArrivalTime = "";
					tripMonitor.WaitingTime = "";
					tripMonitor.Status = currentbookings[i].Status;
					tripMonitorData.Add(tripMonitor);

				}

				ViewBag.trips = tripMonitorData;
			}
			return View("CurrentBookings", bookingSearchVM);
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
		public async Task<ActionResult> BookingHistory()
		{
			var bookingHistory1 = await new SearchService(AUTHTOKEN, p_mobileNo).BookingListAsync();
			var bookingSearchVM = new BookingHistoryDTO();
			bookingSearchVM.booking = bookingHistory1;
			bookingSearchVM.bookings = new BookingSearchsDTO();
			DateTime dateTime = DateTime.Now;
			bookingSearchVM.bookings.DateFrom = new DateTime(dateTime.Year, dateTime.Month, 1);
			bookingSearchVM.bookings.DateTo = DateTime.Now;
			return View("BookingHistory", bookingSearchVM);
		}

		public async Task<PartialViewResult> BookingInvoice(string bookingNo)
		{
			try
			{
				var tripInvoiceList = await new RegisterService(AUTHTOKEN, p_mobileNo).GetBookingInvoiceByBookingNoAsync(bookingNo);
				if (tripInvoiceList == null)
				{
					tripInvoiceList = new TripInvoice();
				}
				return PartialView("BookingInvoice", tripInvoiceList);
			}
			catch (Exception ex)
			{
				return null;
			}
		}

		[HttpPost]
		public async Task<ActionResult> BookingsHistory(BookingHistoryDTO search)
		{
			var bookingHistory = await new SearchService(AUTHTOKEN, p_mobileNo).SearchBookingAsync(search.bookings);
			var bookingSearchVM = new BookingHistoryDTO();
			bookingSearchVM.booking = bookingHistory;

			return View("SearchBookingHistory", bookingSearchVM);
		}

		[HttpGet]
		public async Task<ActionResult> UserApp()
		{


			var userData = new UserData();
			//DateTime dateTime = DateTime.Now;
			//userData.DateFrom = new DateTime(dateTime.Year, dateTime.Month, 1);
			//userData.DateTo = DateTime.Now;
			//userData.userBookingList = await new UserService(AUTHTOKEN, p_mobileNo).searchBookingTripsAsync(userData);
			userData.userDataDashBoard = new UserDataDashBoard();
			userData.userDataDashBoard = await new UserService(AUTHTOKEN, p_mobileNo).GetDashBoardUserData();
			userData.tripDailyBasis = new List<TripDailyBasis>();
			userData.tripDailyBasis = await new UserService(AUTHTOKEN, p_mobileNo).getTotalTripDailyBasis();

			return View("UserApp", userData);
		}
		[HttpGet]
		public async Task<ActionResult> RegisteredList()
		{
			var userData = new UserData();
			DateTime dateTime = DateTime.Now;
			userData.DateFrom = new DateTime(dateTime.Year, dateTime.Month, 1);
			userData.DateTo = DateTime.Now;
			userData.customerStatusList = new List<CustomerStatus>();
			userData.customerStatusList = await new UserService(AUTHTOKEN, p_mobileNo).GetRegisteredButNotBookedList();
			userData.userDataDashBoard = await new UserService(AUTHTOKEN, p_mobileNo).GetDashBoardUserData();
			return View("RegisteredList", userData);
		}
		[HttpPost]
		public async Task<ActionResult> RegisteredList(UserData user)
		{
			var userData = new UserData();
			var CustomerList = await new UserService(AUTHTOKEN, p_mobileNo).GetRegisteredButNotBookedList(user.DateFrom, user.DateTo);
			userData.userDataDashBoard = await new UserService(AUTHTOKEN, p_mobileNo).GetDashBoardUserData();
			userData.customerStatusList = new List<CustomerStatus>();
			userData.customerStatusList = CustomerList;
			userData.DateFrom = DateTime.Now;
			userData.DateTo = DateTime.Now;
			return View("RegisteredList", userData);
		}
		[HttpPost]
		public async Task<ActionResult> SearchTotalTrips(UserData userdata)
		{
			var UserList = await new UserService(AUTHTOKEN, p_mobileNo).searchBookingTripsAsync(userdata);
			var userData = new UserData();
			userData.userBookingList = UserList;
			userData.userDataDashBoard = new UserDataDashBoard();
			userData.userDataDashBoard = await new UserService(AUTHTOKEN, p_mobileNo).GetDashBoardUserData();
			return View("UserApp", userData);
		}

		[HttpGet]
		public async Task<ActionResult> UserAppCancellation()
		{
			var userData = new UserData();
			userData.customerCancellation = await new UserService(AUTHTOKEN, p_mobileNo).getCancelledList();
			userData.driverCancellation = await new UserService(AUTHTOKEN, p_mobileNo).getCancelledListDriver();
			userData.userDataDashBoard = await new UserService(AUTHTOKEN, p_mobileNo).GetDashBoardUserData();

			return View("UserAppCancellation", userData);
		}

		//[HttpPost]
		//public ActionResult UserAppCancellation(Cancellation cancellist)
		//{
		//    Cancellation result = new Cancellation();
		//    result.customerCancellation = new List<CustomerCancellation>();
		//    result.driverCancellation = new List<DriverCancellation>();
		//    return View("UserAppCancellation", result);
		//}

		//public async Task<ActionResult> getRegisteredList()
		//{
		//    return View("UserRegister");
		//}
		[HttpGet]
		public async Task<ActionResult> PaymentHistory()
		{
			PaymentHistory data = new PaymentHistory();
			data.paymentsearch = new Paymentsearch();
			data.customerdetails = new List<CustomerDetails>();
			data.driverCommissiondetails = new List<DriverCommissionDetails>();
			data.pickCCommissiondetails = new List<pickCCommissionDetails>();
			data.dailyPaymentHistory = new DailyPaymentHistory();
			DateTime dateTime = DateTime.Now;
			data.paymentsearch.DateFrom = new DateTime(dateTime.Year, dateTime.Month, 1);
			data.paymentsearch.DateTo = DateTime.Now;
			var paymentresult = await new PaymentService(AUTHTOKEN, p_mobileNo).PaymentHistoryDetails(data.paymentsearch);
			data.customerdetails = paymentresult.customerdetails;
			data.driverCommissiondetails = paymentresult.driverCommissiondetails;
			data.pickCCommissiondetails = paymentresult.pickCCommissiondetails;
			data.dailyPaymentHistory = await new PaymentService(AUTHTOKEN, p_mobileNo).DailyPaymentHistory();
			return View("PaymentHistory", data);
		}
		[HttpPost]
		public async Task<ActionResult> PaymentHistory(PaymentHistory payment)
		{
			var data = await new PaymentService(AUTHTOKEN, p_mobileNo).PaymentHistoryDetails(payment.paymentsearch);
            data.dailyPaymentHistory = await new PaymentService(AUTHTOKEN, p_mobileNo).DailyPaymentHistory();

            return View("PaymentHistory", data);
		}
		[HttpGet]
		public async Task<ActionResult> PendingAmount()
		{
			PendingCommission pending = new PendingCommission();
			pending.driverPendingCommision = await new PaymentService(AUTHTOKEN, p_mobileNo).driverpendingAmountDetails();
			pending.driverPendingAmount = await new DriverService(AUTHTOKEN, p_mobileNo).DriverPendingAmountAsync();

			return View(pending);
		}
		[HttpPost]
		public ActionResult PendingAmounts(PendingAmountDTO pendingAmountDTO)
		{
			return View();
		}
		public async Task<JsonResult> GetDriverBySearch(string status)
		{
			var driverList = await new DriverService(AUTHTOKEN, p_mobileNo).GetDriverBySearch(status);
			return Json(driverList, JsonRequestBehavior.AllowGet);
		}
		public async Task<JsonResult> GetDriverDetails(string id)
		{
			var driverlist = await new DriverService(AUTHTOKEN, p_mobileNo).DriverByIDInfoAsync(id); //DriverInfoAsync(id); added by Kiran
			return Json(driverlist, JsonRequestBehavior.AllowGet);
		}

		public async Task<JsonResult> IsValidOperatorId(string operatorId)
		{
			string operatorDetails = await new DriverService(AUTHTOKEN, p_mobileNo).IsOperatorValid(operatorId);
			int i = Convert.ToInt32(operatorDetails);
			return Json(i, JsonRequestBehavior.AllowGet);
		}

		[HttpGet]
		public ActionResult DriverRevoke()
		{
			return View();
		}
	}

	public class SelectValueText
	{
		public string Value { get; set; }
		public string Text { get; set; }
	}
}