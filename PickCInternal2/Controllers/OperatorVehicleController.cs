using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;

using PickC.Services;
using PickC.Internal2.ViewModals;
using PickC.Services.DTO;
using Operation.Contract;
using Operation.BusinessFactory;
using Master.Contract;
namespace PickC.Internal2.Controllers
{
    public class OperatorVehicleController : BaseController
    {
        [WebAuthFilter]
        [PickCEx]
        // GET: Operator
        [HttpGet]
        //[ActionName("Index")]
        public async Task<ActionResult> OperatorVehicle()
        {
            ViewBag.VehicleCategory = (await new OperatorVehicleService(AUTHTOKEN, p_mobileNo).GetOperatorVehicleCategoryList()).Select(x => new { Value = x.LookupId, Text = x.LookupCode }).ToList();
            ViewBag.VehicleType = (await new OperatorVehicleService(AUTHTOKEN, p_mobileNo).GetOperatorVehicleList()).Select(x => new { Value = x.LookupId, Text = x.LookupCode }).ToList();
            ViewBag.Model = (await new OperatorVehicleService(AUTHTOKEN, p_mobileNo).GetOperatorModelList()).Select(x => new { Value = x.Model, Text = x.Model }).ToList();
            return View("OperatorVehicle");
        }
        [HttpGet]
        public async  Task<JsonResult> GetTonnage(string Tonnage)
        {
            var tonnage = (await new OperatorVehicleService(AUTHTOKEN, p_mobileNo).GetOperatorModelList()).Where(x=>x.Model==Tonnage).Select(x=> new {Value=x.Tonnage,Text=x.Tonnage}).ToList();
            return Json(tonnage,JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public async Task<ActionResult> SaveOperator(OperatorVehicleDTO operatorVehicleDTO)
        {
            operatorVehicleDTO.operatorVehicle.CreatedBy = "JOHN";
            var result = (await new OperatorVehicleService(AUTHTOKEN, p_mobileNo).SaveOperatorVehicleList(operatorVehicleDTO.operatorVehicle));
            return RedirectToAction("OperatorVehicleList");
        }
        [ActionName("OperatorVehicleList")]
        public async Task<ActionResult> OperatorVehicleList()
        {
            var operatorVehicleList = await new OperatorVehicleService(AUTHTOKEN, p_mobileNo).OperatorVehicleList();
            return View(operatorVehicleList);
        }
    }
}