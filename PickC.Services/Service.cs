using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RestSharp;
using System.Net;

namespace PickC.Services
{
    public class Service
    {
        public Service(string token, string mobileNo)
        {
            this.AUTHTOKEN = token;
            this.p_mobileNo = mobileNo;
        }

        public RestRequest restRequest;
        public string AUTHTOKEN;
        public string p_mobileNo;
        public RestRequest p_request
        {
            get
            {
                restRequest = new RestRequest();
                restRequest.AddHeader("AUTH_TOKEN", AUTHTOKEN);
                restRequest.AddHeader("MOBILENO", p_mobileNo);

                return restRequest;
            }
        }
        public T ServiceResponse<T>(IRestResponse<T> response)
         {
            if (response.StatusCode == HttpStatusCode.OK)
                return response.Data;
            else if (response.StatusCode == HttpStatusCode.Unauthorized)
                throw new Exception(HttpStatusCode.Unauthorized.ToString());

            return default(T);
        }

        public string ServiceResponse(IRestResponse response)
        {
            if (response.StatusCode == HttpStatusCode.OK)
                return response.Content;
            else if (response.StatusCode == HttpStatusCode.Unauthorized)
                throw new Exception(HttpStatusCode.Unauthorized.ToString());

            return default(string);
        }
        /*
        public T ServiceResponse<T>(IRestResponse<T> response)
        {
            if (response.StatusCode == HttpStatusCode.OK)
                return response.Data;
            else if (response.StatusCode == HttpStatusCode.Unauthorized)
                throw new HttpResponseException(HttpStatusCode.Unauthorized);

            return default(T);
        }

        public string ServiceResponse(IRestResponse response)
        {
            if (response.StatusCode == HttpStatusCode.OK)
                return response.Content;
            else if (response.StatusCode == HttpStatusCode.Unauthorized)
                throw new HttpResponseException(HttpStatusCode.Unauthorized);

            return default(string);
        }
        */
    }
}
