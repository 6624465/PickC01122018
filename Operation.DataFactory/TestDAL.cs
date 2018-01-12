//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Data.Common;
//using Microsoft.Practices.EnterpriseLibrary.Common;
//using Microsoft.Practices.EnterpriseLibrary.Data;
//using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
//using Operation.Contract;
//using Operation.DataFactory;

//namespace Operation.DataFactory
//{
//    public class TestDAL
//    {
//        //private Database db;
//        //private AppDB AppDb;
//        private DbTransaction currentTransaction = null;
//        private DbConnection connection = null;
//        private DbProviderFactory dbProviderFactory;
        
//        //public TestDAL()
//        //{
//        //    db = DatabaseFactory.CreateDatabase("PickC");
//        //}        

//        public List<TripMonitor> GetList()
//        {
//            using (AppDB appDb = new AppDB("PickC", dbProviderFactory))
//            {
//                return appDb.db.ExecuteSprocAccessor(DBRoutine.LISTTRIPMONITOR, MapBuilder<TripMonitor>.BuildAllProperties()).ToList();

//            }
//        }
//    }

//    public class AppDB : Database, IDisposable
//    {
//        public Database db;
//        public AppDB(string connectionString, DbProviderFactory dbProviderFactory)
//            : base(connectionString, dbProviderFactory)
//        {
//            db = DatabaseFactory.CreateDatabase("PickC");
//        }

//        protected override void DeriveParameters(DbCommand discoveryCommand)
//        {
//            throw new NotImplementedException();
//        }

        
//        private bool disposedValue = false; 

//        protected virtual void Dispose(bool disposing)
//        {
//            if (!disposedValue)
//            {
//                if (disposing)
//                {
                    
//                }

//                disposedValue = true;
//            }
//        }
        
//        public void Dispose()
//        {
//            Dispose(true);            
//            // GC.SuppressFinalize(this);
//        }
        
//    }
//}
