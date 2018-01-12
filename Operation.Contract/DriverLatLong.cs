using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Operation.Contract
{
   public class DriverLatLong:IContract
    {
        public DriverLatLong()
        {

        }
        public RootObject rootObject { get; set; }
    }
    public class Location
    {
        public double latitude { get; set; }
        public double longitude { get; set; }
    }

    public class SnappedPoint
    {
        public Location location { get; set; }
        public int originalIndex { get; set; }
        public string placeId { get; set; }
    }

    public class RootObject
    {
        public List<SnappedPoint> snappedPoints { get; set; }
    }
}
