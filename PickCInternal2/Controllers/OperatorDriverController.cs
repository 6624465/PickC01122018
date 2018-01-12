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
using PickC.Internal2;

namespace PickCInternal2.Controllers
{
    public class OperatorDriverController : BaseController
    {
       [WebAuthFilter]
        [PickCEx]
        // GET: Operator
        [HttpGet]
        public async Task<ActionResult> OperatorDriverList()
        {
            var operatorDriverList = await new OperatorDriverService(AUTHTOKEN, p_mobileNo).OperatorDriverList();
            return View(operatorDriverList);
        }
        [HttpGet]
        public async Task<ActionResult> OperatorDriver()
        {
            ViewBag.Driver = (await new OperatorDriverService(AUTHTOKEN, p_mobileNo).GetDriverList()).Select(x => new { Value = x.DriverID, Text = x.DriverName }).ToList(); 
            ViewBag.VehicleNo = (await new OperatorDriverService(AUTHTOKEN, p_mobileNo).GetvehicleNoList()).Select(x => new { Value = x.VehicleRegistrationNo, Text = x.VehicleRegistrationNo }).ToList();
            return View();
        }
        public async Task<JsonResult> GetDriverDetails(string DriverId)
        {
            var DriverLicenseNoDetails= (await new OperatorDriverService(AUTHTOKEN, p_mobileNo).GetDriverList()).Where(x=>x.DriverID==DriverId).Select(x => new { Value = x.LicenseNo, Text = x.MobileNo }).ToList();
            return Json(DriverLicenseNoDetails,JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public async Task<ActionResult> SaveOperatorDriverList(OperatorDriverDTO operatorDriverDTO)
        {
            operatorDriverDTO.operatorDriverList.CreatedBy = "ADMIN";
            operatorDriverDTO.operatorDriverList.ModifiedBy = "ADMIN";
            var result = (await new OperatorDriverService(AUTHTOKEN, p_mobileNo).SaveOperatorDriverList(operatorDriverDTO.operatorDriverList));
            return RedirectToAction("OperatorDriverList");
        }

    }
}