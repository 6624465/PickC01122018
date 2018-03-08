using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Master.Contract;
using Master.BusinessFactory;
using Operation.BusinessFactory;
using Operation.Contract;
using PickCApi.Core;

using PickCApi.Areas.Master.DTO;
using PickCApi.Utility;
using System.Web.Routing;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using System.Web;
using System.Net.Mail;
using System.Net.Configuration;
using System.Web.Configuration;
using System.Configuration;
using iTextSharp.tool.xml;
using PickCApi.Areas.Operation.DTO;
using PickCApi.Areas.Operation.Controllers;
using PickC.Services.DTO;
using System.Web.Http.Tracing;

namespace PickCApi.Areas.Master.Controllers
{
    [RoutePrefix("api/master/customer")]
    public class RegisterController : ApiBase
    {
        [HttpGet]
        [Route("customerList")]
        [ApiAuthFilter]
        public IHttpActionResult GetCustomerList()
        {
            try
            {
                var customerList = new CustomerBO().GetList().Select(x => new
                {
                    MobileNo = x.MobileNo,
                    Name = x.Name,
                    EmailID = x.EmailId
                }).ToList();
                if (customerList != null)
                    return Ok(customerList);
                else
                    return Ok(new List<Customer>());
            }
            catch (Exception ex)
            {
                return Ok(new List<Customer>());
            }
        }

