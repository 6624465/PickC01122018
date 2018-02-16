using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Web.Mvc;

namespace PickC.Internal2
{
    public class PickCEx : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            var Uri = filterContext.HttpContext.Request.UrlReferrer;
            if (Uri.Host.Contains("localhost"))
            {

            }
            else
            {
                Exception ex = filterContext.Exception;
                filterContext.ExceptionHandled = true;
                var model = new HandleErrorInfo(filterContext.Exception, "Controller", "Action");


                filterContext.Result = new RedirectResult("~/Account/Login?error=true");/*
                /*
                filterContext.Result = new ViewResult()
                {
                    ViewName = "Error1",
                    ViewData = new ViewDataDictionary(model)
                };*/
            }
             
        }
    }
}