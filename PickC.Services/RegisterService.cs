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
    public class RegisterService : Service
    {
        public static string ApiBaseUrl = ConfigurationManager.AppSettings["ApiBaseUrl"];
        public RegisterService(string token, string mobileNo): base(token, mobileNo)
        {

        }

        public async Task<TripInvoice> GetBookingInvoiceByBookingNoAsync(string bookingNo)
        {
            IRestClient client = new RestClient(ApiBaseUrl);
            var request = p_request;
            request.Method = Method.GET;
            request.Resource = "master/customer/sendInvoiceMail/{BookingNo}/{EmailId}/{Ismail}";
            request.AddParameter("BookingNo", bookingNo, ParameterType.UrlSegment);
            request.AddParameter("EmailId", "prasad@gmail.com", ParameterType.UrlSegment);
            request.AddParameter("Ismail", false, ParameterType.UrlSegment);

            return ServiceResponse(
                await client.ExecuteTaskAsync<TripInvoice>(request));
        }
    }
}
