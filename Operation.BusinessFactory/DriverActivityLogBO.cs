
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Operation.DataFactory;
using Operation.Contract;

namespace Operation.BusinessFactory
{
    public class DriverActivityLogBO
    {
        private DriverActivityLogDAL driverActivityLogDAL;
        public DriverActivityLogBO()
        {
            this.driverActivityLogDAL = new DriverActivityLogDAL();
        }

        public DriverActivityLog GetItem(DriverActivityLog item)
        {
            return driverActivityLogDAL.GetItem(item);
        }
    }
}
