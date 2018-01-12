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
    public class RateCardService : Service
    {
        protected static string ApiBaseUrl = ConfigurationManager.AppSettings["ApiBaseUrl"];
        public RateCardService(string token, string mobileNo) : base(token, mobileNo)
        {

        }
        
        public async Task<List<RateCard>> GetDataAsync()
        {
            IRestClient client = new RestClient(ApiBaseUrl);
            var request = new RestRequest();
            request.Method = Method.GET;
            request.Resource = "master/ratecard/list";

            return ServiceResponse(
                await client.ExecuteTaskAsync<List<RateCard>>(request));
        }
    }
}