using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Threading.Tasks;
using System.Configuration;
using System.Net;

using RestSharp;
using Master.Contract;

namespace PickC.Services
{
    public class VehicleGroupService : Service
    {
        protected static string ApiBaseUrl = ConfigurationManager.AppSettings["ApiBaseUrl"];
        public VehicleGroupService(string token, string mobileNo): base(token, mobileNo)
        {

        }
        public async Task<List<LookUp>> GetDataAsync()
        {
            IRestClient client = new RestClient(ApiBaseUrl);
            var request = new RestRequest();
            request.Method = Method.GET;
            request.Resource = "master/vehiclegroup/list";
            return ServiceResponse(
                await client.ExecuteTaskAsync<List<LookUp>>(request));
        }
        public async Task<string> SaveVehicleConfig(VehicleConfig con)
        {
            IRestClient client = new RestClient(ApiBaseUrl);
            var request = new RestRequest();
            request.Method = Method.POST;
            request.Resource = "master/vehicleconfig/save";
            request.AddJsonBody(con);
            return await Task.Run(() =>
            {
                return ServiceResponse(client.Execute(request));
            });
        }
        public async Task<List<VehicleConfig>> GetVehicleConfig()
        {
            IRestClient client = new RestClient(ApiBaseUrl);
            var request = new RestRequest();
            request.Method = Method.GET;
            request.Resource = "master/vehicleconfig/list";
            return ServiceResponse(
                await client.ExecuteTaskAsync<List<VehicleConfig>>(request));
        }
    }
}