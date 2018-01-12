using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

using Operation.Contract;
using Operation.BusinessFactory;

namespace PickCApi.Core
{
    public class OperatorAPIAuthFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            var AUTH_TOKEN = HttpContext.Current.Request.Headers["AUTH_TOKEN"];
            var MOBILENO = HttpContext.Current.Request.Headers["MOBILENO"];
            if (!string.IsNullOrWhiteSpace(AUTH_TOKEN) && !string.IsNullOrWhiteSpace(MOBILENO))
            {

                var result = new OperatorLogInBO().AuthUser(
                    new OperatorLogIn { TokenNo = AUTH_TOKEN, MobileNo = MOBILENO });

                if (!result)
                {
                    actionContext.Response = new HttpResponseMessage
                    {
                        Content = new StringContent(UTILITY.INVALID),
                        StatusCode = HttpStatusCode.Unauthorized
                    };
                }

            }
            else
            {
                actionContext.Response = new HttpResponseMessage
                {
                    Content = new StringContent(UTILITY.FAILEDAUTH),
                    StatusCode = HttpStatusCode.Unauthorized
                };
            }
        }
    }
}