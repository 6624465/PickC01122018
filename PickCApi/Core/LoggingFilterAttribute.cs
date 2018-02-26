using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Web.Http.Tracing;

using Newtonsoft.Json;

namespace PickCApi.Core
{
    public class LoggingFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(HttpActionExecutedContext context)
        {
            GlobalConfiguration.Configuration.Services.Replace(typeof(ITraceWriter), new NLogger());

            var objectContent = context.Response.Content as ObjectContent;
            var responseStr = string.Empty;
            if (objectContent != null && objectContent.Value != null)
            {
                //var type = objectContent.ObjectType; //type of the returned object
                //var value = objectContent.Value; //holding the returned value
                responseStr = JsonConvert.SerializeObject(objectContent.Value);
            }

            var actionParametersStr = string.Empty;
            var DicActionParameters = context.ActionContext.ActionArguments;
            if(DicActionParameters.Count > 0)
            {
                actionParametersStr = JsonConvert.SerializeObject(DicActionParameters);
            }

            var ReqobjectContent = context.Request.Content as ObjectContent;
            var reqStr = string.Empty;
            if (ReqobjectContent != null && ReqobjectContent.Value != null)
                reqStr = JsonConvert.SerializeObject(ReqobjectContent.Value);

            var trace = GlobalConfiguration.Configuration.Services.GetTraceWriter();
            trace.Info(
                context.Request,
                "Controller : " + context.ActionContext.ControllerContext.ControllerDescriptor.ControllerType.FullName + Environment.NewLine +
                "Action : " + context.ActionContext.ActionDescriptor.ActionName + Environment.NewLine +
                "Action Parameters : " + actionParametersStr + Environment.NewLine + 
                "Header : " + context.Request.Headers.ToString() +
                "Response : " + responseStr + Environment.NewLine,
                "JSON",
                reqStr);
        }

        //public override void OnActionExecuting(HttpActionContext filterContext)
        //{
        //    GlobalConfiguration.Configuration.Services.Replace(typeof(ITraceWriter), new NLogger());
        //    var trace = GlobalConfiguration.Configuration.Services.GetTraceWriter();
        //    trace.Info(
        //        filterContext.Request,
        //        "Controller : " + filterContext.ControllerContext.ControllerDescriptor.ControllerType.FullName + Environment.NewLine +
        //        "Action : " + filterContext.ActionDescriptor.ActionName + Environment.NewLine +
        //        "Header : " + filterContext.Request.Headers.ToString() + Environment.NewLine,
        //        "JSON",
        //        filterContext.ActionArguments);
        //}
    }
}