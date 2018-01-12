using System;
using System.Collections.Generic;
using Operation.Contract;
using Operation.DataFactory;

namespace Operation.BusinessFactory
{
    public class TripMonitorBO
    {
        private TripMonitorDAL tripmonitorDAL;
        public TripMonitorBO()
        {
            tripmonitorDAL = new TripMonitorDAL();
        }

        public List<TripMonitor> GetList()
        {
            return tripmonitorDAL.GetList();
        }

        public bool SaveTripMonitor(TripMonitor newItem)
        {

            return tripmonitorDAL.Save(newItem);
        }

        public bool DeleteTripMonitor(TripMonitor item)
        {
            return tripmonitorDAL.Delete(item);
        }

        public TripMonitor GetTripMonitor(TripMonitor item)
        {
            return (TripMonitor)tripmonitorDAL.GetItem<TripMonitor>(item);
        }

    }
}
