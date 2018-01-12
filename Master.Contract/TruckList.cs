using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Master.Contract;

namespace Master.Contract
{
  public class TruckList : IContract
    {
        public TruckList()
        {

        }
        public string Maker { get; set; }

        public string VehicleNo { get; set; }

        public string VehicleType { get; set; }
    }
}
