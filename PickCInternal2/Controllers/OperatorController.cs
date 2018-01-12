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
using PickC.Internal2;
using System.IO;

namespace PickCInternal2.Controllers
{

    [WebAuthFilter]
    [PickCEx]
    public class OperatorController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult> Operator()
        {
            var operatorList = await new OperatorService(AUTHTOKEN, p_mobileNo).OperatorsListAsync();
            return View(operatorList);
        }
        [HttpGet]
        public async Task<ActionResult> OperatorDetails()
         {
            var operatorVm = new OperatorVm
            {
                operatorLookupDTO = await new OperatorService(AUTHTOKEN, p_mobileNo).LookUpDataAsync(),
             OPerator = new Operator()
            };
            return View(operatorVm);
        }
        [HttpPost]
        public async Task<ActionResult> SaveOperator(Operator OPerator)
        {
             OPerator.operatorAttachment = new List<OperatorAttachment>();
            var lookupId = "";
            foreach (string file in Request.Files)
            {
                var fileContent = Request.Files[file];
                if (fileContent.ContentLength > 0)
                {
                    if (file == "fadhar")
                    {
                        lookupId = "1375";
                    }
                    if (file == "fpan")
                    {
                        lookupId = "1374";
                    }
                    if (file == "flicense")
                    {
                        lookupId = "1376";
                    }
                    if (file == "fvoter")
                    {
                        lookupId = "1377";
                    }
                    if (file == "fothers")
                    {
                        lookupId = "1382";
                    }
                    string mapPath = Server.MapPath("~/Attachments/");
                    if (!Directory.Exists(mapPath))
                    {
                        Directory.CreateDirectory(mapPath);
                    }
                    fileContent.SaveAs(mapPath + fileContent.FileName);

                    OperatorAttachment attachment = new OperatorAttachment()
                    {
                        imagePath = fileContent.FileName,
                        lookupCode = lookupId
                    };

                    OPerator.operatorAttachment.Add(attachment);
                }
            }
           
            //foreach (string file in Request.Files)
            //{
            //    var fileContent = Request.Files[file];
            //    if(file== "fadhar")
            //    {
            //         lookupId = "1375";
            //    }
            //    if(file == "fpan") 
            //    {
            //        lookupId = "1374";
            //    }
            //    if(file == "flicense")
            //    {
            //        lookupId = "1376";
            //    }
            //    if(file == "fvoter")
            //    {
            //        lookupId = "1377";
            //    }
            //    if(file== "fothers")
            //    {
            //        lookupId = "1382";
            //    }
            //    string mapPath = Server.MapPath("~/Attachments/");
            //    if (!Directory.Exists(mapPath))
            //    {
            //        Directory.CreateDirectory(mapPath);
            //    }
            //    fileContent.SaveAs(mapPath + fileContent.FileName);

            //    OperatorAttachment attachment = new OperatorAttachment()
            //    {
            //        imagePath = fileContent.FileName,
            //        lookupCode = lookupId
            //    };

            //    attachmentsList.Add(attachment);
            //}
            // OPerator.operatorAttachment = attachmentsList;
            //Operator o = new Operator();
            //o.OperatorVehicle = new List<OperatorVehicle>();
            //OPerator.OperatorVehicle.ForEach(x =>
            //{
            //    x.VehicleType =
            //});
            var result = await new OperatorService(AUTHTOKEN, p_mobileNo).SaveOperatorAsync(OPerator);
            return RedirectToAction("Operator", "Operator");
        }
        [HttpGet]
        public  async Task<ActionResult> Add()
        {
            ViewBag.Driver = (await new OperatorDriverService(AUTHTOKEN, p_mobileNo).GetDriverList()).Select(x => new { Value = x.DriverID, Text = x.DriverName }).ToList();
            ViewBag.VehicleNo = (await new OperatorDriverService(AUTHTOKEN, p_mobileNo).GetvehicleNoList()).Select(x => new { Value = x.VehicleRegistrationNo, Text = x.VehicleRegistrationNo }).ToList();
            ViewBag.VehicleType = (await new OperatorVehicleService(AUTHTOKEN, p_mobileNo).GetOperatorVehicleList()).Select(x => new { Value = x.LookupId, Text = x.LookupCode }).ToList();
            ViewBag.VehicleCategory = (await new OperatorVehicleService(AUTHTOKEN, p_mobileNo).GetOperatorVehicleCategoryList()).Select(x => new { Value = x.LookupId, Text = x.LookupCode }).ToList();
            //ViewBag.Model = (await new OperatorVehicleService(AUTHTOKEN, p_mobileNo).GetOperatorModelList()).Select(x => new { Value = x.Model, Text = x.Model }).ToList();
            var operatorVm = new OperatorVm
            {
                operatorLookupDTO = await new OperatorService(AUTHTOKEN, p_mobileNo).LookUpDataAsync(),
                OPerator = new Operator()
            };

            return View("OperatorDetails", operatorVm);
        }
        [HttpGet]
        public async Task<ActionResult> Edit(string operatorID)

