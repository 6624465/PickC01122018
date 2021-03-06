﻿using System;
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
using Newtonsoft.Json;

namespace PickCInternal2.Controllers
{

    [WebAuthFilter]
    [PickCEx]
    public class OperatorController : BaseController
    {
        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public async Task<ActionResult> Operator()
        {
            if (Request.HttpMethod == "POST")
            {
                string OperatorID = "";
                OperatorID = Request.Form["OperatorID"];
                if (!string.IsNullOrWhiteSpace(OperatorID))
                {
                    List<OperatorDriver> OperatorDriverList = (await new OperatorDriverService(AUTHTOKEN, p_mobileNo).GetDriverList()).Where(x => x.OperatorID == OperatorID).ToList();

                    ViewData["VD:DriverList"] = OperatorDriverList;
                }
            }
            var operatorList = await new OperatorService(AUTHTOKEN, p_mobileNo).OperatorsWithProfileListAsync();
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
                    if (file == "fprofilepic")
                    {
                        lookupId = "1506";
                    }

                    OperatorAttachment attachment = new OperatorAttachment()
                    {
                        imagePath = fileContent.FileName,
                        lookupCode = lookupId
                    };

                    OPerator.operatorAttachment.Add(attachment);
                }
            }

            var OperatorIDObj = await new OperatorService(AUTHTOKEN, p_mobileNo).SaveOperatorAsync(OPerator);

            var anonymous = new { OperatorId =""};
            
            
            var json = JsonConvert.DeserializeAnonymousType(OperatorIDObj, anonymous);
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
                    if (file == "fprofilepic")
                    {
                        lookupId = "1506";
                    }
                    string mapPath = Server.MapPath($"~/Attachments/{json.OperatorId}/");
                    if (!Directory.Exists(mapPath))
                    {
                        Directory.CreateDirectory(mapPath);
                    }
                    ViewData["path"] = mapPath;
                    fileContent.SaveAs(mapPath + fileContent.FileName);                    
                }
            }
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

        [HttpGet]
        public async Task<JsonResult> IsAadharExists(string aadharno)
        {
            var operatorList = await new OperatorService(AUTHTOKEN, p_mobileNo).IsAadharExistsAsync(aadharno);
            return Json(operatorList, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<JsonResult> IsPannoExists(string panno)
        {
            var operatorList = await new OperatorService(AUTHTOKEN, p_mobileNo).IsPancardExistsAsync(panno);
            return Json(operatorList, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> IsMobilenoExists(string mobilenono)
        {
            var operatorList = await new OperatorService(AUTHTOKEN, p_mobileNo).IsMobilenoExistsAsync(mobilenono);
            return Json(operatorList, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> IsAccountnoExists(string accountno)
        {
            var operatorList = await new OperatorService(AUTHTOKEN, p_mobileNo).IsAccountNoExistsAsync(accountno);
            return Json(operatorList, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public async Task<ActionResult> GetOperatorDriverList(string operatorID)
        {
            List<OperatorDriver> OperatorDriverList = (await new OperatorDriverService(AUTHTOKEN, p_mobileNo).GetDriverList()).Where(x => x.OperatorID == operatorID).ToList();
            return View("OperatorDetails", OperatorDriverList);
        }

        [HttpPost]
        public async Task<ActionResult> SaveVehicle(OperatorVehicle operatorVehicle)
        {
            foreach(string file in Request.Files)
            {
                if(Request.Files[file] != null && Request.Files[file].ContentLength > 0)
                {
                    var vehicleAttachmentsPath = Server.MapPath("~/VehicleAttachments/" + operatorVehicle.OperatorVehicleID + "/" + operatorVehicle.VehicleRegistrationNo + "/");
                    System.IO.DirectoryInfo dirInfo = new DirectoryInfo(vehicleAttachmentsPath);

                    if (!dirInfo.Exists)
                        dirInfo.Create();

                    HttpPostedFileBase _file = Request.Files[file];
                    var fileName = Path.GetFileName(_file.FileName);
                    var path = Path.Combine(vehicleAttachmentsPath, fileName);
                    _file.SaveAs(path);

                    /*
                    if(file == "registrationdoc")                    
                        operatorVehicle.registrationdoc = _file.FileName;                    
                    else if(file == "insurancedoc")
                        operatorVehicle.insurancedoc = _file.FileName;
                    else if (file == "pollutioncheckdoc")
                        operatorVehicle.pollutioncheckdoc = _file.FileName;
                    else if (file == "othersdoc")
                        operatorVehicle.othersdoc = _file.FileName;
                    */
                }
            }

            if (!string.IsNullOrWhiteSpace(Request.Form["hdnRegistrationdocFile"]))
                operatorVehicle.registrationdoc = Request.Form["hdnRegistrationdocFile"];
            else
                operatorVehicle.registrationdoc = null;

            if (!string.IsNullOrWhiteSpace(Request.Form["hdnInsurancedocFile"]))
                operatorVehicle.insurancedoc = Request.Form["hdnInsurancedocFile"];
            else
                operatorVehicle.insurancedoc = null;

            if (!string.IsNullOrWhiteSpace(Request.Form["hdnPollutioncheckdocFile"]))
                operatorVehicle.pollutioncheckdoc = Request.Form["hdnPollutioncheckdocFile"];
            else
                operatorVehicle.pollutioncheckdoc = null;

            if (!string.IsNullOrWhiteSpace(Request.Form["hdnOthersdocFile"]))
                operatorVehicle.othersdoc = Request.Form["hdnOthersdocFile"];
            else
                operatorVehicle.othersdoc = null;


            if (!string.IsNullOrWhiteSpace(Request.Form["hdnfrontimagedocFile"]))
                operatorVehicle.FrontImage = Request.Form["hdnfrontimagedocFile"];
            else
                operatorVehicle.FrontImage = null;

            if (!string.IsNullOrWhiteSpace(Request.Form["hdnbackimagedocFile"]))
                operatorVehicle.BackImage = Request.Form["hdnbackimagedocFile"];
            else
                operatorVehicle.BackImage = null;

            if (!string.IsNullOrWhiteSpace(Request.Form["hdnleftimagedocFile"]))
                operatorVehicle.LeftImage = Request.Form["hdnleftimagedocFile"];
            else
                operatorVehicle.LeftImage = null;

            if (!string.IsNullOrWhiteSpace(Request.Form["hdnrightimagedocFile"]))
                operatorVehicle.RightImage = Request.Form["hdnrightimagedocFile"];
            else
                operatorVehicle.RightImage = null;





            var result = await new OperatorVehicleService(AUTHTOKEN, p_mobileNo).SaveOperatorVehicleList(operatorVehicle);
            return RedirectToAction("Edit", new { operatorID = operatorVehicle.OperatorVehicleID });
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
                    ViewData["path"] = mapPath;
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