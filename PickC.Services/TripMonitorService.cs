using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Configuration;
using System.Threading.Tasks;
using RestSharp;

using Operation.Contract;

namespace PickC.Services
{
    public class TripMonitorService : Service
    {
        public static string ApiBaseUrl = ConfigurationManager.AppSettings["ApiBaseUrl"];
        public TripMonitorService(string token, string mobileNo): base(token, mobileNo)
        {
            
        }
        public async Task<List<TripMonitor>> TripMonitorListAsync()
        {
            IRestClient client = new RestClient(ApiBaseUrl);
            var request = p_request;
            request.Method = Method.GET;
            request.Resource = "operation/tripmonitor/list";

            return ServiceResponse<List<TripMonitor>>(
                await client.ExecuteTaskAsync<List<TripMonitor>>(request));
        }
    }
}