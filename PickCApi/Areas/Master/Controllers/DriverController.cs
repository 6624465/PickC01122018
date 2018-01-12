using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using Master.Contract;
using Master.BusinessFactory;
using PickCApi.Core;
using PickCApi.Areas.Master.DTO;
using PickC.Services.DTO;


namespace PickCApi.Areas.Master.Controllers
{
    [RoutePrefix("api/master/driver")]
    [ApiAuthFilter]
    public class DriverController : ApiBase
    {
        [HttpGet]
        [Route("list")]
        public IHttpActionResult DriverList()
        {
            try
            {
                var driverList = new DriverBO().GetList();
                if (driverList != null)
                    return Ok(driverList);
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        [HttpGet]
        [Route("DriverDetailList")]
        public IHttpActionResult DriverDetailList()
        {
            try
            {
                var driverList = new DriverBO().GetDriversDetailList();
                if (driverList != null)
                    return Ok(driverList);
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        [HttpPost]
        [Route("save")]
        public IHttpActionResult SaveDriver(Driver driver)
        {
            try
            {
                driver.CreatedBy = UTILITY.DEFAULTUSER;
                driver.ModifiedBy = UTILITY.DEFAULTUSER;
                driver.CreatedOn = DateTime.Now;
                driver.ModifiedOn = DateTime.Now;

                if (driver.AddressList!=null && driver.AddressList.Count > 0)
                {
                    driver.AddressList.ForEach(x =>
                    {
                        x.AddressLinkId = driver.DriverId;
                        x.AddressType = "DRIVER";
                        x.CreatedBy = UTILITY.DEFAULTUSER;
                        x.CreatedOn = DateTime.Now;
                        x.ModifiedBy = UTILITY.DEFAULTUSER;
                        x.ModifiedOn = DateTime.Now;
                        x.IsActive = true;
                    });
                }
                var result = new DriverBO().SaveDriver(driver);
                if (result)
                    return Ok(UTILITY.SUCCESSMSG);
                else
                    return BadRequest();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpDelete]
        [Route("{driverID}")]
        public IHttpActionResult DeleteDriver(string driverID)
        {
            try
            {
                var result = new DriverBO().DeleteDriver(new Driver { DriverId = driverID });
                if (result)
                    return Ok(UTILITY.DELETEMSG);
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpGet]
        [Route("list/driverbyname/{status}")]

        public IHttpActionResult GetDriverBySearch(string status)
        {
            try
            {
                var result = new DriverBO().GetDriverBySearch(status);
                if (result != null)
                    return Ok(result);
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

        }


        [HttpGet]
        [Route("{driverID}")]
        public IHttpActionResult DriverInfo(string driverID)
        {
            try
            {
                var driver = new DriverBO().GetDriver(new Driver { DriverId = driverID });
                if (driver != null)
                    return Ok(driver);
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }


        [HttpGet]
        [Route("lookupdata")]
        public IHttpActionResult LookUpData()
        {
            try
            {
                var lookupList = new LookUpBO().GetList();

                var genderOptions = lookupList.Where(x => x.LookupCategory == "Gender").ToList();
                var maritalOptions = lookupList.Where(x => x.LookupCategory == "MaritalStatus").ToList();
                var attachments = lookupList.Where(x => x.LookupCategory == "DriverAttachments").ToList();
                var OperatorList= new OperatorBO().GetOperatorList();
                return Ok(new
                {
                    genderOptions = genderOptions,
                    maritalOptions = maritalOptions,
                    attachments = attachments,
                    OperatorList= OperatorList
                });
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPost]
        [Route("deviceid")]
        public IHttpActionResult UpdateDeviceId(DriverDeviceDTO driverDeviceDTO)
        {
            try
            {
                var result = new DriverBO().UpdateDriverDevice(driverDeviceDTO.driverId, driverDeviceDTO.deviceId);
                if (result)
                    return Ok(UTILITY.SUCCESSMSG);
                else
                    return BadRequest();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPost]
        [Route("saveattachment")]

        public IHttpActionResult SaveAttachment(DriverAttachmentsDTO attachments) {
            try
            {
                var result = new DriverBO().SaveAttachment(attachments);
                if (result)
                    return Ok(UTILITY.SUCCESSMSG);
                else
                    return BadRequest();
            }
            catch (Exception ex)
            {

                return InternalServerError(ex);
            }
        }

        [HttpGet]
        [Route("IsOperatorValid/{operatorId}")]
        public IHttpActionResult IsOperatorValid(string operatorId)
        {
            try
            {
                int IsOperatorExists = new OperatorBO().IsOperatorValid(operatorId);
                    return Ok(IsOperatorExists);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        [HttpGet]
        [Route("OperatorList")]
        public IHttpActionResult OperatorList()
        {
            try
            {
                var result = new OperatorBO().GetOperatorList();
                if (result != null)
                    return Ok(result);
                else
                    return BadRequest();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        

    }
}
