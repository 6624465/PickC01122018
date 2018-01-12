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
    public class SearchService:Service
    {
        public SearchService(string token, string mobileNo): base(token, mobileNo)
        {

        }
        public static string ApiBaseUrl = ConfigurationManager.AppSettings["ApiBaseUrl"];
        public async Task<List<Booking>> SearchCurrentBookingAsync(BookingDTO bookingDTO)
        {
            IRestClient client = new RestClient(ApiBaseUrl);
            var request = p_request;
            request.Method = Method.POST;
            request.Resource = "operation/search/currentbooking";
            request.AddJsonBody(bookingDTO);

            return ServiceResponse(
                await client.ExecuteTaskAsync<List<Booking>>(request));
        }

        public async Task<List<Booking>> SearchBookingByDateAsync(BookingSearchDTO booking) {
            IRestClient client = new RestClient(ApiBaseUrl);
            var request = p_request;
            request.Method = Method.POST;
            request.Resource = "operation/search/bookingbydate";
            request.AddJsonBody(booking);

            //request.AddParameter("fromDate", fromDate, ParameterType.UrlSegment);
            //request.AddParameter("toDate", toDate, ParameterType.UrlSegment);

            return ServiceResponse(await client.ExecuteTaskAsync<List<Booking>>(request));
        }
        public async Task<List<Booking>> BookingListAsync()
        {
            IRestClient client = new RestClient(ApiBaseUrl);
            var request = p_request;
            request.Method = Method.GET;
            request.Resource = "operation/search/list";

            return await Task.Run(() =>
            {
                return ServiceResponse<List<Booking>>(client.Execute<List<Booking>>(request));
            });
        }
        public async Task<List<BookingHistory>> BookingHistoryListAsync()
        {
            IRestClient client = new RestClient(ApiBaseUrl);
            var request = p_request;
            request.Method = Method.GET;
            request.Resource = "operation/search/list";

            return await Task.Run(() =>
            {
                return ServiceResponse<List<BookingHistory>>(client.Execute<List<BookingHistory>>(request));
            });
        }
        public async Task<List<Booking>> GetCustomerBySearch(int? status)
        {
            IRestClient client = new RestClient(ApiBaseUrl);
            var request = p_request;
            request.Method = Method.POST;
            request.Resource = "operation/search/list/driverbyname/{status}";
            request.AddParameter("status", status, ParameterType.UrlSegment);
            return await Task.Run(() =>
            {
                return ServiceResponse<List<Booking>>(client.Execute<List<Booking>>(request));
            });
        }
        public async Task<List<BookingHistory>> SearchBookingHistoryAsync(BookingHistoryDTO bookingDTO)
        {
            IRestClient client = new RestClient(ApiBaseUrl);
            var request = p_request;
            request.Method = Method.POST;
            request.Resource = "operation/search/bookingHistory";
            request.AddJsonBody(bookingDTO);

            return ServiceResponse(
                await client.ExecuteTaskAsync<List<BookingHistory>>(request));
        }
        public async Task<List<Booking>> SearchBookingAsync(BookingSearchsDTO bookingDTO)
        {
            IRestClient client = new RestClient(ApiBaseUrl);
            var request = p_request;
            request.Method = Method.POST;
            request.Resource = "operation/search/bookingHistory";
            request.AddJsonBody(bookingDTO);

            return ServiceResponse(
                await client.ExecuteTaskAsync<List<Booking>>(request));
        }
    }
}
