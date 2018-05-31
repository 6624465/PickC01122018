using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PickCApi
{
    public partial class PayTMTransactionStatus : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            try
            {

            
            string value = "https://securegw-stage.paytm.in/merchant-status/getTxnStatus?JsonData=";

            Dictionary<string, string> innerrequest = new Dictionary<string, string>();
            Dictionary<string, string> outerrequest = new Dictionary<string, string>();



            innerrequest.Add("MID", "PRTECH81813676074829");

            innerrequest.Add("ORDERID", "ORDER712750351");


            String first_jason = new JavaScriptSerializer().Serialize(innerrequest);


            first_jason = first_jason.Replace("\\", "").Replace(":\"{", ":{").Replace("}\",", "},");

            string Check = paytm.CheckSum.generateCheckSum("7PeD5JQt7od2qf7V", innerrequest);
            string correct_check = Server.UrlEncode(Check);

            innerrequest.Add("CHECKSUMHASH", correct_check);


            String final = new JavaScriptSerializer().Serialize(innerrequest);
            final = final.Replace("\\", "").Replace(":\"{", ":{").Replace("}\",", "},");

            String url = value + final;

            WebRequest request = WebRequest.Create(url);



            request.Headers.Add("ContentType", "application/json");
            request.Method = "POST";

            using (StreamWriter requestWriter2 = new StreamWriter(request.GetRequestStream()))
            {
                requestWriter2.Write(final);

            }

            string responseData = string.Empty;



            using (StreamReader responseReader = new StreamReader(request.GetResponse().GetResponseStream()))
            {
                responseData = responseReader.ReadToEnd();

                Response.Write(responseData);
                Response.Write("Requested Json= " + final);

            }
            }
            catch (Exception ex)
            {

                Response.Write(ex.Message.ToString());   
            }
        }
    }
}