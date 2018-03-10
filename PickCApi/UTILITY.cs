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

        public static string NOTIFYTORECEIVERTRIPSTART = "trip has started and the vehicle is on the way to delivery location.";
        public static string NOTIFYTORECEIVERTRIPEND = "Trip completed successfully. Please proceed for payment options";


        public static string NotifyCustomer = "Booking Cancelled by System";
        public static string NotifyCustomerFail = "Booking Cancelled by System is Failed";
        public static string NotifySuccess = "Booking Confirmed";//done
        public static string NotifyFailed = "Booking Failed";
        public static string NotifyCancelledByDriver = "Booking Cancelled by driver";//done
        public static string NotifyDriverpaymentReceived = "DriverpaymentReceived";
        public static string NotifyCustomerPickupStart = "Driver is about to reach pickup location";
        public static string NotifyTripStart = "Trip Started";//pending
        public static string NotifyTripEnd = "Trip End";//pending
        public static string NotifyInvoiceGenerated = "Invoice Generated";//pending
        public static string NotifyNewBooking = "New Booking Available";
        public static string NotifyBookingCancelledByUser = "Booking Cancelled by user";//done
        public static string NotifyPaymentDriver = "Customer Payment Received";//done
        public static string NotifyBookingAcceptedByOtherDriver = "Booking Accepted by other driver";
        public static string NotifyPickUpReachDateTime = "Driver has reached pick up location";
        public static string NotifyDestinationReachDateTime = "Driver reached delivery location";

        public static short radius = 4;//2
        public static string SmsOTP = "OTP for PICKC: {0}";
        public static string SmsConfirmTrip = "Your booking has been confirmed. Your trip will be started soon...PICKC";
        public static string SmsConfirmBooking = "Your booking has been confirmed. Your booking no. is {0}...PICKC";
        public static string SmsDestinationReachedDateTime = "Your Pick-C truck: {0} has reached delivery location.";
        public static string SmsNotifyTripEndToCustomer = "Your Pick-C trip completed successfully. Please proceed for payment options of Rupees {0}.";
        public static string SmsNotifyTripEndToReceiver = "Your Pick-C trip completed successfully. Please proceed for payment options (Cash / PayTM) of Rupees {0}.";
        public static string SmsNotifyTripStart = "Pick-C booking no: {0} ,trip has started and truck is on the way to delivery location. Driver: {1}, Mobile: {2}, Truck No:{3}";
        public static string SmsNotifyPickUpReachDateTime = "Your Pick-C truck: {0} has reached pickup location.";
        public static string SmsNotifyPaymentCompleted = "Your Pick-C booking no: {0} of Rupees {1} is done successfully.";
        public static string SmsSignUpOTP = "Your Pick-C SIGN-UP OTP is: {0}";
        public static string SmsBookingAcceptOTP = "Your Pick-C OTP is: {0}, please share this with your Pick-C driver, to start the trip.";



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
        public static string DRIVERISCONFIRMPICKUPREACHEDPENDING = "Driver Is Confirmed Pickup Reach Is Pending";
        public static string DRIVERISCONFIRMPICKUPREACHEDTRIPSTARTPENDING = "Driver Is Confirmed Pickup Reached Trip Start Is Pending";
        public static string DESTINATIONREACHEDTRIPENDPENDING = "DESTINATION REACHED TRIP END PENDING";
        public static string TRIPSTARTEDTRIPENDPENDING = "TRIP STARTED TRIP END PENDING";
        public static string UPDATESUCCESSMSG = "UPDATED  SUCCESSFULLY";
        public static string UPDATEFAILUREMSG = "UPDATION FAILED";
        public static string LOGIN = "USER LOGGEDIN SUCCESSFULLY";
        public static string SUCCESSTATUS = "True";
        public static string FAILURESTATUS = "False";
        public static bool  FAIL = false;
        public static bool SUCCESS = true;
        public static string BOOKINGSUCCESS = "Booked Successfully";
        public const string DRIVERISREACHPICKUPPENDING = "Driver Is Reach Pickup Pending";
        public const string DRIVERPICKUPREACHEDTRIPNOTSTARTED = "Driver Pickup Reached  Trip Not Started";
        public const string NOACTIONPERFOMED = "No Action Perfomed";



    }


}