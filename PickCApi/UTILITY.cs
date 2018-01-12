using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PickCApi
{
    public static class UTILITY
    {
        public static string SUCCESSMESSAGE = "Success"; //krishna & Srinath
        public static string FAILEDMESSAGE = "Failed"; //krishna & Srinath
        public static string SUCCESSMSG = "SAVED SUCESSFULLY";
        public static string DELETEMSG = "DELETED SUCCESSFULLY";
        public static string FAILEDAUTH = "MOBILENO OR AUTH TOKEN IS MISSING";
        public static string INVALID = "INVALID MOBILENO OR AUTH TOKEN";
        public static string LOGOUT = "USER LOGGEDOUT SUCCESSFULLY";
        public static string DEFAULTUSER = "ADMIN";
        public static string FAILEDMSG = "Operation failed";

        public static string NotifyCustomer = "Booking Cancelled by System";
        public static string NotifyCustomerFail = "Booking Cancelled by System is Failed";
        public static string NotifySuccess = "Booking Confirmed";//done
        public static string NotifyFailed = "Booking Failed";
        public static string NotifyCancelledByDriver = "Booking Cancelled by driver";//done
        public static string NotifyDriverpaymentReceived = "DriverpaymentReceived";
        public static string NotifyCustomerPickupStart = "Driver is Started to reach pickup location";
        public static string NotifyTripStart = "Trip Started";//pending
        public static string NotifyTripEnd = "Trip End";//pending
        public static string NotifyInvoiceGenerated = "Invoice Generated";//pending
        public static string NotifyNewBooking = "New Booking Available";
        public static string NotifyBookingCancelledByUser = "Booking Cancelled by user";//done
        public static string NotifyPaymentDriver = "Customer Payment Received";//done
        public static string NotifyBookingAcceptedByOtherDriver = "Booking Accepted by other driver";
        public static string NotifyPickUpReachDateTime = "Driver reached pickup location";
        public static string NotifyDestinationReachDateTime = "Driver reached destination location";

        public static short radius = 4;//2
        public static string SmsOTP = "OTP for PICKC: {0}";
        public static string SmsConfirmTrip = "Your booking has been confirmed. Your trip will be started soon...PICKC";
        public static string SmsConfirmBooking = "Your booking has been confirmed. Your booking no. is {0}...PICKC";

        public enum HEADERKEYS
        {
            AUTH_TOKEN,
            DRIVERID,
            LATITUDE,
            LONGITUDE,
            MOBILENO,
            TYPE
        }
        public static string MESSAGESENT = "Message Sent to Driver";
        public static string ACCEPTREQUEST = "Accepted request by driver";
        public static string REJECTREQUEST = "Rejected request by driver";
        public static string UPDATESUCCESSMSG = "UPDATED  SUCCESSFULLY";
        public static string UPDATEFAILUREMSG = "UPDATION FAILED";
        public static string LOGIN = "USER LOGGEDIN SUCCESSFULLY";
        public static string SUCCESSTATUS = "True";
        public static string FAILURESTATUS = "False";
        public static bool  FAIL = false;

        public static bool SUCCESS = true;
        public static string BOOKINGSUCCESS = "Booked Successfully";



    }


}