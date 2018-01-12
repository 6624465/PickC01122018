using PickC.Services.DTO;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PickC.Services
{
    public class ManufacturerService : Service
    {
        public ManufacturerService(string token, string mobileno) : base(token, mobileno)
        {

        }
        public ManufacturerService() : base("", "")
        {

        }

        public static string ApiBaseUrl = ConfigurationManager.AppSettings["ApiBaseUrl"];


        public async Task<string> Save(DriverManufacturerDTO manufacturer)
        {
            IRestClient client = new RestClient(ApiBaseUrl);
            var request = p_request;
            request.Method = Method.POST;
            request.Resource = "master/manufacturer/save";
            request.AddJsonBody(manufacturer);

            return ServiceResponse(
              await client.ExecuteTaskAsync(request));
        }
    }
}
