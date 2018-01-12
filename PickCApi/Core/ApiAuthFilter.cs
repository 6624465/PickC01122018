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
    public class ApiAuthFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            var TYPE = HttpContext.Current.Request.Headers["TYPE"];
            if (string.IsNullOrWhiteSpace(TYPE))
            {
                var AUTH_TOKEN = HttpContext.Current.Request.Headers["AUTH_TOKEN"];
                var MOBILENO = HttpContext.Current.Request.Headers["MOBILENO"];
                if (!string.IsNullOrWhiteSpace(AUTH_TOKEN) && !string.IsNullOrWhiteSpace(MOBILENO))
                {

                    var result = new CustomerLogInBO().AuthUser(
                        new CustomerLogin { TokenNo = AUTH_TOKEN, MobileNo = MOBILENO });

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
            else if(TYPE == "DRIVER")
            {
                var AUTH_TOKEN = HttpContext.Current.Request.Headers["AUTH_TOKEN"];
                var DRIVERID = HttpContext.Current.Request.Headers["DRIVERID"];
                var LATITUDE = HttpContext.Current.Request.Headers["LATITUDE"];
                var LONGITUDE = HttpContext.Current.Request.Headers["LONGITUDE"];

                if (!string.IsNullOrWhiteSpace(AUTH_TOKEN) && !string.IsNullOrWhiteSpace(DRIVERID))
                {
                    var result = new DriverActivityBO().AuthenticateDriver(new DriverActivity {
                        TokenNo = AUTH_TOKEN,
                        DriverId = DRIVERID,
                        Latitude = Convert.ToDecimal(LATITUDE),
                        Longitude = Convert.ToDecimal(LONGITUDE)
                    });

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

        /*
        public bool AuthUser(CustomerLogin item)
        {
            return customerloginDAL.AuthUser(item);
        }

        public bool AuthUser<T>(T item, DbTransaction parentTransaction) where T : IContract
        {
            currentTransaction = parentTransaction;
            return true;
        }

        public bool AuthUser<T>(T item) where T : IContract
        {
            var result = 0;
            var customerLogin = (CustomerLogin)(object)item;

            if(currentTransaction == null)
            {
                connection = db.CreateConnection();
                connection.Open();
            }

            var transaction = (currentTransaction == null ? connection.BeginTransaction() : currentTransaction);

            try
            {
                var authcmd = db.GetStoredProcCommand(DBRoutine.AUTHENTICATEUSER);

                db.AddInParameter(authcmd, "TokenNo", System.Data.DbType.String, customerLogin.TokenNo);
                db.AddInParameter(authcmd, "mobileNo", System.Data.DbType.String, customerLogin.MobileNo);

                result = Convert.ToInt32(db.ExecuteScalar(authcmd, transaction));

                if (currentTransaction == null)
                    transaction.Commit();
            }
            catch (Exception)
            {
                if (currentTransaction == null)
                    transaction.Rollback();

                throw;
            }

            return (result > 0 ? true : false);
        }

        */
    }
}