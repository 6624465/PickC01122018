using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Master.Contract;
using RestSharp;

namespace PickC.Internal
{
    public class BaseController : Controller
    {
        public string AUTHTOKEN
        {
            get
            {
                return System.Web.HttpContext.Current.Session["SSN_TOKEN"].ToString();
            }
        }

        public string p_mobileNo
        {
            get
            {
                var obj = (Customer)System.Web.HttpContext.Current.Session["SSN_CUSTOMER"];
                return obj.MobileNo;
            }
        }

        public string p_emailID
        {
            get
            {
                var obj = (Customer)HttpContext.Session["SSN_CUSTOMER"];
                return obj.EmailID;
            }
        }
    }
}