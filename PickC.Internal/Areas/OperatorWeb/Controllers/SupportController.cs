using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PickC.Internal.Areas.OperatorWeb.Controllers
{
    public class SupportController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult faq()
        {
            return View();
        }

        [HttpGet]
        public ActionResult addshift()
        {
            return View();
        }
    }
}