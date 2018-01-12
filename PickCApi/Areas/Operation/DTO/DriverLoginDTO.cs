using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PickCApi.Areas.Operation.DTO
{
    
    public class DriverLoginDTO
    {
        public string driverID { get; set; }
        public string password { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
    }
}