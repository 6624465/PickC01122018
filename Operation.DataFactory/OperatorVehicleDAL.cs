using System;
using System.Collections.Generic;
using System.Linq;
using Master.Contract;
using System.Text;
using PickC.Services.DTO;
using System.Threading.Tasks;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Operation.DataFactory;

namespace Operation.DataFactory
{
   public class OperatorVehicleDAL
    {
        private Database db;
        private DbTransaction currentTransaction = null;
        private DbConnection connection = null;
        /// <summary>
        /// Constructor
        /// </summary>
        public OperatorVehicleDAL()
        {

            db = DatabaseFactory.CreateDatabase("PickC");

        }

        #region IDataFactory Members
        public List<LookUp> GetList()
        {
            return db.ExecuteSprocAccessor(DBRoutine.SELECTOPERATORVEHICLETYPELIST, MapBuilder<LookUp>.BuildAllProperties()).ToList();
        }
        public List<LookUp> GetVehicleCategoryList()
        {
            return db.ExecuteSprocAccessor(DBRoutine.SELECTOPERATORVEHICLECATEGORYLIST, MapBuilder<LookUp>.BuildAllProperties()).ToList();
        }
        public List<OperatorVehicles> GetModelList()
        {
            return db.ExecuteSprocAccessor(DBRoutine.SELECTOPERATORVEHICLEMODELLIST, MapBuilder<OperatorVehicles>.BuildAllProperties()).ToList();
        }
        public List<OperatorVehicle> GetOperatorVehicleList()
        {
            return db.ExecuteSprocAccessor(DBRoutine.SELECTOPERATORVEHICLETOTALLIST, MapBuilder<OperatorVehicle>.BuildAllProperties()).ToList();
        }
        public List<OperatorVehicle> GetOperatorVehicleListById(string OperatorVehicleID)
        {
            return db.ExecuteSprocAccessor(DBRoutine.SELECTOPERATORVEHICLETOTALLISTBYID, MapBuilder<OperatorVehicle>.BuildAllProperties(), OperatorVehicleID).ToList();
        }
        //public List<OperatorVehicleList> GetOperatorVehicleListByIdList(string OperatorVehicleID)
        //{
        //    return db.ExecuteSprocAccessor(DBRoutine.SELECTOPERATORVEHICLETOTALLISTBYID, MapBuilder<OperatorVehicleList>.BuildAllProperties(),OperatorVehicleID).ToList();
        //}
        public bool Save<T>(T item, DbTransaction parentTransaction) where T : IContract
        {
            currentTransaction = parentTransaction;
            return Save(item);

        }
        public bool Save<T>(T item) where T : IContract
        {
            var result = 0;
            var operatorvehicle = (OperatorVehicle)(object)item;

            if (currentTransaction == null)
            {
                connection = db.CreateConnection();
                connection.Open();
            }

            var transaction = (currentTransaction == null ? connection.BeginTransaction() : currentTransaction);

            try
            {

                var savecommand = db.GetStoredProcCommand(DBRoutine.SAVEOPERATORVEHICLELIST);
                db.AddInParameter(savecommand, "OperatorVehicleID", System.Data.DbType.String, operatorvehicle.OperatorVehicleID);
                db.AddInParameter(savecommand, "VehicleRegistrationNo", System.Data.DbType.String, operatorvehicle.VehicleRegistrationNo);
                db.AddInParameter(savecommand, "VehicleType", System.Data.DbType.Int16, operatorvehicle.VehicleType);
                db.AddInParameter(savecommand, "VehicleCategory", System.Data.DbType.String, operatorvehicle.VehicleCategory);
                db.AddInParameter(savecommand, "Model", System.Data.DbType.String, operatorvehicle.Model);
                db.AddInParameter(savecommand, "Tonnage", System.Data.DbType.String, operatorvehicle.Tonnage);
                db.AddInParameter(savecommand, "CreatedBy", System.Data.DbType.String, operatorvehicle.CreatedBy);
                db.AddInParameter(savecommand, "ModifiedBy", System.Data.DbType.String, operatorvehicle.ModifiedBy);


                result = db.ExecuteNonQuery(savecommand, transaction);

                if (currentTransaction == null)
                    transaction.Commit();

            }
            catch (Exception ex)
            {
                if (currentTransaction == null)
                    transaction.Rollback();

                throw;
            }
            return (result > 0 ? true : false);

        }
        #endregion
    }
}
