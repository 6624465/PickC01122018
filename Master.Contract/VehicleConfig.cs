using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Master.Contract;

namespace Master.Contract
{
	public class VehicleConfig:IContract
	{
		public VehicleConfig() { }


        public Int32 VehicleModelId { get; set; }


        public string VehicleGroupDescription { get; set; }


		public string Maker { get; set; }


		public string Model { get; set; }


		public decimal  Tonnage { get; set; }


		public Int16  VehicleGroup { get; set; }


	}
}




