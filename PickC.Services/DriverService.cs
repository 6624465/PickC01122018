﻿using System;
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
    public class DriverService : Service
    {
        public static string ApiBaseUrl = ConfigurationManager.AppSettings["ApiBaseUrl"];
        public DriverService(string token, string mobileNo) : base(token, mobileNo)
        {

        }
        public async Task<List<Driver>> DriversListAsync()
        {
            IRestClient client = new RestClient(ApiBaseUrl);
            var request = p_request;
            request.Method = Method.GET;
			//request.Resource = "master/driver/list";
			request.Resource = "master/driver/driverlist";

			return await Task.Run(() =>
            {
                return ServiceResponse<List<Driver>>(client.Execute<List<Driver>>(request));
            });
        }
        public async Task<List<DriverDetails>> DriverDetailListAsync()
        {
            IRestClient client = new RestClient(ApiBaseUrl);
            var request = p_request;
            request.Method = Method.GET;
            request.Resource = "master/driver/DriverDetailList";

            return await Task.Run(() =>
            {
                return ServiceResponse<List<DriverDetails>>(client.Execute<List<DriverDetails>>(request));
            });
        }

        public async Task<List<DriverDetailsWithPic>> DriverDetailListPicAsync()
        {
            IRestClient client = new RestClient(ApiBaseUrl);
            var request = p_request;
            request.Method = Method.GET;
            request.Resource = "master/driver/profilepic/DriverDetailList";

            return await Task.Run(() =>
            {
                return ServiceResponse<List<DriverDetailsWithPic>>(client.Execute<List<DriverDetailsWithPic>>(request));
            });
        }

        public async Task<string> SaveDriverAsync(DriverMdl driver)
        {
            IRestClient client = new RestClient(ApiBaseUrl);
            var request = p_request;
            request.Method = Method.POST;
			//request.Resource = "master/driver/save";
			request.Resource = "master/driver/savedriver";
			request.AddJsonBody(driver);

            return await Task.Run(() =>
            {
                return ServiceResponse(client.Execute(request));
            });
        }
        public async Task<Driver> DriverInfoAsync(string driverID)
        {
            IRestClient client = new RestClient(ApiBaseUrl);
            var request = p_request;
            request.Method = Method.GET;
            request.Resource = "master/driver/{driverID}";
            request.AddParameter("driverID", driverID, ParameterType.UrlSegment);

            return await Task.Run(() =>
            {
                return ServiceResponse<Driver>(client.Execute<Driver>(request));
            });
        }

		//added by Kiran
		public async Task<DriverMdl> DriverByIDInfoAsync(string driverID)
		{
			IRestClient client = new RestClient(ApiBaseUrl);
			var request = p_request;
			request.Method = Method.GET;
			request.Resource = "master/driver/DriverInfoByID/{driverByID}";
			request.AddParameter("driverByID", driverID, ParameterType.UrlSegment);

			return await Task.Run(() =>
			{
				return ServiceResponse<DriverMdl>(client.Execute<DriverMdl>(request));
			});
		}

		public async Task<string> DeleteDriverAsync(string driverID)
        {
            IRestClient client = new RestClient(ApiBaseUrl);
            var request = p_request;
            request.Method = Method.DELETE;
            request.Resource = "master/driver/{driverID}";
            request.AddParameter("driverID", driverID, ParameterType.UrlSegment);

            return await Task.Run(() =>
            {
                return ServiceResponse(client.Execute(request));
            });
        }

        public async Task<DriverLookupDTO> LookUpDataAsync()
        {
            IRestClient client = new RestClient(ApiBaseUrl);
            var request = p_request;
            request.Method = Method.GET;
            request.Resource = "master/driver/lookupdata";

            return await Task.Run(() =>
            {
                return ServiceResponse<DriverLookupDTO>(client.Execute<DriverLookupDTO>(request));
            });

        }


        public async Task<List<Driver>> GetDriverBySearch(bool? status)
        {

            IRestClient client = new RestClient(ApiBaseUrl);
            var request = p_request;
            request.Method = Method.GET;

            if (status.HasValue)
            {
                request.Resource = "master/driver/list/driverbyname/{status}";
                request.AddParameter("status", status, ParameterType.UrlSegment);
            }
            else
            {
                request.Resource = "master/driver/list/driverbyname/";
            }

            return await Task.Run(() =>
            {
                return ServiceResponse<List<Driver>>(client.Execute<List<Driver>>(request));
            });
        }

        public async Task<List<DriverAttachmentListStatus>> GetDriverBySearch(string status)
        {

            IRestClient client = new RestClient(ApiBaseUrl);
            var request = p_request;
            request.Method = Method.GET;
            request.Resource = "master/driver/list/driverbyname/{status}";
            request.AddParameter("status", status, ParameterType.UrlSegment);

            //if (status.HasValue)
            //{
            //    request.Resource = "master/driver/list/driverbyname/{status}";
            //    request.AddParameter("status", status, ParameterType.UrlSegment);
            //}
            //else
            //{
            //    request.Resource = "master/driver/list/driverbyname/";
            //}

            return await Task.Run(() =>
            {
                return ServiceResponse<List<DriverAttachmentListStatus>>(client.Execute<List<DriverAttachmentListStatus>>(request));
            });
        }

        public async Task<string> SaveDriverAttachmentAsync(DriverAttachmentsDTO attachment) {

            IRestClient client = new RestClient(ApiBaseUrl);
            var request = p_request;
            request.Method = Method.POST;
            request.Resource = "master/driver/saveattachment";
            request.AddJsonBody(attachment);

            return await Task.Run(()=>

            {
                return ServiceResponse(client.Execute(request));
            });
            
        }

        public async Task<string> IsOperatorValid(string operatorId)
        {
            IRestClient client = new RestClient(ApiBaseUrl);
            var request = p_request;
            request.Method = Method.GET;
            request.Resource = "master/driver/IsOperatorValid/{operatorId}";
            request.AddParameter("operatorId", operatorId, ParameterType.UrlSegment);

            return await Task.Run(() =>
            {
                return ServiceResponse(client.Execute(request));
            });
        }

        public async Task<List<DriverPendingAmount>> DriverPendingAmountAsync()
        {
            IRestClient client = new RestClient(ApiBaseUrl);
            var request = p_request;
            request.Method = Method.GET;
            request.Resource = "master/driver/driverpendingamount";

            return await Task.Run(() =>
            {
                return ServiceResponse<List<DriverPendingAmount>>(client.Execute<List<DriverPendingAmount>>(request));
            });
        }

        public async Task<string> IsAadharExistsAsync(string aadharno)
        {
            IRestClient client = new RestClient(ApiBaseUrl);
            var request = p_request;
            request.Method = Method.GET;
            request.Resource = "master/driver/AadharCardNoExist/{Aadharcardno}";
            request.AddParameter("Aadharcardno", aadharno, ParameterType.UrlSegment);


            return await Task.Run(() =>
            {
                return ServiceResponse(client.Execute(request));
            });
        }


        public async Task<string> IsPancardExistsAsync(string pancardno)
        {
            IRestClient client = new RestClient(ApiBaseUrl);
            var request = p_request;
            request.Method = Method.GET;
            request.Resource = "master/driver/PanCardNoExist/{Pancardno}";
            request.AddParameter("Pancardno", pancardno, ParameterType.UrlSegment);


            return await Task.Run(() =>
            {
                return ServiceResponse(client.Execute(request));
            });
        }

        public async Task<string> IsMobileNoExistsAsync(string mobileno)
        {
            IRestClient client = new RestClient(ApiBaseUrl);
            var request = p_request;
            request.Method = Method.GET;
            request.Resource = "master/driver/MobileNoExist/{Mobileno}";
            request.AddParameter("Mobileno", mobileno, ParameterType.UrlSegment);


            return await Task.Run(() =>
            {
                return ServiceResponse(client.Execute(request));
            });
        }

        public async Task<string> IsAccountNoExistsAsync(string accountno)
        {
            IRestClient client = new RestClient(ApiBaseUrl);
            var request = p_request;
            request.Method = Method.GET;
            request.Resource = "master/driver/AccountNoExist/{Accountno}";
            request.AddParameter("Accountno", accountno, ParameterType.UrlSegment);


            return await Task.Run(() =>
            {
                return ServiceResponse(client.Execute(request));
            });
        }

    }
}