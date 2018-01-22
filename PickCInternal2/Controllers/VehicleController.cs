using PickC.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using PickC.Internal2;
using Master.Contract;

namespace PickCInternal2.Controllers
{
    [WebAuthFilter]
    [PickCEx]
    public class VehicleController : BaseController
    {
        // GET: Vehicle
        public async Task<ActionResult> Vehicle()
        {
            ViewBag.VehicleCategory = (await new OperatorVehicleService(AUTHTOKEN, p_mobileNo).GetOperatorVehicleCategoryList()).Select(x => new { Value = x.LookupId, Text = x.LookupCode }).ToList();
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> saveVehicleConfig(VehicleConfig con)
        {
            var result = await new VehicleGroupService(AUTHTOKEN, p_mobileNo).SaveVehicleConfig(con);
            return View();

        }
        //public ActionResult getVehicleModelList()
        //{
        //    var result = n
        //}
    }
}