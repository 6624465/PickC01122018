using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Master.Contract;
using RestSharp;

namespace PickC.Web
{
    public class BaseController : Controller
    {
        public string AUTHTOKEN
        {
            get
            {
                if (ISLOGGEDIN)
                    return System.Web.HttpContext.Current.Session["SSN_TOKEN"].ToString();
                else
                    return "";
            }
        }

        public bool ISLOGGEDIN
        {
            get
            {                
                if (System.Web.HttpContext.Current.Session["SSN_TOKEN"] == null)
                    return false;
                else
                    return true;
            }
        }

        public string p_mobileNo
        {
            get
            {
                if (ISLOGGEDIN)
                {
                    var obj = (Customer)System.Web.HttpContext.Current.Session["SSN_CUSTOMER"];
                    return obj.MobileNo;
                }
                else
                    return "";
            }
        }

        public string p_emailID
        {
            get
            {
                if (ISLOGGEDIN)
                {
                    var obj = (Customer)HttpContext.Session["SSN_CUSTOMER"];
                    return obj.EmailID;
                }
                else
                    return "";
                
            }
        }
    }
}