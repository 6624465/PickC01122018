using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Master.Contract;

namespace PickC.Services.DTO
{
    public class DriverLookupDTO
    {
        public List<LookUp> genderOptions { get; set; }
        public List<LookUp> maritalOptions { get; set; }

        public List<LookUp> attachments { get; set; }
        public List<Operator> OperatorList { get; set; }
    }
}