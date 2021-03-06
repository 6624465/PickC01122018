﻿using PickCInternal2.HtmlHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace PickCInternal2
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            ModelBinders.Binders.Add(typeof(DateTime), new MyDateTimeModelBinder());
            ModelBinders.Binders.Add(typeof(DateTime?), new MyDateTimeModelBinder());
        }
    }
}
