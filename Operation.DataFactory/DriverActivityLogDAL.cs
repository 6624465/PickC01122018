using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Operation.Contract;
using Operation.DataFactory;
using System.Data;

namespace Operation.DataFactory
{
    public class DriverActivityLogDAL
    {
        private Database db;
        private DbTransaction currentTransaction = null;
        private DbConnection connection = null;

        public DriverActivityLogDAL()
        {
            db = DatabaseFactory.CreateDatabase("PickC");
        }

        public DriverActivityLog GetItem(DriverActivityLog driverActivityLog)
        {
            var driveractivityLogItem = db.ExecuteSprocAccessor(DBRoutine.SELECTDRIVERACTIVITYLOG,
                                                    MapBuilder<DriverActivityLog>.BuildAllProperties(),
                                                    driverActivityLog.DriverID, driverActivityLog.TokenNo).FirstOrDefault();

            if (driveractivityLogItem == null) return null;

            return driveractivityLogItem;
        }
    }
}
