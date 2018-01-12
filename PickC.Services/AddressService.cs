using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Threading.Tasks;
using System.Configuration;
using System.Net;

using Master.Contract;
using RestSharp;

namespace PickC.Services
{
    public class AddressService : Service
    {
        public AddressService(string token, string mobileNo): base(token, mobileNo)
        {

        }
        public static string ApiBaseUrl = ConfigurationManager.AppSettings["ApiBaseUrl"];

        public async Task<List<Address>> AddressListAsync(string addressLinkID)
        {
            IRestClient client = new RestClient(ApiBaseUrl);
            var request =p_request;
            request.Method = Method.GET;
            request.Resource = "master/address/list/{addressLinkID}";
            request.AddParameter("addressLinkID", addressLinkID, ParameterType.UrlSegment);

            return ServiceResponse<List<Address>>(
                await client.ExecuteTaskAsync<List<Address>>(request));
        }

        public async Task<Address> AddressAsync(int addressID)
        {
            IRestClient client = new RestClient(ApiBaseUrl);
            var request = p_request;
            request.Method = Method.GET;
            request.Resource = "master/address/{addressID}";
            request.AddParameter("addressID", addressID, ParameterType.UrlSegment);

            return ServiceResponse<Address>(
                await client.ExecuteTaskAsync<Address>(request));
        }

        public async Task<string> SaveAsync(Address address)
        {
            IRestClient client = new RestClient(ApiBaseUrl);
            var request = p_request;
            request.Method = Method.POST;
            request.Resource = "master/address/save";
            request.AddJsonBody(address);

            return ServiceResponse(
                await client.ExecuteTaskAsync(request));
        }

        public async Task<string> DeleteAsync(int addressID)
        {
            IRestClient client = new RestClient(ApiBaseUrl);
            var request = p_request;
            request.Method = Method.DELETE;
            request.Resource = "master/address/{addressID}";

            return ServiceResponse(
                await client.ExecuteTaskAsync(request));
        }
    }
}