        [HttpPost]
        [Route("login")]
        public IHttpActionResult Login(Customer customer)
        {
            try
            {
                var token = new CustomerLogInBO().CustomerLogIn(customer.MobileNo, customer.Password);

                if (!string.IsNullOrWhiteSpace(token))
                {
                    var _customer = new CustomerBO().GetCustomer(new Customer { MobileNo = customer.MobileNo });
                    if (_customer.IsOTPVerified)
                        return Ok(new
                        { Token = token, Status = UTILITY.SUCCESS });
                    else
                        return Ok(new { Token = "", Status = UTILITY.FAIL });
                }
                else
                    return Ok(new { Token = "", Status = UTILITY.FAIL });
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        [HttpGet]
        [Route("check/{mobile}")]
        //[ApiAuthFilter]
        public IHttpActionResult CheckMobile(string mobile)
        {
            bool result = false;
            try
            {
                var customerList = new CustomerBO().GetList();

                if (customerList != null)
                {
                    result = customerList.Where(x => x.MobileNo == mobile).ToList().Count() > 0;
                    return Ok(result);
                }
                else
                    return Ok(result);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        [HttpPost]
        [Route("save")]
        // [ApiAuthFilter]
        public IHttpActionResult CreateCustomer(Customer customer)
        {
            try
            {
                customer.OTP = GenerateOTP();
                customer.IsOTPVerified = false;
                customer.OTPSendDate = DateTime.Now;
                customer.OTPVerifiedDate = null;

                var result = new CustomerBO().SaveCustomer(customer);
                if (result)
                {
                    SendOTP(customer.MobileNo, string.Format(UTILITY.SmsSignUpOTP, customer.OTP));
                    return Ok(UTILITY.SUCCESS);
                }
                else
                    return Ok(UTILITY.FAIL);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        [HttpGet]
        [Route("verifyOtp/{mobile}/{otp}")]
        //[ApiAuthFilter]
        public IHttpActionResult VerifyOTP(string mobile, string otp)
        {
            try {
            var customer = new CustomerBO().GetCustomer(new Customer { MobileNo = mobile });
            if (customer.OTP == otp)
            {
                customer.IsOTPVerified = true;
                customer.OTPVerifiedDate = DateTime.UtcNow;
                new CustomerBO().SaveCustomer(customer);
                return Ok(UTILITY.SUCCESS);
            }
            else
            {
                return Ok(UTILITY.FAIL);
            }
            }catch(Exception ex)
            {
                return InternalServerError(ex);
        }
        }
        [HttpGet]
        [Route("{mobile}")]
        [ApiAuthFilter]
        public IHttpActionResult GetCustomer(string mobile)
        {
            try
            {
                var customer = new CustomerBO().GetCustomer(new Customer { MobileNo = mobile });
                if (customer != null)
                    return Ok(new
                    {
                        MobileNo = customer.MobileNo,
                        Name = customer.Name,
                        EmailID = customer.EmailId,
                        Status = UTILITY.SUCCESS
                    });
                else
                    return Ok(new { MobileNo = "", Name = "", EmailID = "", Status = UTILITY.FAIL });
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        [HttpPost]
        [Route("{mobile}")]
        [ApiAuthFilter]
        public IHttpActionResult UpdateCustomer(string mobile, Customer customer)
        {
            try
            {
                var customerBO = new CustomerBO();
                var customerObj = customerBO.GetCustomer(new Customer { MobileNo = mobile });
                if (customerObj != null)
                {
                    customerObj.Name = customer.Name;
                    customerObj.EmailId = customer.EmailId;
                    customerObj.Password = customer.Password;

                    var result = customerBO.SaveCustomer(customerObj);
                    if (result)
                        return Ok(UTILITY.SUCCESS);
                    else
                        return Ok(UTILITY.FAIL);
                }
                else
                    return Ok(UTILITY.FAIL);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPost]
        [Route("changePassword/{mobile}")]
        //  [ApiAuthFilter]
        public IHttpActionResult ChangePassword(CustomerPassword customerPassword)
        {
            var result = false;
            try
            {
                result = new CustomerBO().UpdateCustomerPassword(customerPassword);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpGet]
        [Route("forgotPassword/{mobile}")]
        public IHttpActionResult Forgotpassword(string mobile)
        {
            try {
            var customer = new CustomerBO().GetCustomer(new Customer { MobileNo = mobile });
            if (customer != null)
            {
                customer.OTP = GenerateOTP();
                new CustomerBO().SaveCustomer(customer);
                SendOTP(customer.MobileNo, customer.OTP);
                return Ok(UTILITY.SUCCESS);
            }
            else
                return Ok(UTILITY.FAIL);
            }
            catch(Exception ex)
            {
                return InternalServerError(ex);
            }

        }
        [HttpPost]
        [Route("forgotPassword")]

        public IHttpActionResult Forgotpassword(ForgotPasswordDTO forgot)
        {
            try {
            var customer = new CustomerBO().GetCustomer(new Customer { MobileNo = forgot.MobileNo });
            if (customer != null && customer.OTP == forgot.OTP)
            {
                customer.Password = forgot.NewPassword;
                new CustomerBO().SaveCustomer(customer);

                return Ok(UTILITY.SUCCESS);
            }
            else
                return Ok(UTILITY.FAIL);
            }catch(Exception ex)
            {
                return InternalServerError(ex);
        }
        }
        [HttpGet]
        [Route("bookingHistoryListbyCustomerMobileNo/{mobileNo}")]
        [ApiAuthFilter]
        public IHttpActionResult BookingHistoryListbyCustomerMobileNo(string MobileNo)
        {
            try
            {
                var bookingList = new BookingBO().GetBookingHistoryList().Where(x => x.CustomerID == MobileNo);
                if (bookingList != null)
                    return Ok(bookingList);
                else
                    return Ok(new List<BookingHistoryList>());
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpGet]
        [Route("checkCustomerPassword/{mobile}/{password}")]
        [ApiAuthFilter]
        public IHttpActionResult CheckCustomerPassword(string mobile, string password)
        {
            try
            {
                var customer = new CustomerBO().GetList().Where(x => x.MobileNo == mobile).ToList();
                string CusPassword = customer.Select(x => x.Password).FirstOrDefault();
                if (CusPassword != "" && CusPassword == password)
                {
                    return Ok(UTILITY.SUCCESS);
                }
                else
                {
                    return Ok(UTILITY.FAIL);
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        [HttpPost]
        [Route("user")]
        [ApiAuthFilter]
        public IHttpActionResult GetTrucksInRange(TrucksInRangeDTO trucksInRangeDTO)
        {
            try
            {
                var trucks = new BookingBO().GetTrucksInRange(
                    HeaderValueByKey("MOBILENO"),
                    trucksInRangeDTO.Latitude,
                    trucksInRangeDTO.Longitude,
                    UTILITY.radius, trucksInRangeDTO.VehicleGroup, trucksInRangeDTO.VehicleType);
                if (trucks != null)
                    return Ok(trucks);
                else
                    return Ok(new List<NearTrucksInRange>());
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        [HttpGet]
        [Route("isInTrip")]
        [ApiAuthFilter]
        public IHttpActionResult IsCustomerInTrip()
        {
            try
            {
                var trip = new TripBO().CustomerCurrentTrip(HeaderValueByKey("MOBILENO"));
                return Ok(new
                {
                    IsInTrip = trip != null ? true : false,
                    BookingNo = (trip != null ? trip.BookingNo : "")
                });
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        [HttpPost]
        [Route("bookingSave")]
        [ApiAuthFilter]
        public IHttpActionResult SaveBooking(Booking booking)
        {
            try
            {
                var result = new BookingBO().SaveBooking(booking);
                if (result)
                {
                    Booking bookings = new BookingBO().GetBooking(new Booking
                    {
                        BookingNo = booking.BookingNo
                    });
                    var driverList = new BookingBO().GetNearTrucksDeviceID(bookings.BookingNo,
                        UTILITY.radius,
                        bookings.VehicleType,
                        bookings.VehicleGroup,
                        bookings.Latitude,
                        bookings.Longitude);//UTILITY.radius
                    if (driverList.Count > 0)
                    {
                        PushNotification(driverList.Select(x => x.DeviceId).ToList<string>(),
                            booking.BookingNo, UTILITY.NotifyNewBooking);
                        return Ok(new
                        {
                            BookingNo = booking.BookingNo,
                            Status = UTILITY.BOOKINGSUCCESS
                        });
                    }
                    else
                    {
                        var CancelBooking = new BookingBO().DeleteBooking(new Booking { BookingNo = booking.BookingNo });
                        if (CancelBooking)
                            return Ok(new { BookingNo = "", Status = UTILITY.NotifyCustomer });
                        else
                            return Ok(new { BookingNo = "", Status = UTILITY.FAILURESTATUS });
                    }
                }
                else
                    return Ok(result);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        [HttpGet]
        [Route("isConfirm/{bookingNo}")]
        [ApiAuthFilter]
        public IHttpActionResult IsConfirmBooking(string bookingNo)
        {
            try
            {
                var booking = new BookingBO().GetBooking(new Booking
                {
                    BookingNo = bookingNo
                });

                var driverInfo = new Driver();
                var driverActivity = new DriverActivity();
                if (booking.IsConfirm)
                {
                    driverInfo = new DriverBO().GetDriver(new Driver { DriverId = booking.DriverId });
                    driverActivity = new DriverActivityBO().GetDriverActivityByDriverID(new DriverActivity { DriverId = booking.DriverId });
                }

                if (booking != null)
                    return Ok(new
                    {
                        IsConfirm = booking.IsConfirm,
                        DriverId = driverInfo.DriverId ?? "",
                        VehicleNo = driverInfo.VehicleNo ?? "",
                        DriverName = driverInfo.DriverName ?? "",
                        DriverImage = "",
                        MobileNo = driverInfo.MobileNo ?? "",
                        Latitude = driverActivity.CurrentLat,
                        Longitude = driverActivity.CurrentLong,
                        OTP = booking.OTP,
                        VehicleType = booking.VehicleType,
                        VehicleCategory = booking.VehicleGroup
                    });
                else
                    return Ok(new
                    {
                        IsConfirm = false,
                        DriverId = "",
                        VehicleNo = "",
                        DriverName = "",
                        DriverImage = "",
                        MobileNo = "",
                        Latitude = 0,
                        Longitude = 0,
                        OTP = "",
                        VehicleType = 0,
                        VehicleCategory = 0
                    });
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpGet]
        [Route("cargoTypeList")]
        public IHttpActionResult GetCargoTypeList()
        {
            try
            {
                var typelist = new LookUpBO().GetCargoTypeList();
                if (typelist != null)
                    return Ok(typelist);
                else
                    return Ok(new List<CargoTypeList>());

            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        [HttpPost]
        [Route("tripEstimate")]
        [ApiAuthFilter]
        public IHttpActionResult CustomerTripEstimate(TripEstimate tripEstimate)
        {
            try
            {
                decimal distance = GetTravelTimeBetweenTwoLocations(tripEstimate.frmLatLong, tripEstimate.toLatLong).distance;
                decimal duration = GetTravelTimeBetweenTwoLocations(tripEstimate.frmLatLong, tripEstimate.toLatLong).duration;
                var tripEstimateForCustomer = new CustomerBO().GetTripEstimateForCustomer
                    (tripEstimate.VehicleType, tripEstimate.VehicleGroup, distance, tripEstimate.LdUdCharges, duration);
                if (tripEstimateForCustomer != null)
                {
                    return Ok(new { tripEstimateForCustomer });
                }
                else
                    return Ok(new List<TripEstimateForCustomer>());
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        [HttpGet]
        [Route("rateCardList")]
        public IHttpActionResult GetRateCardList()
        {
            try
            {
                var rateCardList = new RateCardBO().GetList();

                if (rateCardList != null)
                    return Ok(rateCardList);
                else
                    return Ok(new List<RateCard>());
            }
            catch (Exception ex)
            {
                return Ok(new List<RateCard>());
            }
        }
        [HttpGet]
        [Route("rateCard/{VehicleType}/{VehicleCategory}")]
        public IHttpActionResult RateCardValues(int vehicleType, int vehicleCategory)
        {
            try
            {
                var rateCardList = new RateCardBO().GetList().Where(x => x.VehicleType == vehicleType && x.Category == vehicleCategory);

                if (rateCardList != null)
                    return Ok(rateCardList);
                else
                    return Ok(new List<RateCard>());
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }


        [HttpGet]
        [Route("avgDriverRating/{driverId}")]
        [ApiAuthFilter]
        public IHttpActionResult GetDriverRatingDetails(string DriverID)
        {
            try {
            var driverRating = new DriverBO().GetDriverRating(new DriverRating { DriverId = DriverID });
            if (driverRating != null)
                return Ok(driverRating);
            else
                return Ok(new DriverRating());
            }catch(Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        [HttpPost]
        [Route("cancelBooking")]
        [ApiAuthFilter]
        public IHttpActionResult DeleteByBookingNo(DeleteBookingDTO deleteBookingDTO)
        {
            var result = false;
            try
            {
                result = new BookingBO().DeleteBooking(new Booking { BookingNo = deleteBookingDTO.BookingNo,Remarks=deleteBookingDTO.Remarks });
                if (result)
                {
                    string GetDriverDeviceIDByBookingNo = new BookingBO().GetDriverDeviceIDByBookingNo(deleteBookingDTO.BookingNo);
                    if (!string.IsNullOrWhiteSpace(GetDriverDeviceIDByBookingNo))
                    {
                        PushNotification(GetDriverDeviceIDByBookingNo, deleteBookingDTO.BookingNo, UTILITY.NotifyBookingCancelledByUser);
                        return Ok(UTILITY.SUCCESS);
                    }
                    else
                        return Ok(UTILITY.FAIL);
                }
                else
                    return Ok(result);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        [HttpGet]
        [Route("drivergeOposition/{driverId}")]
        [ApiAuthFilter]
        public IHttpActionResult GetDriverGeoPosition(string driverID)
        {
            try
            {
                var driverActivity = new DriverActivityBO().GetDriverActivityByDriverID(new DriverActivity { DriverId = driverID });
                if (driverActivity != null)
                {
                    return Ok(new
                    {
                        CurrentLat = driverActivity.CurrentLat,
                        CurrentLong = driverActivity.CurrentLong
                    });
                }
                else
                {
                    return Ok(new
                    {
                        CurrentLat = 0.0,
                        CurrentLong = 0.0
                    });
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        /* vijay: only by customer */
        [HttpGet]
        [Route("pay/{bookingNo}/{driverId}/{PaymentType}")]
        [ApiAuthFilter]
        public IHttpActionResult CustomerPaymentProcessed(string BookingNo, string Driverid,int PaymentType)
        {
            try {
                var result = new BookingBO().CustomerPaymentTypeUpdate(BookingNo, PaymentType);
                /* 2/27/2018 */
                new BookingBO().CustomerPaymentUpdate(BookingNo);
                if (result)
                {
                    var driver = new DriverBO().GetDriver(new Driver { DriverId = Driverid });

                    if (driver != null)
                    {
                        PushNotification(driver.DeviceId, BookingNo, UTILITY.NotifyPaymentDriver);

                        var bookingDetails = new BookingBO().GetBooking(new Booking { BookingNo = BookingNo });
                        if (bookingDetails.CustomerId != bookingDetails.ReceiverMobileNo)
                        {
                            SendDriverDetailsToCustomer(bookingDetails.ReceiverMobileNo, string.Format(UTILITY.SmsNotifyPaymentCompleted, bookingDetails.BookingNo, bookingDetails.InvoiceAmount));
                        }
                        SendDriverDetailsToCustomer(bookingDetails.CustomerId, string.Format(UTILITY.SmsNotifyPaymentCompleted, bookingDetails.BookingNo, bookingDetails.InvoiceAmount));

                        return Ok(UTILITY.SUCCESS);
                    }
                    else
                    {
                        return Ok(UTILITY.FAIL);
                    }
                }
                else
                {
                    return Ok(UTILITY.FAIL);
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        [HttpGet]
        [Route("billDetails/{bookingNo}")]
        [ApiAuthFilter]
        public IHttpActionResult CustomerPaymentDetails(string bookingNo)
        {
            try {
            var customer = new CustomerBO().GetCustomerPaymentDetails(bookingNo);
            if (customer != null)
            {
                return Ok(customer);
            }
            else
                return Ok(new List<CustomerBillDetails>());
        }
            catch(Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        [HttpGet]
        [Route("tripInvoice/{bookingNo}")]
        [ApiAuthFilter]
        public IHttpActionResult TripInvoice(string BookingNo)
        {
            try
            {
                TripInvoice tripInvoice = new CustomerBO().GetTripInvoiceList(new TripInvoice { BookingNo = BookingNo });
                if (tripInvoice != null)
                    return Ok(tripInvoice);
                else
                    return Ok(new TripInvoice());
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        [HttpGet]
        [Route("sendInvoiceMail/{bookingNo}/{emailId}/{Ismail}")]
        //[ApiAuthFilter]
        public IHttpActionResult SendInvoiceMail(string BookingNo, string EmailId, bool Ismail = true)
        {
            try
            {
                TripInvoice tripInvoiceList = new CustomerBO().GetTripInvoiceList(new TripInvoice
                {
                    BookingNo = BookingNo
                });
                if (Ismail)
                {
                    DriverTripInvoice driverTripInvoice = new DriverBO().GetDriverTripInvoice(new DriverTripInvoice
                    {
                        BookingNo = BookingNo
                    });
                    CompanyTripInvoice companyTripInvoice = new CustomerBO().GetCompanyTripInvoiceList(new CompanyTripInvoice
                    {
                        BookingNo = BookingNo
                    });

                    InvoiceDTO invoiceDTO = new InvoiceDTO();
                    invoiceDTO.CompanyTripInvoice = companyTripInvoice;
                    invoiceDTO.DriverTripInvoice = driverTripInvoice;
                    invoiceDTO.TripInvoice = tripInvoiceList;

                    byte[] pdf; // result will be here

                    var cssText = File.ReadAllText(HttpContext.Current.Server.MapPath("~/Areas/Master/Views/Driver/bootstrap.css"));
                    var html = this.RenderView<InvoiceDTO>("~/Areas/Master/Views/Driver/Invoices.cshtml", invoiceDTO);
                    using (var memoryStream = new MemoryStream())
                    {
                        var document = new Document(PageSize.A4, 25f, 25f, 25f, 25f);
                        var writer = PdfWriter.GetInstance(document, memoryStream);
                        document.Open();

                        using (var cssMemoryStream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(cssText)))
                        {
                            using (var htmlMemoryStream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(html)))
                            {
                                XMLWorkerHelper.GetInstance().ParseXHtml(writer, document, htmlMemoryStream, cssMemoryStream);
                            }
                        }
                        document.Close();
                        //new System.Threading.Thread(x => {
                        //    new EmailGenerator().ConfigMail(EmailId, true, "PickC Invoice", "<div>PickC Invoice</div>", pdf, BookingNo);
                        //}).Start();
                        pdf = memoryStream.ToArray();
                        var templatePath = System.Web.Hosting.HostingEnvironment.MapPath("~/InvoiceContent.txt");
                        string strText = "";
                        using (StreamReader sr = new StreamReader(templatePath))
                        {
                            while (sr.Peek() >= 0)
                            {
                                strText = sr.ReadToEnd();
                            }
                        }
                        string mailBody = string.Format(strText,invoiceDTO.TripInvoice.CustomerName,DateTime.Now.ToString("dd/MM/yyyy"));
                        bool sendCustomerMail = new EmailGenerator().ConfigMail(EmailId, true, "PickC Invoice", mailBody, pdf, BookingNo);
                        if (sendCustomerMail)
                            return Ok(UTILITY.SUCCESS);
                        else
                            return Ok(UTILITY.FAIL);
                    }
                }
                else
                {
                    return Ok(tripInvoiceList);
                }
            }
            catch (Exception ex)
            {
                //HttpRequestMessage httpMsg = new HttpRequestMessage();
                //httpMsg.Content = new StringContent("Some Error Content");
                //var logger = new NLogger();
                //TraceRecord record = new TraceRecord(httpMsg, "some category", TraceLevel.Error);
                
                return InternalServerError(ex);
            }
        }
        [HttpPost]
        [Route("sendMessageToPickC")]
        //[ApiAuthFilter]

        public IHttpActionResult SendMessageToPickC(ContactUs contactUs)
        {
            bool result = false;
            try
            {
                contactUs.CreatedBy = UTILITY.DEFAULTUSER;
                result = new CustomerBO().SaveCustomer(contactUs);
                string fromMail = string.Empty;
                if (result == true)
                {
                    if (contactUs.Type == "CustomerSupport" || contactUs.Type == "FeedBack")
                        fromMail = "support@pickcargo.in";
                    else if (contactUs.Type == "ContactUs" || contactUs.Type == "Careers")
                        fromMail = "info@pickcargo.in";
                    bool sendMail = new EmailGenerator().ConfigMail(fromMail, true, contactUs.Subject, contactUs.Message);

                    if (sendMail)
                        return Ok(UTILITY.SUCCESS);
                    else
                        return Ok(UTILITY.FAIL);
                }
                else
                    return Ok(result);

            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPost]
        [Route("driverRating")]
        [ApiAuthFilter]
        public IHttpActionResult DriverRatingDetails(DriverRating driverRating)
        {
            try {
            var DriverRating = new DriverBO().SaveDriverRating(driverRating);
            if (DriverRating)
            {
                return Ok(UTILITY.SUCCESS);
            }
            else
                return Ok(UTILITY.FAIL);
            }catch(Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpGet]
        [Route("vehicleTypeList")]
        public IHttpActionResult GetVehicleTypeList()
        {
            try
            {
                var typelist = new LookUpBO().GetVehicleTypeList();
                if (typelist != null)
                    return Ok(typelist);
                else
                    return Ok(new List<LookUp>());
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPost]
        [Route("deviceId")]
        [ApiAuthFilter]
        public IHttpActionResult UpdateDeviceId(CustomerDeviceDTO customerDeviceDTO)
        {
            try
            {
                var result = new CustomerBO().UpdateCustomerDevice(customerDeviceDTO.MobileNo, customerDeviceDTO.DeviceId);
                if (result)
                    return Ok(UTILITY.SUCCESS);
                else
                    return Ok(UTILITY.FAIL);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpGet]
        [Route("logout")]
        [ApiAuthFilter]
        public IHttpActionResult Logout()
        {
            try
            {
                IEnumerable<string> headerValues;
                var token = string.Empty;
                if (Request.Headers.TryGetValues("AUTH_TOKEN", out headerValues))
                {
                    token = headerValues.FirstOrDefault();
                }
                var result = new CustomerLogInBO()
                    .DeleteCustomerLogIn(new CustomerLogin { TokenNo = token });

                if (result)
                    return Ok(UTILITY.SUCCESS);
                else
                    return Ok(UTILITY.FAIL);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        [HttpGet]
        [Route("vehicleGroupList")]
        public IHttpActionResult GetVehicleGroupList()
        {
            try
            {
                var vehiclegrouplist = new LookUpBO().GetVehicleGroupList();
                if (vehiclegrouplist != null)
                    return Ok(vehiclegrouplist);
                else
                    return Ok(new List<LookUp>());
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpGet]
        [Route("customerPaymentsIsPaidCheck")]
        [ApiAuthFilter]
        public IHttpActionResult GetCustomerPaymentsIsPaidCheck()
        {
            try
            {
                var CustomerMobNo = HttpContext.Current.Request.Headers["MOBILENO"];
                var result = new CustomerBO().GetCustomerPaymentsCheck(CustomerMobNo);
                if (!(string.IsNullOrWhiteSpace(result)))
                {
                    return Ok(new { Status = UTILITY.FAIL, BookingNo = result });
                }
                else
                    return Ok(new { Status = UTILITY.SUCCESS, BookingNo = "" });
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        [HttpGet]
        [Route("booking/{bookingNo}")]
        [ApiAuthFilter]
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
                    return Ok(new Booking());
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        [HttpPost]
        [Route("getRSAKey")]
        public IHttpActionResult getRSAKey(RSAObject obj)
        {
            try {
            var CCAVENUE_ACCESS_CODE = ConfigurationManager.AppSettings["CCAVENUE_ACCESS_CODE"];
            if (CCAVENUE_ACCESS_CODE == obj.Access_code)
            {
                string vParams = "access_code=" + obj.Access_code + "&" + "order_id=" + obj.Order_id;
                string queryUrl = "https://secure.ccavenue.com/transaction/getRSAKey";
                var encStr = postPaymentRequestToGateway(queryUrl, vParams);
                if (encStr != null && encStr != "")
                {
                    return Ok(new { RSAKey = encStr, Status = UTILITY.SUCCESS });
                }
                else
                {
                    return Ok(new { RSAKey = "", Status = UTILITY.FAIL });
                }
            }
            else
            {
                return Ok(new { RSAKey = "", Status = UTILITY.FAIL });
            }
            }catch(Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        #region apart from customer app
        private string postPaymentRequestToGateway(String queryUrl, String urlParam)
        {
            String message = "";
            try
            {
                StreamWriter myWriter = null;// it will open a http connection with provided url
                WebRequest objRequest = WebRequest.Create(queryUrl);//send data using objxmlhttp object
                objRequest.Method = "POST";
                //objRequest.ContentLength = TranRequest.Length;
                objRequest.ContentType = "application/x-www-form-urlencoded";//to set content type
                myWriter = new System.IO.StreamWriter(objRequest.GetRequestStream());
                myWriter.Write(urlParam);//send data
                myWriter.Close();//closed the myWriter object

                // Getting Response
                System.Net.HttpWebResponse objResponse = (System.Net.HttpWebResponse)objRequest.GetResponse();//receive the responce from objxmlhttp object 
                using (System.IO.StreamReader sr = new System.IO.StreamReader(objResponse.GetResponseStream()))
                {
                    message = sr.ReadToEnd();
                }
            }
            catch (Exception exception)
            {
                Console.Write("Exception occured while connection." + exception);
            }
            return message;
        }





        [HttpGet]
        [Route("driverMonitorInCustomer/{driverId}")]
        [ApiAuthFilter]
        public IHttpActionResult DriverMonitorInCustomer(string DriverID)
        {
            try
            {
                
                var driverMonitorList = new DriverActivityBO().GetDriverMonitorInCustomer(new DriverMonitorInCustomer { DriverId = DriverID });
                if (driverMonitorList != null)
                {
                    var driverActivity = new DriverActivityBO().GetDriverActivityByDriverID(new DriverActivity { DriverId = DriverID });
                    var latlongs = new DriverActivityBO().GetFiveLatLongsforDriver(DriverID, driverActivity.TokenNo).TrimEnd('|');
                    var accuracyResult = GetLatLongsValues(latlongs);
                    return Ok(new {
                        driverMonitorList = driverMonitorList,
                        accuracy = accuracyResult
                    });
                }
                else
                    return Ok(new DriverMonitorInCustomer());

                
                
            }
            catch (Exception ex)
            {
                return Ok(ex.Message + ":" + ex.InnerException + ":" + ex.StackTrace);
                //return InternalServerError(ex);
            }
        }



        [HttpPost]
        [Route("saveImageRegister")]
        public IHttpActionResult SaveImageRegister(DriverImageRegister driverImageRegister)
        {
            try
            {
                var result = new CustomerBO().SaveImageDriverDetails(driverImageRegister);
                if (result)
                {
                    return Ok("Success");
                }
                else
                    return Ok(UTILITY.FAILURESTATUS);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpDelete]
        [Route("{mobile}")]
        [ApiAuthFilter]
        public IHttpActionResult DeleteCustomer(string mobile)
        {
            try
            {
                var result = new CustomerBO().DeleteCustomer(new Customer { MobileNo = mobile });
                if (result)
                    return Ok(UTILITY.SUCCESS);
                else
                    return Ok(UTILITY.FAIL);
            }
            catch (Exception ex)
            {
                return Ok(UTILITY.FAIL);
            }
        }


        [HttpGet]
        [Route("testTripInvoice")]
        public IHttpActionResult TestInvoice()
        {
            try
            {
                byte[] pdf; // result will be here

                var cssText = File.ReadAllText(HttpContext.Current.Server.MapPath("~/Areas/Master/Views/Driver/bootstrap.css"));
                var html = File.ReadAllText(HttpContext.Current.Server.MapPath("~/Areas/Master/Views/Driver/driverinvoice.html"));
                using (var memoryStream = new MemoryStream())
                {
                    var document = new Document(PageSize.A4, 25f, 25f, 25f, 25f);
                    var writer = PdfWriter.GetInstance(document, memoryStream);
                    document.Open();

                    using (var cssMemoryStream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(cssText)))
                    {
                        using (var htmlMemoryStream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(html)))
                        {
                            XMLWorkerHelper.GetInstance().ParseXHtml(writer, document, htmlMemoryStream, cssMemoryStream);
                        }
                    }
                    document.Close();

                    pdf = memoryStream.ToArray();
                    FileStream fs = new FileStream(@"Downloads\driverinvoice.pdf", FileMode.OpenOrCreate);
                    fs.Write(pdf, 0, pdf.Length);
                    fs.Close();
                }
                return Ok();
            }
            catch (Exception ex)
            {
                return Ok();
            }
        }
        private string RenderView<T>(string path, T model)
        {
            var controller = CreateController<PickCApi.Areas.Master.Controllers.EmailController>();
            RouteData route = new RouteData();
            System.Web.Mvc.ControllerContext newContext = new System.Web.Mvc.ControllerContext(new HttpContextWrapper(System.Web.HttpContext.Current), route, controller);
            newContext.RouteData.Values.Add("controller", "Fault");
            var html = RenderViewToString(newContext, path, model, true);

            return html;
        }

        private byte[] GetPDF2(string pHTML)
        {
            byte[] result;
            try
            {
                MemoryStream memoryStream = new MemoryStream();
                TextReader reader = new StringReader(pHTML);
                Document expr_2B = new Document(PageSize.A4, 25f, 25f, 25f, 25f);
                PdfWriter.GetInstance(expr_2B, memoryStream);
                HTMLWorker hTMLWorker = new HTMLWorker(expr_2B);
                expr_2B.Open();
                hTMLWorker.StartDocument();
                hTMLWorker.Parse(reader);
                hTMLWorker.EndDocument();
                hTMLWorker.Close();
                expr_2B.Close();
                result = memoryStream.ToArray();
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }

        [HttpGet]
        [Route("GetCustomerList")]
        public IHttpActionResult GetCustomersDetails()
        {
            try
            {
                var result = new CustomerBO().GetCustomersDetails();
                if (result != null)
                    return Ok(result);
                else
                    return Ok(new CustomerInfo());
            }
            catch (Exception ex)
            {
                return Ok(new CustomerInfo());
            }
        }

        /* only for customer */
        [HttpGet]
        [Route("isReachPickupWaiting")]
      //  [ApiAuthFilter]
        public IHttpActionResult IsCustomerWaitingPickup()
        {
            try
            {
                var bookingNo = new BookingBO().CustomerIsReachPickupPending(HeaderValueByKey("MOBILENO"));
                if(string.IsNullOrWhiteSpace(bookingNo))
                {
                    var customerBookingNo = new BookingBO().CustomerAfterReachPickupTripStartPending(HeaderValueByKey("MOBILENO"));
                    if(!string.IsNullOrWhiteSpace(customerBookingNo))
                    {
                        return Ok(new { BookingNo = customerBookingNo, Message = UTILITY.DRIVERPICKUPREACHEDTRIPNOTSTARTED });
                    }
                    else
                    {
                        return Ok(new { BookingNo = "", Message = UTILITY.NOACTIONPERFOMED });
                    }
                }
                else
                {
                    return Ok(new { BookingNo = bookingNo, Message = UTILITY.DRIVERISREACHPICKUPPENDING });
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        #endregion

    }
}


