using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Operation.DataFactory
{
    public static class DBRoutine
    {


        /// <summary>
        /// [Operation].[Trip]
        /// </summary>

        public const string SELECTTRIP = "[Operation].[usp_TripSelect]";
        public const string  TOTALTRIPS = "[Operation].[usp_userTotaltripsList]";
        public const string LISTTRIP = "[Operation].[usp_TripList]";
        public const string SAVETRIP = "[Operation].[usp_TripSave]";
        public const string DELETETRIP = "[Operation].[usp_TripDelete]";
        public const string TRIPEND = "[Operation].[usp_TripEnd]";
        public const string CUSTOMERCURRENTTRIP = "[Operation].[usp_CustomerCurrentTrip]";
        public const string DRIVERCURRENTTRIP = "[Operation].[usp_DriverCurrentTrip]";
        public const string TRIPUPDATETRAVELLEDDISTANCE = "[Operation].[usp_UpdateTripTravelledDistance]";


       /* shruthi*/
        public const string REGISTERDBUTNOTBOOKEDCOUNT = "[Operation].[usp_CustomerLoginCount]";/*shruthi*/
        public const string REGISTEREDBUTNOTBOOKEDLIST = "[Operation].[usp_RegisteredButNotBooked]";/*shruthi*/
        public const string REGISTEREDBUTNOTBOOKEDLISTBYDATERANGE = "[Operation].[usp_RegisteredButNotBookedByDtRange]";

        /// <summary>
        /// [Operation].[Invoice]
        /// </summary>

        public const string SELECTINVOICE = "[Operation].[usp_InvoiceSelect]";
        public const string LISTINVOICE = "[Operation].[usp_InvoiceList]";
        public const string SAVEINVOICE = "[Operation].[usp_InvoiceSave]";
        public const string DELETEINVOICE = "[Operation].[usp_InvoiceDelete]";
        public const string SELECTINVOICEBYBOOKINGNO = "[Operation].[usp_InvoiceSelectByBookingNo]";




        /// <summary>
        /// [Operation].[Booking]
        /// </summary>
        /// 

        public const string BOOKINGBYBOOKINGNO = "[Operation].[usp_BookingListByBookingNo2]"; // used for current bookings map

        public const string SELECTBOOKING = "[Operation].[usp_BookingSelect]";
        public const string LISTBOOKING = "[Operation].[usp_BookingList]";
        public const string SAVEBOOKING = "[Operation].[usp_BookingSave]";
        public const string DELETEBOOKING = "[Operation].[usp_BookingDelete]";
        public const string LISTOFBOOKINGBYMOBILENO = "[Operation].[usp_BookingListByMobileNo]";
        public const string LISTOFBOOKINGBYBOOKINGNO = "[Operation].[usp_BookingListByBookingNo]";
        public const string LISTNEARBOOKINGSFORDRIVER = "[Operation].[usp_NearBookingsForDriver]";
        public const string LISTOFBOOKINGBYSTATUS = "[Master].[usp_GetCustomerByStatus]";
        public const string LISTOFCURRENTBOOKING = "[Operation].[usp_BookingList]";/*mobile*/
        public const string LISTOFCURRENTBOOKINGINTERNAL= "[Operation].[usp_BookingListInternal]";/* for internal*/

        public const string SAVEPICKUPREACHDATETIME = "[Operation].[usp_SavePickupReachDateTime]";
        public const string SAVEDESTINATIONREACHDATETIME = "[Operation].[usp_SaveDestinationReachDateTime]";
        public const string BOOKINGHISTORYLIST = "[Operation].[usp_BookingHistoryList]";
        public const string LISTOFBOOKINGHISTORYLISTBYMOBILENO = "[Operation].[usp_BookingHistoryListByMobileNo]";

        public const string CUSTOMERRECEIVEDUPDATEPAYMENTVALUES = "[Operation].[usp_UpdateCustomerPaymentValues]";
        public const string CUSTOMERPAYMENTTYPEUPDATE = "[Operation].[usp_UpdateCustomerPaymentTypeValues]";

        /// <summary>
        /// [Operation].[OperatorVehicle]
        /// </summary>

        public const string SELECTOPERATORVEHICLETYPELIST = "[Master].[usp_OperatorVehicleTypeList]";
        public const string SELECTOPERATORVEHICLECATEGORYLIST = "[Master].[usp_OperatorVehicleCategoryList]";
        public const string SELECTOPERATORVEHICLEMODELLIST = "[Master].[usp_OperatorVehicleModelList]";
        public const string SAVEOPERATORVEHICLELIST = "[Master].[usp_OperatorVehicleSaveList]";
        public const string SELECTOPERATORVEHICLETOTALLIST = "[Master].[usp_OperatorVehicleList]";
        public const string SELECTOPERATORVEHICLETOTALLISTBYID = "[Master].[usp_OperatorVehicleListByID]";



        /// <summary>
        /// [Operation].[CustomerLogin]
        /// </summary>

        public const string SELECTCUSTOMERLOGIN = "[Operation].[usp_CustomerLoginSelect]";
        public const string LISTCUSTOMERLOGIN = "[Operation].[usp_CustomerLoginList]";
        public const string SAVECUSTOMERLOGIN = "[Operation].[usp_CustomerLoginSave]";
        public const string DELETECUSTOMERLOGIN = "[Operation].[usp_CustomerLoginDelete]";
        public const string AUTHENTICATEUSER = "[Operation].[usp_AUTHENTICATE_USER]";
        public const string CUSTOMERLOGIN = "[Operation].[usp_DoCustomerLogIn]";

        public const string SELECTDRIVERMONITORINCUSTOMER = "[Operation].[usp_DriverMonitorInCustomer]";
        /// <summary>
        /// [Operation].[OperatorLogin]
        /// </summary>

        //public const string SELECTCUSTOMERLOGIN = "[Operation].[usp_CustomerLoginSelect]";
        //public const string LISTCUSTOMERLOGIN = "[Operation].[usp_CustomerLoginList]";
        //public const string SAVECUSTOMERLOGIN = "[Operation].[usp_CustomerLoginSave]";
        public const string DELETEOPERATORLOGIN = "[Operation].[usp_OperatorLoginDelete]";
        public const string OPERATORAUTHENTICATEUSER = "[Operation].[usp_OperatorAUTHENTICATE_USER]";
        public const string OPERATORLOGIN = "[Operation].[usp_DoOperatorLogIn]";



        /// <summary>
        /// [Operation].[TripMonitor]
        /// </summary>

        public const string DELETETRIPMONITOR = "[Operation].[usp_TripMonitorDelete]";
        public const string SELECTTRIPMONITOR = "[Operation].[usp_TripMonitorSelect]";
        public const string LISTTRIPMONITOR = "[Operation].[usp_TripMonitorList]";
        public const string LISTTRIPMONITOR2 = "[Operation].[usp_TripMonitorList2]";
        public const string SAVETRIPMONITOR = "[Operation].[usp_TripMonitorSave]";
        public const string OPERATORTRIPMONITOR = "[Master].[usp_OperatorDriverMonitor]";

        public const string SELECTDRIVERACTIVITY = "[Operation].[usp_DriverActivitySelect]";
        public const string LISTDRIVERACTIVITY = "[Operation].[usp_DriverActivityList]";
        public const string SAVEDRIVERACTIVITY = "[Operation].[usp_DriverActivitySave]";
        public const string DELETEDRIVERACTIVITY = "[Operation].[usp_DriverActivityDelete]";
        public const string DRIVERLOGIN = "[Operation].[usp_DoDriverLogIn]";
        public const string UPDATEDRIVERDUTYSTATUS = "[Operation].[usp_UpdateDriverDutyStatus]";
        public const string AUTHENTICATEDRIVER = "[Operation].[usp_AUTHENTICATE_DRIVER]";
        public const string SELECTDRIVERACTIVITYBYDRIVERID = "[Operation].[usp_DriverActivitySelectByDriverID]";

        public const string SELECTDRIVERACTIVITYLOG = "[Operation].[usp_DriverActivityLogSelect]";

        public const string UPDATEDRIVERCURRENTLOCATIONVALUES = "[Operation].[usp_UpdateDriverCurrentLocationValues]";

        public const string LISTNEARTRUCKSINRANGE = "[Operation].[usp_NearTrucksInRange]";
        public const string BOOKINGCANCELLEDBYDRIVER = "[Operation].[usp_BookingCancelledByDriver]";
        public const string BOOKINGREJECTEDBYDRIVER = "[Operation].[usp_BookingRejectByDriver]";


        public const string BOOKINGCONFIRMBYDRIVER = "[Operation].[usp_BookingConfirmByDriver]";
        public const string ISBOOKINGALREADYCONFIRMED = "[Operation].[usp_IsBookingAlreadyConfirmed]";
        public const string GETCUSTOMERDEVICEIDBYBOOKINGNO = "[Operation].[usp_GetCustomerDeviceIDByBookingNo]";
        public const string GETCUSTOMERDEVICEIDBYTRIPID = "[Operation].[usp_GetCustomerDeviceIDByTripID]";
        public const string GETCUSTOMERDEVICEIDBYBOOKINGNOFORPAYMENTRECEIVE = "[Operation].[usp_GetCustomerDeviceIDByBookingNoForPayementReceive]";
        public const string GETDRIVERDEVICEIDBYBOOKINGNO = "[Operation].[usp_GetDriverDeviceIDByBookingNo]";
        public const string NEARTRUCKSDEVICELIST = "[Operation].[usp_NearTrucksDeviceList]";


        public const string BOOKINGSEARCH = "[Operation].[usp_GetCurrentBookings]";
        public const string BOOKINGBYDATES = "[Operation].[usp_BookingSearchByDate]";
        public const string BOOKINGHISTORYSEARCH = "[Operation].[usp_BookingHistoryList]";
        public const string BOOKINGHISTORYSEARCH1 = "[Operation].[usp_BookingHistoryList2]";//PickC internal2

        public const string CUSTOMERCURRENTTRIPBEFORETRIPSTART = "[Operation].[usp_CustomerCurrentTripBeforeTripStart]";

        //Driver Summary

        public const string DRIVERSUMMARY = "[Operation].[usp_GetDriverSummary]";
        public const string DRIVERPAYMENTS="[Operation].[usp_GetDriverPayments]";

        /// <summary>
        /// [Operation].[Trip]
        /// </summary>
        public const string DRIVERLATESTFIVELATLONGS = "[Operation].[usp_GetDriverFiveLatLong]";
        public const string TOTALAMOUNTRECEIVEDFROMCUSTOMER = "[Operation].[usp_TotalAmountReceivedFromCustomer]"; /*shruthi*/
        public const string PICKCCOMMISSION = "[Operation].[usp_PickCCommission]";/*shruthi*/
        public const string DRIVERCOMMISSION= "[Operation].[usp_DriverCommission]";/*shruthi*/
        public const string BOOKINGCANCELLEDBYCUSTOMER = "[operation].[usp_getListCancelledByCustomer]";/*shruthi*/
        public const string BOOKINGCANCELLEDBYDRIVERLIST = "[operation].[usp_getListCancelledByDriver]";/*shruthi*/
        public const string DRIVERCOMMISIONPENDINGTOPICKC="[operation].[usp_CommisionPendingToPickc]";
        public const string DRIVERCOMMISIONPENDINGTOPICKTOTAL = "[operation].[usp_CommisionPendingToPickcAmount]";

        public const string  DASHBOARDCOUNT="[operation].[usp_dashBoardCountList]";
        public const string TOTALTRIPDETAILSDAILYBASIS = "[master].[usp_getTotalTripsDetailDailyBasis]";
        public const string PAYMENTHISTORYDAILYBASIS="[operation].[usp_paymenthistoryDailyBasis]";
        public const string GETCUSTOMERISREACHPICKUPPENDINGBOOKINGNO = "[Operation].[usp_GetCustomerIsReachPickupPendingBookingNo]";
        public const string GETCUSTOMERAFTERREACHPICKUPTRIPSTARTPENDINGBOOKINGNO = "[Operation].[usp_GetCustomerAfterReachPickupPendingBookingNo]";



        /// <summary>
        /// [Security].[RoleRights]
        /// </summary>

        public const string SELECTROLERIGHTS = "[Security].[usp_RoleRightsSelect]";
        public const string LISTROLERIGHTS = "[Security].[usp_RoleRightsList]";
        public const string SAVEROLERIGHTS = "[Security].[usp_RoleRightsSave]";
        public const string DELETEROLERIGHTS = "[Security].[usp_RoleRightsDelete]";
        public const string LISTROLERIGHTSBYROLE = "[Security].[usp_RoleRightsListByRole]";
        public const string LISTSECURABLES = "[Security].[usp_SecurablesList]";
        public const string DELETEALLRIGHTSOFROLE = "[Security].[usp_DeleteRightsOfRole]";
        public const string LISTSECURABLESBYROLE = "[Security].[usp_SecurablesListByRole]";

        /// <summary>
        /// [Security].[Roles]
        /// </summary>

        public const string SELECTROLES = "[Security].[usp_RolesSelect]";
        public const string LISTROLES = "[Security].[usp_RolesList]";
        public const string SAVEROLES = "[Security].[usp_RolesSave]";
        public const string DELETEROLES = "[Security].[usp_RolesDelete]";


        /// <summary>
        /// [Security].[Users]
        /// </summary>

        public const string SELECTUSERS = "[Security].[usp_UsersSelect]";
        public const string LISTUSERS = "[Security].[usp_UsersList]";
        public const string SAVEUSERS = "[Security].[usp_UsersSave]";
        public const string DELETEUSERS = "[Security].[usp_UsersDelete]";
        public const string USERBRANCHLIST = "[Master].[UserBranchList]";
        public const string USERBRANCHSELECTEDLIST = "[Master].[UserBranchSelectedList]";

        public const string VALIDATEUSERLOGIN = "[Security].[usp_ValidateUserLogin]";


        public const string DELETEUSERRIGHTS = "[Security].[usp_UserRightsDelete]";
        public const string LISTUSERRIGHTS = "[Security].[usp_UserRightsList]";
        public const string LISTUSERRIGHTSBYUSER = "[Security].[usp_UserRightsListByUser]";

        public const string SAVEUSERRIGHTS = "[Security].[usp_UserRightsSave]";
        public const string SELECTUSERRIGHTS = "[Security].[usp_UserRightsSelect]";

    }
}
