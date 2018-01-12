using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Web.Mvc;

namespace PickC.Web
{
    public class PickCEx : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            Exception ex = filterContext.Exception;
            filterContext.ExceptionHandled = true;
            var model = new HandleErrorInfo(filterContext.Exception, "Controller", "Action");
            /*
            filterContext.Result = new ViewResult()
            {
                ViewName = "Error1",
                ViewData = new ViewDataDictionary(model)
            };*/
            filterContext.Result = new RedirectResult("~/Account/Login");
        }
    }
}