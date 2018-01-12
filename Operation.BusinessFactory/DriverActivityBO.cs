using System;
using System.Collections.Generic;
using Operation.Contract;
using Operation.DataFactory;

namespace Operation.BusinessFactory
{
    public class DriverActivityBO
    {
        private DriverActivityDAL driveractivityDAL;
        public DriverActivityBO()
        {
            driveractivityDAL = new DriverActivityDAL();
        }

        public List<DriverActivity> GetList()
        {
            return driveractivityDAL.GetList();
        }
        public DriverMonitorInCustomer GetDriverMonitorInCustomer(DriverMonitorInCustomer item)
        {
            return (DriverMonitorInCustomer)driveractivityDAL.GetDriverMonitorInCustomer<DriverMonitorInCustomer>(item);
        }

        public bool AuthenticateDriver(DriverActivity item)
        {
            return driveractivityDAL.AuthenticateDriver(item);
        }

        public bool SaveDriverActivity(DriverActivity newItem)
        {

            return driveractivityDAL.Save(newItem);
        }
        public bool UpdateCurrentDriverLocation(UpdateDriverCurrentLocation updateDriverCurrentLocation)
        {

            return driveractivityDAL.UpdateCurrentDriverLocation(updateDriverCurrentLocation);
        }
        public string GetFiveLatLongsforDriver(string DriverID, string Token)
        {
            return driveractivityDAL.GetFiveLatLongsforDriver(DriverID, Token);
        }

        public bool DeleteDriverActivity(DriverActivity item)
        {
            return driveractivityDAL.Delete(item);
        }

        public DriverActivity GetDriverActivity(DriverActivity item)
        {
            return (DriverActivity)driveractivityDAL.GetItem<DriverActivity>(item);
        }

        public DriverActivity GetDriverActivityByDriverID(DriverActivity item)
        {
            return (DriverActivity)driveractivityDAL.GetDriverActivityByDriverID<DriverActivity>(item);
        }

        public bool DriverActivityUpdate(DriverActivity item)
        {
            return driveractivityDAL.DriverActivityUpdate(item);
        }

        public string DriverLogIn(string driverID, string password, string latitude, string longitude)
        {
            return driveractivityDAL.DriverLogIn(driverID, password, latitude, longitude);
        }
    }
}
