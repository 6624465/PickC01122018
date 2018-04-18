using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Common;
using System.ComponentModel;
using System.Collections.ObjectModel;
using Operation.Contract;
using Operation.DataFactory;

namespace Operation.DataFactory
{
    public class UserBranchDAL
    {
        private Database db;
        private DbTransaction currentTransaction = null;
        private DbConnection connection = null;

        public UserBranchDAL()
        {
            db = DatabaseFactory.CreateDatabase("PickC");
        }

        public List<UserBranch> GetList(string UserID)
        {
            return db.ExecuteSprocAccessor(DBRoutine.USERBRANCHLIST,
                                            MapBuilder<UserBranch>
                                            .MapAllProperties()
                                            .Build(), UserID).ToList();
        }

        public List<UserBranchSelectedList> GetUserBranchSelectedList(string UserID, string CompanyCode)
        {
            return db.ExecuteSprocAccessor(DBRoutine.USERBRANCHSELECTEDLIST,
                                            MapBuilder<UserBranchSelectedList>
                                            .MapAllProperties()
                                            .Build(), UserID, CompanyCode).ToList();
        }

  

 
    }
}
