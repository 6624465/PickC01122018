using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PickC.Web.Utilities;
namespace PickC.Web.Controllers
{
    public class DefaultController : BaseController
    {

        public ActionResult menu(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return View("menu");
            else
                return View(name);
        }
        [HttpGet]
        public JsonResult SendSMS(string MobNo)
        {
            bool flag = new smsGenerator().ConfigSms(MobNo, "Please click the below link to download the PICK-C app.");
            return base.Json(flag, JsonRequestBehavior.AllowGet);
        }
    }
}