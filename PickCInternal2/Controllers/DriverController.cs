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

namespace PickC.Internal2.Controllers
{
	[WebAuthFilter]
	[PickCEx]
	public class DriverController : BaseController
	{
		[HttpGet]

		public async Task<ActionResult> Driver()
		{
			var driverList = await new DriverService(AUTHTOKEN, p_mobileNo).DriverDetailListAsync();
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
			return View(driverVm);
		}
		[HttpGet]
		public async Task<ActionResult> Edit(string driverID)
		{
			//Task<Driver> taskDriver = new DriverService(AUTHTOKEN, p_mobileNo).DriverInfoAsync(driverID);
			Task<DriverMdl> taskDriver = new DriverService(AUTHTOKEN, p_mobileNo).DriverByIDInfoAsync(driverID);
			Task<DriverLookupDTO> taskDriverLookupDTO = new DriverService(AUTHTOKEN, p_mobileNo).LookUpDataAsync();

			await Task.WhenAll(taskDriver, taskDriverLookupDTO);

			var driverVm = new DriverVm
			{
				driverLookupDTO = await taskDriverLookupDTO,
				driver = await taskDriver
			};
			driverVm.driver.DateofIssue = DateTime.Now;
			driverVm.driver.DateofReturn = DateTime.Now;

			if (Request.IsAjaxRequest())
			{
				return Json(driverVm, JsonRequestBehavior.AllowGet);
			}
			return View(driverVm);

		}
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
					if (file == "fphoto")
					{
						lookupId = "1385";
					}
					if (file == "flicense")
					{
						lookupId = "1386";
					}
					if (file == "fpan")
					{
						lookupId = "1387";
					}
					if (file == "fadhaar")
					{
						lookupId = "1388";
					}
					if (file == "faddressProof")
					{
						lookupId = "1389";
					}
					if (file == "fothers")
					{
						lookupId = "1390";
					}
					string mapPath = Server.MapPath("~/DriverAttachments/");
					if (!Directory.Exists(mapPath))
					{
						Directory.CreateDirectory(mapPath);
					}
					fileContent.SaveAs(mapPath + fileContent.FileName);

					DriverAttachment attachment = new DriverAttachment()
					{
						ImagePath = fileContent.FileName,
						LookupCode = lookupId
					};

					driver.driverAttachment.Add(attachment);
				}
			}
			var result = await new DriverService(AUTHTOKEN, p_mobileNo).SaveDriverAsync(driver);
			return RedirectToAction("Driver", "Driver");
		}
	}
}