using Master.Contract;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PickC.Services
{
    public class VehicleTypeService : Service
    {
        protected static string ApiBaseUrl = ConfigurationManager.AppSettings["ApiBaseUrl"];
        public VehicleTypeService(string token, string mobileNo): base(token, mobileNo)
        {

        }
        public async Task<List<LookUp>> GetDataAsync()
        {
            IRestClient client = new RestClient(ApiBaseUrl);
            var request = new RestRequest();
            request.Method = Method.GET;
            request.Resource = "master/vehicletype/list";
            return ServiceResponse(
                await client.ExecuteTaskAsync<List<LookUp>>(request));
        }
    }
}