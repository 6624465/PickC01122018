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
public class OperatorDriverService : Service
    {
        public static string ApiBaseUrl = ConfigurationManager.AppSettings["ApiBaseUrl"];
        public OperatorDriverService(string token, string mobileNo) : base(token, mobileNo)
        {

        }
        public async Task<List<OperatorDriver>> GetDriverList()
        {
            IRestClient client = new RestClient(ApiBaseUrl);
            var request = p_request;
            request.Method = Method.GET;
            request.Resource = "operator/Driver/list";

            return await Task.Run(() =>
            {
                return ServiceResponse<List<OperatorDriver>>(client.Execute<List<OperatorDriver>>(request));
            });
        }
        public async Task<List<OperatorVehuicleAttachedNo>> GetvehicleNoList(string OPeratorID)
        {
            IRestClient client = new RestClient(ApiBaseUrl);
            var request = p_request;
            request.Method = Method.GET;
            request.Resource = "operator/Driver/VehicleNolist/{OPeratorID}";
            request.AddParameter("OPeratorID", OPeratorID, ParameterType.UrlSegment);

            return await Task.Run(() =>
            {
                return ServiceResponse<List<OperatorVehuicleAttachedNo>>(client.Execute<List<OperatorVehuicleAttachedNo>>(request));
            });
        }
        public async Task<List<OperatorVehuicleAttachedNo>> GetvehicleNoList()
        {
            IRestClient client = new RestClient(ApiBaseUrl);
            var request = p_request;
            request.Method = Method.GET;
            request.Resource = "operator/Driver/VehicleNolist";
            return await Task.Run(() =>
            {
                return ServiceResponse<List<OperatorVehuicleAttachedNo>>(client.Execute<List<OperatorVehuicleAttachedNo>>(request));
            });
        }
        public async Task<string> RegisterOTPAsync(string mobile, string otp)
        {
            IRestClient client = new RestClient(ApiBaseUrl);
            var request = new RestRequest();
            request.Method = Method.POST;
            request.Resource = "master/customer/verifyotp/{mobile}/{otp}";
            request.AddParameter("mobile", mobile, ParameterType.UrlSegment);
            request.AddParameter("otp", otp, ParameterType.UrlSegment);
            return ServiceResponse(
                 await client.ExecuteTaskAsync(request));

        }
        public async Task<string> SaveOperatorDriverList(OperatorDriverList operatorDriverList)
        {
            IRestClient client = new RestClient(ApiBaseUrl);
            var request = p_request;
            request.Method = Method.POST;
            request.Resource = "operator/Driver/save";
            request.AddJsonBody(operatorDriverList);
            return await Task.Run(() =>
            {
                return ServiceResponse(client.Execute(request));
            });
        }
        public async Task<List<OperatorDriverList>> OperatorDriverList()
        {
            try
            {
                IRestClient client = new RestClient(ApiBaseUrl);
                var request = p_request;
                request.Method = Method.GET;
                request.Resource = "operator/Driver/totalList";

                return await Task.Run(() =>
                {
                    return ServiceResponse<List<OperatorDriverList>>(client.Execute<List<OperatorDriverList>>(request));
                });
            }
          catch(Exception ex)
            {
                throw ex;
            }          
        }
    }
}
