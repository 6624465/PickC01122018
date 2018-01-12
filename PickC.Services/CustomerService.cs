using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using RestSharp;

using Master.Contract;
using PickC.Services.DTO;


namespace PickC.Services
{
    public class CustomerService : Service
    {
        public CustomerService(string token, string mobileNo): base(token, mobileNo)
        {

        }

        public CustomerService(): base("","")
        {

        }
        public static string ApiBaseUrl = ConfigurationManager.AppSettings["ApiBaseUrl"];

        public async Task<LoginDTO> LoginAsync(Customer customer)
        {
            IRestClient client = new RestClient(ApiBaseUrl);
            var request = new RestRequest();
            request.Method = Method.POST;
            request.Resource = "master/customer/login";
            request.AddJsonBody(customer);
            try
            {
                return ServiceResponse<LoginDTO>(
                    await client.ExecuteTaskAsync<LoginDTO>(request));
            }
            catch(Exception ex)
            {
                return new LoginDTO {
                    Token = ex.Message
                };
            }
            
        }
        public async Task<string> RegisterAsync(Customer customer)
        {
            IRestClient client = new RestClient(ApiBaseUrl);
            var request = new RestRequest();
            request.Method = Method.POST;
            request.Resource = "master/customer/save";
            request.AddJsonBody(customer);
            return ServiceResponse(
                 await client.ExecuteTaskAsync(request));

        }
        public async Task<string> SaveImageRegisterAsync(DriverImageRegister driverImageRegister)
        {
            IRestClient client = new RestClient(ApiBaseUrl);
            var request = new RestRequest();
            request.Method = Method.POST;
            request.Resource = "master/customer/saveImageRegister";
            request.AddJsonBody(driverImageRegister);
            return ServiceResponse(
                 await client.ExecuteTaskAsync(request));

        }

        public async Task<string> RegisterOTPAsync(string mobile,string otp)
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

        /* while calling this function session is not yet set */
        public async Task<Customer> GetCustomerInfoAsync(string token, string mobile)
        {
            IRestClient client = new RestClient(ApiBaseUrl);
            var request = new RestRequest();
            request.Method = Method.GET;

            request.AddHeader("AUTH_TOKEN", token);
            request.AddHeader("MOBILENO", mobile);

            request.Resource = "master/customer/{mobile}";
            request.AddParameter("mobile", mobile, ParameterType.UrlSegment);

            return ServiceResponse<Customer>(
                await client.ExecuteTaskAsync<Customer>(request));

        }

        public async Task<List<CustomerDTO>> GetCustomerListAsync()
        {
            IRestClient client = new RestClient(ApiBaseUrl);
            var request = p_request;
            request.Method = Method.GET;
            request.Resource = "master/customer/customerList";

            return ServiceResponse<List<CustomerDTO>>(
                await client.ExecuteTaskAsync<List<CustomerDTO>>(request));
        }

        public async Task<string> UpdateCustomerAsync(Customer customer)
        {
            IRestClient client = new RestClient(ApiBaseUrl);
            var request = p_request;
            request.Method = Method.POST;
            request.Resource = "master/customer/{mobile}";
            request.AddParameter("mobile", customer.MobileNo, ParameterType.UrlSegment);
            request.AddJsonBody(customer);

            return ServiceResponse(
                await client.ExecuteTaskAsync(request));
        }

        public async Task<string> LogoutAsync()
        {
            IRestClient client = new RestClient(ApiBaseUrl);
            var request = p_request;
            request.Method = Method.GET;
            request.Resource = "master/customer/logout";

            return ServiceResponse(
                await client.ExecuteTaskAsync(request));
        }
        public async Task<TripInvoice> TripInvoiceList(string BookingNo)
        {
            IRestClient client = new RestClient(ApiBaseUrl);
            var request = p_request;
            request.Method = Method.GET;
            request.Resource = "master/customer/TripInvoice/{BookingNo}";
            request.AddParameter("BookingNo", BookingNo, ParameterType.UrlSegment);
            request.AddJsonBody(BookingNo);

            return ServiceResponse<TripInvoice>(
                await client.ExecuteTaskAsync<TripInvoice>(request));
        }

        public async Task<string> SendMail(ContactUs contactUs)
        {
            IRestClient client = new RestClient(ApiBaseUrl);
            var request = new RestRequest();
            request.Method = Method.POST;
            request.Resource = "master/customer/SendMessageToPickC";
            request.AddJsonBody(contactUs);
            return ServiceResponse(
                 await client.ExecuteTaskAsync(request));

        }
        public async Task<CustomerInfo> GetCustomerDetailsAsync(string MobileNo)
        {
            IRestClient client = new RestClient(ApiBaseUrl);
            var request = new RestRequest();
            request.Method = Method.GET;
            request.Resource = "master/customer/GetCustomerDetails/{MobileNo}";
             request.AddParameter("MobileNo", MobileNo, ParameterType.UrlSegment);
            request.AddJsonBody(MobileNo);
            return ServiceResponse<CustomerInfo>(
                 await client.ExecuteTaskAsync<CustomerInfo>(request));

        }
        
    }
}
