
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Configuration;
using PickC.Services.DTO;

using Operation.BusinessFactory;

namespace PickCApi.Areas.Operation.Controllers
{
    [RoutePrefix("api/opearation/payment")]
    public class PaymentController : ApiBase
    {
        [HttpPost]
        [Route("summary/{driverID}")]
        public IHttpActionResult GetDriverSummary(string driverID) {
            try
            {
                var result = new SummaryBO().GetSummary(driverID);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpGet]
        [Route("payments/{driverID}")]
        public IHttpActionResult GetDriverPayments(string driverID) {
            try
            {
                var result = new SummaryBO().GetPayments(driverID);
                return Ok(result);
            }
            catch (Exception ex)
            {

                return InternalServerError(ex);
            }
        }

        //[HttpPost]
        //[Route("getRSAKey")]
        //public IHttpActionResult getRSAKey(RSAObject obj)
        //{
        //    var CCAVENUE_ACCESS_CODE = ConfigurationManager.AppSettings["CCAVENUE_ACCESS_CODE"];
        //    if(CCAVENUE_ACCESS_CODE == obj.access_code)
        //    {
        //        string vParams = "access_code=" + obj.access_code + "&" + "order_id=" + obj.order_id;
        //        string queryUrl = "https://secure.ccavenue.com/transaction/getRSAKey";
        //        var encStr = postPaymentRequestToGateway(queryUrl, vParams);
        //        if (encStr != null && encStr != "")
        //        {
        //            return Ok(new
        //            {
        //                RSAKey = encStr
        //            });
        //        }
        //        else
        //        {
        //            return Ok(new { RSAKey = "" });
        //        }
        //    }
        //    else
        //    {
        //        return Unauthorized();
        //    }            
        //}

        [HttpGet]
        [Route("ccavenue/cancel/{mobile}")]
        public IHttpActionResult ccavenueCancel(string mobile)
        {
            return Ok("Cancelled");
        }

        [HttpGet]
        [Route("ccavenue/redirect/{mobile}/{bookingNo}")]
        public IHttpActionResult redirect(string mobile)
        {
            return Ok("redirected");
        }

        private string postPaymentRequestToGateway(String queryUrl, String urlParam)
        {
            String message = "";
            try
            {
                StreamWriter myWriter = null;// it will open a http connection with provided url
                WebRequest objRequest = WebRequest.Create(queryUrl);//send data using objxmlhttp object
                objRequest.Method = "POST";
                //objRequest.ContentLength = TranRequest.Length;
                objRequest.ContentType = "application/x-www-form-urlencoded";//to set content type
                myWriter = new System.IO.StreamWriter(objRequest.GetRequestStream());
                myWriter.Write(urlParam);//send data
                myWriter.Close();//closed the myWriter object

                // Getting Response
                System.Net.HttpWebResponse objResponse = (System.Net.HttpWebResponse)objRequest.GetResponse();//receive the responce from objxmlhttp object 
                using (System.IO.StreamReader sr = new System.IO.StreamReader(objResponse.GetResponseStream()))
                {
                    message = sr.ReadToEnd();
                }
            }
            catch (Exception exception)
            {
                Console.Write("Exception occured while connection." + exception);
            }
            return message;
        }
        [HttpPost]
        [Route("PaymentHistoryDetails")]
        public IHttpActionResult getPaymentdetails(Paymentsearch search)
        {
            try
            {
                var result = new SummaryBO().PaymentDetails(search.DateFrom,search.DateTo);
                    return Ok(result);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        [HttpGet]
        [Route("PendingDriverCommisionDetails")]
        public IHttpActionResult getPendingDriverCommission()
        {
            try
            {
                var result = new SummaryBO().getDriverCommision();
                if (result != null)
                    return Ok(result);
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
           
        }
        [HttpGet]
        [Route("DailyPaymentHistory")]
        public IHttpActionResult getDailyPaymentHistory()
        {
            try
            {
                var result = new SummaryBO().DailyPaymentHistory();
                if (result != null)
                    return Ok(result);
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        [HttpGet]
        [Route("DriverPendingAmount")]
        public IHttpActionResult GetDriverPendingAmount()
        {
            try
            {
                var result = new SummaryBO().DailyPaymentHistoryAmount();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
   

    public class RSAObject
    {
        public string Access_code { get; set; }
        public string Order_id { get; set; }
    }
}
