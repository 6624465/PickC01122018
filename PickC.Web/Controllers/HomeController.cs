using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;

using Master.Contract;
using PickC.Web.ViewModels;
using PickC.Services;
using System.Net;

namespace PickC.Web.Controllers
{
    public class HomeController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            var tripEstimateVm = new TripEstimateVm();

            var task1 = new VehicleGroupService(AUTHTOKEN, p_mobileNo).GetDataAsync();
            var task2 = new VehicleTypeService(AUTHTOKEN, p_mobileNo).GetDataAsync();

            var result = await Task.WhenAll(task1, task2);

            tripEstimateVm.VehicleGrouplookUpData = result[0];
            tripEstimateVm.VehicleTypelookUpData = result[1];
            if (ISLOGGEDIN)
                tripEstimateVm.customer = (Customer)HttpContext.Session["SSN_CUSTOMER"];
            else
                tripEstimateVm.customer = new Customer();

            ViewBag.isUserLoggedIn = ISLOGGEDIN;            

            return View(tripEstimateVm);
            
        }
        [HttpGet]
        public  ActionResult ImageRegisterNow()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> GetFile(DriverImageRegister driverImageRegister)
        {
            //DownloadPDF();
            var result = await new CustomerService().SaveImageRegisterAsync(driverImageRegister);
            return Json(result,JsonRequestBehavior.AllowGet);
             }

        public void DownloadPDF()
        {
            Response.Clear();
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=" + "ImageRegisterNow" + ".pdf");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.TransmitFile(Server.MapPath("~/images/RegisterNow.pdf"));
            Response.End();    
        }
    }
}