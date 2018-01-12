using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Operation.Contract;

namespace Operation.Contract
{
	public class NearTrucksInRange:IContract
	{
        public NearTrucksInRange() { }

        
		public string DriverID { get; set; }

        public string VehicleNo { get; set; }        

        public string PhoneNo { get; set; }

        public decimal Latitude { get; set; }
        
        public decimal Longitude { get; set; }

        public Int16 VehicleType { get; set; }

        public Int16 VehicleGroup { get; set; }


    }
}




