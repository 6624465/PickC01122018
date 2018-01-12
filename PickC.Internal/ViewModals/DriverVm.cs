using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Master.Contract;
using PickC.Services.DTO;

namespace PickC.Internal.ViewModals
{
    public class DriverVm
    {
        public Driver driver { get; set; }
        public DriverLookupDTO driverLookupDTO { get; set; }
    }
}