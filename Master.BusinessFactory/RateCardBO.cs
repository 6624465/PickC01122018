using System;
using System.Collections.Generic;
using Master.Contract;
using Master.DataFactory;

namespace Master.BusinessFactory
{
    public class RateCardBO
    {
        private RateCardDAL ratecardDAL;
        public RateCardBO()
        {
            ratecardDAL = new RateCardDAL();
        }

        public List<RateCard> GetList()
        {
            return ratecardDAL.GetList();
        }

        public bool SaveRateCard(RateCard newItem)
        {

            return ratecardDAL.Save(newItem);
        }

        public bool DeleteRateCard(RateCard item)
        {
            return ratecardDAL.Delete(item);
        }

        public RateCard GetRateCard(RateCard item)
        {
            return (RateCard)ratecardDAL.GetItem<RateCard>(item);
        }

    }
}
