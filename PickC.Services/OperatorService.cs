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
    public class OperatorService : Service
    {
        public static string ApiBaseUrl = ConfigurationManager.AppSettings["ApiBaseUrl"];
        public OperatorService(string token, string mobileNo) : base(token, mobileNo)
        {

        }
        public async Task<List<Operator>> OperatorsListAsync()
        {
            IRestClient client = new RestClient(ApiBaseUrl);
            var request = p_request;
            request.Method = Method.GET;
            request.Resource = "master/operator/list";

            return await Task.Run(() =>
            {
                return ServiceResponse<List<Operator>>(client.Execute<List<Operator>>(request));
            });
        }
        public async Task<List<Operator>> GetOperatorList()
        {
            IRestClient client = new RestClient(ApiBaseUrl);
            var request = p_request;
            request.Method = Method.GET;
            request.Resource = "master/driver/Operatorlist";

            return await Task.Run(() =>
            {
                return ServiceResponse<List<Operator>>(client.Execute<List<Operator>>(request));
            });
        }
        
        public async Task<OperatorLookupDTO> LookUpDataAsync()
        {
            IRestClient client = new RestClient(ApiBaseUrl);
            var request = p_request;
            request.Method = Method.GET;
            request.Resource = "master/operator/lookupdata";

            return await Task.Run(() =>
            {
                return ServiceResponse<OperatorLookupDTO>(client.Execute<OperatorLookupDTO>(request));
            });
        }
        public async Task<Operator> OperatorInfoAsync(string operatorID)
        {
            IRestClient client = new RestClient(ApiBaseUrl);
            var request = p_request;
            request.Method = Method.GET;
            request.Resource = "master/operator/{operatorID}";
            request.AddParameter("operatorID", operatorID, ParameterType.UrlSegment);

            return await Task.Run(() =>
            {
                return ServiceResponse<Operator>(client.Execute<Operator>(request));
            });
        }
        public async Task<string> DeleteOperatorAsync(string operatorID)
        {
            IRestClient client = new RestClient(ApiBaseUrl);
            var request = p_request;
            request.Method = Method.DELETE;
            request.Resource = "master/operatorID/{OPEratorID}";
            request.AddParameter("OPEratorID", operatorID, ParameterType.UrlSegment);

            return await Task.Run(() =>
            {
                return ServiceResponse(client.Execute(request));
            });
        }
      
        public async Task<string> SaveOperatorAsync(Operator OPerator)
        {
            IRestClient client = new RestClient(ApiBaseUrl);
            var request = p_request;
            request.Method = Method.POST;
            request.Resource = "master/operator/save";
            request.AddJsonBody(OPerator);

            return await Task.Run(() =>
            {
                return ServiceResponse(client.Execute(request));
            });
        }
        public async Task<string> SaveOperatorAttachmentAsync(OperatorAttachment attachment)
        {

            IRestClient client = new RestClient(ApiBaseUrl);
            var request = p_request;
            request.Method = Method.POST;
            request.Resource = "master/operator/saveattachment";
            request.AddJsonBody(attachment);

            return await Task.Run(() =>

            {
                return ServiceResponse(client.Execute(request));
            });

        }
    }
}
