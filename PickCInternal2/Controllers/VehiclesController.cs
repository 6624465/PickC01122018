using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using PickC.Services;

namespace PickCInternal2.Controllers
{
    public class VehiclesController : Controller
    {
        // GET: Vehicles
        public ActionResult Vehicle()
        {
            return View();
        }
        public ActionResult Add()
        {
            return View();
        }
        public async Task<ActionResult> SaveVehicleModel()
        {
            //  var result = await new VehicleGroupService(AUTHTOKEN, p_mobileNo).SaveVehicleConfig();
            return View();
        }
    }
}