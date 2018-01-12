using System;
using System.Collections.Generic;
using Master.Contract;
using Master.DataFactory;
using PickC.Services.DTO;

namespace Master.BusinessFactory
{
    public class DriverBO
    {
        private DriverDAL driverDAL;
        public DriverBO()
        {
            driverDAL = new DriverDAL();
        }
        public List<Driver> GetList()
        {
            return driverDAL.GetList();
        }
        public List<DriverDetails> GetDriversDetailList()
        {
            return driverDAL.GetDriversDetailList();
        }
        public int GetTripCount(string MobileNo)
        {
            return driverDAL.GetTripCount(MobileNo);
        }
        public int GetTripCountbyDriverID(string DriverID,DateTime FromDate,DateTime ToDate)
        {
            return driverDAL.GetTripCountbyDriverID(DriverID,FromDate,ToDate);
        }
        public decimal GetTripAmount(string MobileNo)
        {
            return driverDAL.GetTripAmount(MobileNo);
        }
        public decimal GetTripAmountbyDriverID(string DriverID,DateTime FromDate, DateTime ToDate)
        {
            return driverDAL.GetTripAmountbyDriverID(DriverID,FromDate,ToDate);
        }
        public bool SaveDriver(Driver newItem)
        {

            return driverDAL.Save(newItem);
        }
        public bool SaveDriverReferral(DriverReferral newItem)
        {

            return driverDAL.SaveDriverReferral(newItem);
        }

        public bool DeleteDriver(Driver item)
        {
            return driverDAL.Delete(item);
        }
        public List<TripCE> GetTripCountEarnings(string MobileNo,DateTime fromdate, DateTime todate)
        {
            return driverDAL.GetTripCountandEarnings(MobileNo,fromdate,todate);
        }
        public List<DriverTodayTripList> GetTodayListOfTrips(string DriverID, DateTime fromdate, DateTime todate)
        {
            return driverDAL.GetTodayListOfTrips(DriverID,fromdate,todate);
        }
        public List<TripCElist> GetTripCountEarningsList(string MobileNo, DateTime fromdate, DateTime todate)
        {
            return driverDAL.GetTripCountandEarningsList(MobileNo, fromdate, todate);
        }
        public List<TripCElist> GetTripEarningsList(string MobileNo)
        {
            return driverDAL.GetDailyTripEarningsList(MobileNo);
        }
        public List<TripCountList> GetTripCountsList(string MobileNo)
        {
            return driverDAL.GetDailyTripCountList(MobileNo);
        }
        public Driver GetDriver(Driver item)
        {
            return (Driver)driverDAL.GetItem<Driver>(item);
        }
        public List<DriverEarningPaymentType> GetDriverTripAmountbyPaymentType(string DriverID,DateTime FromDate,DateTime ToDate)
        {
            return driverDAL.GetDriverTripAmountbyPaymentType(DriverID,FromDate,ToDate);
        }
        public List<DriverWiseDailyAmountPaymentTypeList> DriverTripAmountbyPaymentTypeListOfTrips(string DriverID, DateTime FromDate, DateTime ToDate, int PaymentType)
        {
            return driverDAL.DriverTripAmountbyPaymentTypeListOfTrips(DriverID, FromDate, ToDate, PaymentType);
        }
        public DriverRating GetDriverRating(DriverRating item)
        {
            return (DriverRating)driverDAL.GetDriverRating<DriverRating>(item);
        }


        public bool UpdateDriverDevice(string driverID, string deviceID)
        {
            return driverDAL.UpdateDriverDevice(driverID, deviceID);
        }

        public List<DriverAttachmentListStatus> GetDriverBySearch(string status)
        {
            return (List<DriverAttachmentListStatus>)driverDAL.GetDriverBySearch(status);
        }

        public bool SaveAttachment(DriverAttachmentsDTO attachment)
        {
            return driverDAL.SaveAttachment(attachment);
        }
        public bool UpdateDriverPassword(DriverPassword item)
        {
            return driverDAL.UpdateDriverPassword(item);
        }
        public List<Driver> OperatorWiseDriverAttachList(string MobileNo)
        {
            return driverDAL.GetOperatorWiseDriverAttachList(MobileNo);
        }
        public List<Driver> GetOperatorWiseDriversList(string MobileNo)
        {
            return driverDAL.GetOperatorWiseDriversList(MobileNo);
        }
        public bool SaveDriverRating(DriverRating driverRating)
        {
            return driverDAL.SaveDriverRating(driverRating);
        }
        public DriverTripInvoice GetDriverTripInvoice(DriverTripInvoice driverTripInvoice)
        {
            return (DriverTripInvoice)driverDAL.GetDriverTripInvoice<DriverTripInvoice>(driverTripInvoice);
        }
    }
}
