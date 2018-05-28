using System;
using System.Collections.Generic;
using paytm;
using PickCApi;

public partial class GenerateChecksum : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("MID", PayTMConstants.MID); //Provided by Paytm
            parameters.Add("ORDER_ID", "ORDER000001"); //unique OrderId for every request
            parameters.Add("CUST_ID", "CUST00001"); // unique customer identifier 
            parameters.Add("CHANNEL_ID", PayTMConstants.CHANNEL_ID); //Provided by Paytm
            parameters.Add("INDUSTRY_TYPE_ID", PayTMConstants.INDUSTRY_TYPE_ID); //Provided by Paytm
            parameters.Add("WEBSITE", PayTMConstants.WEBSITE); //Provided by Paytm
            parameters.Add("TXN_AMOUNT", "1.00"); // transaction amount
            parameters.Add("CALLBACK_URL", PayTMConstants.CALLBACK_URL); //Provided by Paytm
            parameters.Add("EMAIL", "ragsarma@gmail.com"); // customer email id
            parameters.Add("MOBILE_NO", "9999999999"); // customer 10 digit mobile no.
            string paytmChecksum = "";

            foreach (string key in parameters.Keys)
            {
                if (parameters[key].ToUpper().Contains("REFUND") || parameters[key].ToUpper().Contains("|"))
                {
                    parameters[key] = "";
                }
            }

            paytmChecksum = CheckSum.generateCheckSum(PayTMConstants.MERCHANT_KEY, parameters);
            Response.Write(paytmChecksum);

        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
    }
}
