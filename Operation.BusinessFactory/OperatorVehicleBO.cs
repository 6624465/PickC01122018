using System;
using System.Collections.Generic;
using Operation.Contract;
using PickC.Services.DTO;
using Operation.DataFactory;
using Master.Contract;
namespace Operation.BusinessFactory
{
     public  class OperatorVehicleBO
    {
        private OperatorVehicleDAL operatorVehicleDAL;
        public OperatorVehicleBO()
        {
            operatorVehicleDAL = new OperatorVehicleDAL();
        }
        public List<LookUp> GetList()
        {
            return operatorVehicleDAL.GetList();
        }
        public List<LookUp> GetCategoryList()
        {
            return operatorVehicleDAL.GetVehicleCategoryList();
        }
        public List<OperatorVehicles> GetModelList()
        {
            return operatorVehicleDAL.GetModelList();
        }
        public bool SaveOperatorVehicleDTO(OperatorVehicle operatorVehicle)
        {
            return operatorVehicleDAL.Save(operatorVehicle);
        }
        public List<OperatorVehicle> GetOperatorVehicleList()
        {
            return operatorVehicleDAL.GetOperatorVehicleList();
        }
    }
    }

