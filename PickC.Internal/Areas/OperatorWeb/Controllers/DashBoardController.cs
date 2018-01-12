using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PickC.Internal.Areas.OperatorWeb.Controllers
{
    public class DashBoardController : Controller
    {        
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
    }
}