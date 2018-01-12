using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;

using PickC.Services;
using PickC.Internal.ViewModals;
using PickC.Services.DTO;
using Operation.Contract;
using Operation.BusinessFactory;

namespace PickC.Internal.Areas.Internal.Controllers
{
    public class OperatorVehicleListController : BaseController
    {
        [WebAuthFilter]
        [PickCEx]
        // GET: Operator
        [HttpGet]
        [ActionName("Index")]
        public async Task<ActionResult> OperatorVehicleList()
        {
            ViewBag.VehicleType = await new OperatorVehicleService(AUTHTOKEN, p_mobileNo).GetOperatorVehicleList();//.where(x => new { Value=x.LookupCode,Text=x.LookupDescription });
            //ViewBag.Model = new OperatorVehicleService(AUTHTOKEN, p_mobileNo).GetOperatorModelList();//.Select(x => new { Value = x.LookupCode, Text = x.LookupDescription });
            //ViewBag.Tonnage = new OperatorVehicleService(AUTHTOKEN, p_mobileNo).GetTonnageTonnageList();//.Select(x => new { Value = x.LookupCode, Text = x.LookupDescription });
            return View();
        }
    }
}