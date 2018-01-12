using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Master.Contract
{
    public class OperatorVehicle : IContract
    {
        public string OperatorVehicleID { get; set; }
        public string VehicleRegistrationNo { get; set; }
        public Int16 VehicleCategory { get; set; }
        public Int16 VehicleType { get; set; }
        public string VehicleTypeDescription { get; set; }
        public string VehicleCategoryDescription { get; set; }
        public string Model { get; set; }
        public string Tonnage { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
      
    }
    public class VehicleAttachment
    {
        public string Registration { get; set; }
        public string InsuranceDocument { get; set; }
        public string PollutionCheckDocument { get; set; }
        public string Others { get; set; }
    }
}
