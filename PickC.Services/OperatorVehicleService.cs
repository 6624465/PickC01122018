using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Threading.Tasks;
using System.Configuration;
using RestSharp;
using Master.Contract;
using PickC.Services.DTO;

namespace PickC.Services
{
    public class OperatorVehicleService : Service
    {
        public static string ApiBaseUrl = ConfigurationManager.AppSettings["ApiBaseUrl"];
        public OperatorVehicleService(string token, string mobileNo) : base(token, mobileNo)
        {

        }
        public async Task<List<LookUp>> GetOperatorVehicleList()
        {
            IRestClient client = new RestClient(ApiBaseUrl);
            var request = p_request;
            request.Method = Method.GET;
            request.Resource = "operator/Vehicle/list";

            return await Task.Run(() =>
            {
                return ServiceResponse<List<LookUp>>(client.Execute<List<LookUp>>(request));
            });
        }
        public async Task<List<LookUp>> GetOperatorVehicleCategoryList()
        {
            IRestClient client = new RestClient(ApiBaseUrl);
            var request = p_request;
            request.Method = Method.GET;
            request.Resource = "operator/Vehicle/CategoryList";

            return await Task.Run(() =>
            {
                return ServiceResponse<List<LookUp>>(client.Execute<List<LookUp>>(request));
            });
        }
        public async Task<List<OperatorVehicles>> GetOperatorModelList()
        {
            IRestClient client = new RestClient(ApiBaseUrl);
            var request = p_request;
            request.Method = Method.GET;
            request.Resource = "operator/Vehicle/ModelList";

            return await Task.Run(() =>
            {
                return ServiceResponse<List<OperatorVehicles>>(client.Execute<List<OperatorVehicles>>(request));
            });
        }
        public async Task<string> SaveOperatorVehicleList(OperatorVehicle operatorVehicle)
        {
            OperatorVehicle ov = new OperatorVehicle();
            IRestClient client = new RestClient(ApiBaseUrl);
            var request = p_request;
            request.Method = Method.POST;
            request.Resource = "operator/Vehicle/save";
            request.AddJsonBody(operatorVehicle);
            return await Task.Run(() =>
            {
                return ServiceResponse(client.Execute(request));
            });
            
        }
        public async Task<List<OperatorVehicle>> OperatorVehicleList()
        {
            IRestClient client = new RestClient(ApiBaseUrl);
            var request = p_request;
            request.Method = Method.GET;
            request.Resource = "operator/Vehicle/totalList";

            return await Task.Run(() =>
            {
                return ServiceResponse<List<OperatorVehicle>>(client.Execute<List<OperatorVehicle>>(request));
            });
        }
    }
    }

