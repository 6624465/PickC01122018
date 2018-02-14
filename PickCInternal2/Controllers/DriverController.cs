using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Threading.Tasks;

using Master.Contract;
using PickC.Services;
using PickC.Services.DTO;
using PickC.Internal2.ViewModals;
using System.IO;
using System.Web.Routing;

namespace PickC.Internal2.Controllers
{
	[WebAuthFilter]
	[PickCEx]
	public class DriverController : BaseController
	{
		[HttpGet]

		public async Task<ActionResult> Driver()
		{
			var driverList = await new DriverService(AUTHTOKEN, p_mobileNo).DriverDetailListPicAsync();
            return View(driverList);
		}

		[HttpGet]
		public async Task<ActionResult> Add(string OperatorId)
		{
			ViewBag.Operators = (await new OperatorService(AUTHTOKEN, p_mobileNo).GetOperatorList()).Select(x => new { Value = x.OperatorID, Text = x.OperatorName }).ToList();
			var driverVm = new DriverVm
			{
				driverLookupDTO = await new DriverService(AUTHTOKEN, p_mobileNo).LookUpDataAsync(),
				driver = new DriverMdl()
			};
			driverVm.driver.DateofIssue = DateTime.Now;
			driverVm.driver.DateofReturn = DateTime.Now;
			ViewBag.PassedOperatorId = OperatorId;
            TempData["operatorId"] = OperatorId;

            return View(driverVm);
		}
		[HttpGet]
		public async Task<ActionResult> Edit(string driverID,string OperatorID)
		{
            TempData["operatorIdEdit"] = OperatorID;
            ViewBag.OperatorId = OperatorID;
            //TempData.Keep("operatorIdEdit");

            var driverVm = await GetDriverInfo(driverID);
            if (Request.IsAjaxRequest())
			{
				return Json(driverVm, JsonRequestBehavior.AllowGet);
			}
			return View(driverVm);

		}

        private async Task<DriverVm> GetDriverInfo(string DriverID)
        {
            
            Task<DriverMdl> taskDriver = new DriverService(AUTHTOKEN, p_mobileNo).DriverByIDInfoAsync(DriverID);
            Task<DriverLookupDTO> taskDriverLookupDTO = new DriverService(AUTHTOKEN, p_mobileNo).LookUpDataAsync();

            await Task.WhenAll(taskDriver, taskDriverLookupDTO);

            var driverVm = new DriverVm
            {
                driverLookupDTO = await taskDriverLookupDTO,
                driver = await taskDriver
            };
            //ViewData["EditOperator"] = driverVm.driver.OperatorId;
            return driverVm;
        }

        /*
        [HttpPost]
        public async Task<ActionResult> BankDetailInfo()
        {
            var driverID = Request.Form["hdnDriverID"];
            var _bankDetailID = Request.Form["hdnDriverBankDetailID"];
            var driverVm = await GetDriverInfo(driverID);

            if(!string.IsNullOrWhiteSpace(_bankDetailID))
            {
                BankDetails bankDetails = null;
                var bankDetailID = Convert.ToInt32(_bankDetailID);
                if(bankDetailID == -1)
                    bankDetails = new BankDetails();
                else

            }
        }
        */
        [HttpGet]
		public async Task<ActionResult> DeleteDriver(string driverID)
		{
			try
			{
				var result = await new DriverService(AUTHTOKEN, p_mobileNo).DeleteDriverAsync(driverID);
				return Json(result, JsonRequestBehavior.AllowGet);
			}
			catch (Exception ex)
			{
				return Json(ex.Message, JsonRequestBehavior.AllowGet);
			}
		}

		[HttpPost]
		public async Task<ActionResult> SaveDriver(DriverMdl driver)
		{
			driver.driverAttachment = new List<DriverAttachment>();
			var lookupId = "";
			foreach (string file in Request.Files)
			{
				var fileContent = Request.Files[file];
				if (fileContent.ContentLength > 0)
				{
					if (file == "fprofilepic")
					{
						lookupId = "1506";
					}
					if (file == "flicense")
					{
						lookupId = "1376";
					}
					if (file == "fpan")
					{
						lookupId = "1374";
					}
					if (file == "fadhar")
					{
						lookupId = "1375";
					}
					if (file == "fvoter")
					{
						lookupId = "1377";
					}
					if (file == "fothers")
					{
						lookupId = "1382";
					}
					string mapPath = Server.MapPath("~/DriverAttachments/");
                    if (!Directory.Exists(mapPath))
                    {
                        Directory.CreateDirectory(mapPath);
                    }
                    fileContent.SaveAs(mapPath + fileContent.FileName);

                    //string path = Server.MapPath("~/PickCApi/DriverImages");
                    //if (!Directory.Exists(path))
                    //{
                    //    Directory.CreateDirectory(path);
                    //}
                    //fileContent.SaveAs(path + fileContent.FileName);


                    DriverAttachment attachment = new DriverAttachment()
					{
						ImagePath = fileContent.FileName,
						LookupCode = lookupId
					};

					driver.driverAttachment.Add(attachment);
				}
			}
			var result = await new DriverService(AUTHTOKEN, p_mobileNo).SaveDriverAsync(driver);


            var operatorId = TempData["operatorId"];
            TempData.Remove("operatorId");
            var editOperatorid = TempData["operatorIdEdit"];
            TempData.Remove("operatorIdEdit");
            var op = (operatorId != null ? operatorId : editOperatorid != null ? editOperatorid : "");


            RouteValueDictionary routeValueDictionary = new System.Web.Routing.RouteValueDictionary();
            routeValueDictionary.Add("operatorID", op);
            if (!string.IsNullOrWhiteSpace(op.ToString()))
            {
                return RedirectToAction("Edit", "Operator", routeValueDictionary);
            }
            return RedirectToAction("Driver", "Driver");
        }
	}
}