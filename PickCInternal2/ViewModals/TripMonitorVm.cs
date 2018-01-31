using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Master.Contract;

namespace PickC.Internal2.ViewModals
{
	public class TripMonitorVm
	{
		public Address address { get; set; }
		public string title { get; set; }

		//added by kiran

		public string BookingNo { get; set; }
		public string DriverName { get; set; }
		public string LocationFrom { get; set; }
		public string LocationTo { get; set; }
		public string VehicleNo { get; set; }
		public string VehicleType { get; set; }
		public string VehicleCategory { get; set; }
		public string ETA { get; set; }
		public string TripStartTime { get; set; }
		public string ArrivalTime { get; set; }
		public string WaitingTime { get; set; }
		public int Status { get; set; }

		//added by kiran
	}

	public class Address
	{
		public string address { get; set; }
		public string lat { get; set; }
		public string lng { get; set; }
	}


	public class DriverMonitorVm
	{
		public List<DriverAttachmentListStatus> driverList { get; set; }
		public List<TripMonitorVm> tripMonitorVmList { get; set; }
	}
	//public class DriverAttachmentListStatus
	//{
	//    public string DriverId{ get; set; }
	//    public string DriverName{ get; set; }
	//    public string MobileNo{ get; set; }
	//    public string VehicleNo { get; set; }
	//    public string Status { get; set; }
	//    //public string Status { get; set; }

	//}
}