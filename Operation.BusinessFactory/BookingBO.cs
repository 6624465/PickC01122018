using System;
using System.Collections.Generic;
using Operation.Contract;
using Operation.DataFactory;

namespace Operation.BusinessFactory
{
    public class BookingBO
    {
        private BookingDAL bookingDAL;
        public BookingBO()
        {
            bookingDAL = new BookingDAL();
        }

        public List<Booking> GetList()
        {
            return bookingDAL.GetList();
        }
        public List<BookingHistoryList> GetBookingHistoryList()
        {
            return bookingDAL.GetBookingHistoryList();
        }
        public List<Booking> GetCustomerBySearch(int? status)
        {
            return (List<Booking>)bookingDAL.GetCustomerBySearch(status);
        }
        public List<Booking> GetListByMobileNo(string mobileNo)
        {
            return bookingDAL.GetListByMobileNo(mobileNo);
        }
        public List<Booking> GetListByBookingNo(string BookingNo)
        {
            return bookingDAL.GetListByBookingNo(BookingNo);
        }

        public bool SaveBooking(Booking newItem)
        {

            return bookingDAL.Save(newItem);
        }

        public bool DeleteBooking(Booking item)
        {
            return bookingDAL.Delete(item);
        }

        public Booking GetBooking(Booking item)
        {
            return (Booking)bookingDAL.GetItem<Booking>(item);
        }

        public List<Booking> GetNearBookingsForDriver(Guid tokenNo, string driverID, decimal latitude, decimal longitude, short minDistance)
        {
            return bookingDAL.GetNearBookingsForDriver(tokenNo, driverID, latitude, longitude, minDistance);
        }

        public List<NearTrucksInRange> GetTrucksInRange(string CustomerID, decimal latitude, decimal longitude, short minDistance, short vehicleGroup, short vehicleType)
        {
            return bookingDAL.GetTrucksInRange(CustomerID, latitude, longitude, minDistance, vehicleGroup, vehicleType);
        }
        public bool BookingRejectedByDriver(string tokenNo, string driverID, string vehicleNo, string bookingNo, string cancelRemarks, bool istripstarted, bool IsLoadingUnloading)
        {
            return bookingDAL.BookingRejectedByDriver(tokenNo, driverID, vehicleNo, bookingNo, cancelRemarks, istripstarted, IsLoadingUnloading);
        }
        public bool BookingCancelledByDriver(string tokenNo, string driverID, string vehicleNo, string bookingNo, string cancelRemarks, bool istripstarted,bool IsLoadingUnloading)
        {
            return bookingDAL.BookingCancelledByDriver(tokenNo, driverID, vehicleNo, bookingNo, cancelRemarks, istripstarted, IsLoadingUnloading);
        }

        public bool BookingConfirmByDriver(string driverID, string tokenNo, string vehicleNo, string bookingNo,string CustomerOTP)
        {
            return bookingDAL.BookingConfirmByDriver(driverID, tokenNo, vehicleNo, bookingNo, CustomerOTP);
        }

        public string GetCustomerDeviceIDByBookingNo(string bookingNo)
        {
            return bookingDAL.GetCustomerDeviceIDByBookingNo(bookingNo);
        }

        public string GetCustomerDeviceIDByTripID(string TripID)
        {
            return bookingDAL.GetCustomerDeviceIDByTripID(TripID);
        }

        public List<Master.Contract.Driver> GetNearTrucksDeviceID(string bookingNo, short minDistance, short vehicleType, short vehicleGroup, decimal latitude, decimal longitude)
        {
            return bookingDAL.GetNearTrucksDeviceID(bookingNo, minDistance, vehicleType, vehicleGroup, latitude, longitude);
        }

        public string GetDriverDeviceIDByBookingNo(string bookingNo)
        {
            return bookingDAL.GetDriverDeviceIDByBookingNo(bookingNo);
        }

        public bool SavePickupReachDateTime(string bookingNo, DateTime PickupReachDateTime)
        {
            return bookingDAL.SavePickupReachDateTime(bookingNo, PickupReachDateTime);
        }

        public bool SaveDestinationReachDateTime(string bookingNo, DateTime DestinationReachDateTime)
        {
            return bookingDAL.SaveDestinationReachDateTime(bookingNo, DestinationReachDateTime);
        }

        public List<BookingHistoryDetails> GetBookingListByMobileNo(string MobileNo)
        {
            return bookingDAL.GetBookingListByMobileNo(MobileNo);
        }
        public bool CustomerPaymentUpdate(string bookingNo)
        {
            return bookingDAL.UpdateCustomerPaymentValues(bookingNo);
        }
        public string GetCustomerDeviceIDByBookingNoByPaymentReceiveConfirm(string bookingNo)
        {
            return bookingDAL.GetCustomerDeviceIDByBookingNoByPaymentReceiveConfirm(bookingNo);
        }
        public Booking CustomerCurrentConfirmTrip(string mobileNo)
        {
            return bookingDAL.GetCustomerCurrentConfirmTrip(mobileNo);
        }
    }
}
