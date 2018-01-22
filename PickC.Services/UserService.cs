using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Operation.Contract;
using RestSharp;

namespace PickC.Services
{
    public class UserService:Service
    {
        public UserService(string token ,string mobileNo):base(token,mobileNo)
        {

        }
        public static string ApiBaseUrl = ConfigurationManager.AppSettings["ApiBaseUrl"];
        public async Task<List<UserBookingList>> searchBookingTripsAsync(UserData userdata)
        {
            IRestClient client = new RestClient(ApiBaseUrl);
            var request = p_request;
            request.Method = Method.POST;
            request.Resource = "operation/trip/totalTrips";
            request.AddJsonBody(userdata);

            return ServiceResponse(
                await client.ExecuteTaskAsync<List<UserBookingList>>(request));
        }
      public async Task<UserDataDashBoard> GetDashBoardUserData()
        {
            IRestClient client = new RestClient(ApiBaseUrl);
            var request = p_request;
            request.Method = Method.GET;
            request.Resource = "operation/trip/userDataDashBoard";
            return ServiceResponse(await client.ExecuteTaskAsync<UserDataDashBoard>(request));
        }
      
        public async Task<List<CustomerStatus>> GetRegisteredButNotBookedList()
        {
            IRestClient client = new RestClient(ApiBaseUrl);
            var request = p_request;
            request.Method = Method.GET;
            request.Resource = "operation/trip/CustomerStatus";
            return ServiceResponse(await client.ExecuteTaskAsync<List<CustomerStatus>>(request));

        }
     
        public async Task<List<CustomerCancellation>> getCancelledList()
        {
            IRestClient client = new RestClient(ApiBaseUrl);
            var request = p_request;
            request.Method = Method.GET;
            request.Resource = "operation/trip/CancelledListByCustomer";
            return ServiceResponse(await client.ExecuteTaskAsync<List<CustomerCancellation>>(request));

        }
        public async Task<List<DriverCancellation>> getCancelledListDriver()
        {
            IRestClient client = new RestClient(ApiBaseUrl);
            var request = p_request;
            request.Method = Method.GET;
            request.Resource = "operation/trip/CancelledListByDriver";
            return ServiceResponse(await client.ExecuteTaskAsync<List<DriverCancellation>>(request));

        }
        public async Task<List<TripDailyBasis>> getTotalTripDailyBasis()
        {
            IRestClient client = new RestClient(ApiBaseUrl);
            var request = p_request;
            request.Method = Method.GET;
            request.Resource = "operation/trip/GetTotalTripDetailsOnDailyBasis";
            return ServiceResponse(await client.ExecuteTaskAsync<List<TripDailyBasis>>(request));

        }

    }
}
