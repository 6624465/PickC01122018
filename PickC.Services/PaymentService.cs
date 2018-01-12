using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PickC.Services.DTO;
using RestSharp;
using Operation.Contract;
namespace PickC.Services
{
    public class PaymentService:Service
    {
        public PaymentService(string token, string mobileNo): base(token, mobileNo)
        {

        }
        public static string ApiBaseUrl = ConfigurationManager.AppSettings["ApiBaseUrl"];
        public async Task<PaymentHistory> PaymentHistoryDetails(Paymentsearch search)
        {
            IRestClient client = new RestClient(ApiBaseUrl);
            var request = p_request;
            request.Method = Method.POST;
            request.Resource = "opearation/payment/PaymentHistoryDetails";
            request.AddJsonBody(search);

            return ServiceResponse(
                await client.ExecuteTaskAsync<PaymentHistory>(request));
        }
        public async Task<List<DriverPendingCommission>> driverpendingAmountDetails()
        {
            IRestClient client = new RestClient(ApiBaseUrl);
            var request = p_request;
            request.Method = Method.GET;
            request.Resource = "opearation/payment/PendingDriverCommisionDetails";
            
            return ServiceResponse(
                await client.ExecuteTaskAsync<List<DriverPendingCommission>>(request));
        }
        
    }
}