        {
            Task<Operator> taskOperator = new OperatorService(AUTHTOKEN, p_mobileNo).OperatorInfoAsync(operatorID);
            Task<OperatorLookupDTO> taskoperatorLookupDTO = new OperatorService(AUTHTOKEN, p_mobileNo).LookUpDataAsync();
            ViewBag.Driver = (await new OperatorDriverService(AUTHTOKEN, p_mobileNo).GetDriverList()).Where(x=>x.OperatorID == operatorID).Select(x => new { Value = x.DriverID, Text = x.DriverName }).ToList();
            ViewBag.VehicleNo = (await new OperatorDriverService(AUTHTOKEN, p_mobileNo).GetvehicleNoList(operatorID)).Select(x => new { Value = x.VehicleRegistrationNo, Text = x.VehicleRegistrationNo }).ToList();
            ViewBag.VehicleType = (await new OperatorVehicleService(AUTHTOKEN, p_mobileNo).GetOperatorVehicleList()).Select(x => new { Value = x.LookupId, Text = x.LookupCode }).ToList();
            ViewBag.VehicleCategory = (await new OperatorVehicleService(AUTHTOKEN, p_mobileNo).GetOperatorVehicleCategoryList()).Select(x => new { Value = x.LookupId, Text = x.LookupCode }).ToList();
            //ViewBag.Model = (await new OperatorVehicleService(AUTHTOKEN, p_mobileNo).GetOperatorModelList()).Select(x => new { Value = x.Model, Text = x.Model }).ToList();
            await Task.WhenAll(taskOperator, taskoperatorLookupDTO);

            var operatorVm = new OperatorVm
            {
                operatorLookupDTO = await taskoperatorLookupDTO,
                OPerator = await taskOperator
            };

            return View("OperatorDetails", operatorVm);
        }

        public async Task<JsonResult> GetAttachmentData()
        {
            var operatorLookupDTO = await new OperatorService(AUTHTOKEN, p_mobileNo).LookUpDataAsync();
            return Json(operatorLookupDTO, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public async Task<JsonResult> GetTonnage(string Tonnage)
        {
            var tonnage = (await new OperatorVehicleService(AUTHTOKEN, p_mobileNo).GetOperatorModelList()).Where(x => x.Model == Tonnage).Select(x => new { Value = x.Tonnage, Text = x.Tonnage }).ToList();
            return Json(tonnage, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public async Task<JsonResult> GetCategoryModel(int categoryValue)
         {
            var model1 = (await new OperatorVehicleService(AUTHTOKEN, p_mobileNo).GetOperatorModelList());
            var model=model1.Where(x => x.VehicleGroup == categoryValue).Select(m => new { Value = m.Model, Text = m.Model }).ToList();
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public async Task<JsonResult> GetVehicleNo(string VehicleNo)
        {
            var vehicleNo = (await new OperatorDriverService(AUTHTOKEN, p_mobileNo).GetvehicleNoList()).Where(x => x.VehicleRegistrationNo == VehicleNo).Count();
            if (vehicleNo > 0)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
        }
        public async Task<JsonResult> GetDriverDetails(string DriverId)
        {
            var DriverLicenseNoDetails = (await new OperatorDriverService(AUTHTOKEN, p_mobileNo).GetDriverList()).Where(x => x.DriverID == DriverId).Select(x => new { Value = x.LicenseNo, Text = x.MobileNo }).ToList();
            return Json(DriverLicenseNoDetails, JsonRequestBehavior.AllowGet);
        }
        public async Task<JsonResult> AddAttachment()
        {
            var result = "";

            try
            {
                foreach (string file in Request.Files)
                {
                    var fileContent = Request.Files[file];
                    var operatorId = Request.Form[0];
                    var lookupId = Request.Form[1];
                    string mapPath = Server.MapPath("~/Attachments/");
                    if (!Directory.Exists(mapPath))
                    {
                        Directory.CreateDirectory(mapPath);
                    }
                    fileContent.SaveAs(mapPath + fileContent.FileName);

                    OperatorAttachment atttachment = new OperatorAttachment()
                    {
                        //attachmentId = operatorId + lookupId,
                        //operatorId = operatorId,
                        imagePath = fileContent.FileName,
                        lookupCode = lookupId
                    };


                    result = await new OperatorService(AUTHTOKEN, p_mobileNo).SaveOperatorAttachmentAsync(atttachment);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}