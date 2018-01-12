using Operation.Contract;
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
  public  class DriverSummaryService:Service
    {
        public DriverSummaryService(string token, string mobileno) : base(token, mobileno)
        {

        }

        public static string ApiBaseUrl = ConfigurationManager.AppSettings["ApiBaseUrl"];

        public async Task<SummaryDTO> GetDriverSummary(string driverID) {
            IRestClient client = new RestClient(ApiBaseUrl);

            var request = p_request;
            request.Method = Method.POST;
            request.Resource = "opearation/payment/summary/{driverID}";
            request.AddParameter("driverID", driverID, ParameterType.UrlSegment);

            return await Task.Run(() =>
            {
                return ServiceResponse<SummaryDTO>(client.Execute<SummaryDTO>(request));
            });
        }

        public async Task<List<DriverPaymentsDTO>> GetPayment(string driverID) {

            IRestClient client = new RestClient(ApiBaseUrl);
            var request = p_request;
            request.Method = Method.GET;
            request.Resource = "opearation/payment/payments/{driverID}";
            request.AddParameter("driverID", driverID,ParameterType.UrlSegment);

            return await Task.Run(()=> {
                return ServiceResponse<List<DriverPaymentsDTO>>(client.Execute<List<DriverPaymentsDTO>>(request));
            });
        }

        //public async Task<string> GetPaymentDetails(string driverID)
        //{
        //    IRestClient client = new RestClient(ApiBaseUrl);
        //    var request = p_request;
        //    request.Method = Method.GET;
        //    request.Resource = "opearation/payment/payments/{driverID}";
            

        //}
    }
}
