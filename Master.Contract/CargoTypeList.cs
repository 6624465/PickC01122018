using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Master.Contract
{
   public class CargoTypeList :IContract
    {
        public CargoTypeList() { }


        public Int16 LookupId { get; set; }


        public string LookupCode { get; set; }


        public string LookupDescription { get; set; }


        public string LookupCategory { get; set; }

        public string Image { get; set; }

    }
}
