using Master.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PickC.Services.DTO
{
   public class BookingDTO
    {
        public DateTime? BookingDate { get; set; }
        public string BookingNo { get; set; }
        public string CustomerNo { get; set; }
        public int VehicleGroup { get; set; }
        public List<LookUp> VehicleGroupList { get; set; }
        public int VehicleType { get; set; }
        public List<LookUp> VehicleTypeList { get; set; }

        public string CustomerName { get; set; }
        public string VehicleNumber { get; set; }
    }
}
