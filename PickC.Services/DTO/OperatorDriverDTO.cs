using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Master.Contract;

namespace PickC.Services.DTO
{
public class OperatorDriverDTO
    {
        public IEnumerable<OperatorDriver> operatorDriver { get; set; }
        public IEnumerable<OperatorVehuicleAttachedNo> operatorVehuicleAttachedNo { get; set; }
        public OperatorDriverList operatorDriverList { get; set; }
    }
}
