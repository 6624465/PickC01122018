using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Master.Contract;
using PickC.Services.DTO;

namespace PickC.Internal2.ViewModals
{
    public class OperatorVm
    {
        public Operator OPerator { get; set; }
        public OperatorLookupDTO operatorLookupDTO { get; set; }
    }
}