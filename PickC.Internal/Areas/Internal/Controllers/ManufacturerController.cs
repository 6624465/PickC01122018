using PickC.Services;
using PickC.Services.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace PickC.Internal.Areas.Internal.Controllers
{
    public class ManufacturerController : BaseController
    {
        // GET: Internal/Manufacturer
        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Add(DriverManufacturerDTO manufactur)
        {
            var result = await new ManufacturerService(AUTHTOKEN, p_mobileNo).Save(manufactur);
            return View("Add");
        }
    }
}