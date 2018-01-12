using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Master.Contract;

namespace Master.Contract
{
    public class LookUp : IContract
    {
        public LookUp() { }


        public Int16 LookupId { get; set; }


        public string LookupCode { get; set; }


        public string LookupDescription { get; set; }


        public string LookupCategory { get; set; }

        public string Image { get; set; }
    }
}




