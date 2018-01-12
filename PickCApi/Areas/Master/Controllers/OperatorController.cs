
using System;
using System.Web;
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
using Operation.BusinessFactory;
using System.IO;

namespace PickCApi.Areas.Master.Controllers
{
    [RoutePrefix("api/master/operator")]
    //[OperatorAPIAuthFilter]
    public class OperatorController : ApiBase
    {

        [HttpGet]
        [Route("list")]
        public IHttpActionResult OperatorList()
        {
            try
            {
                var operatorList = new OperatorBO().GetList();

                if (operatorList != null)
                    return Ok(operatorList);
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

                return Ok(new
                {
                    genderOptions = genderOptions,
                    maritalOptions = maritalOptions,
                    attachments = attachments
                });
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        //[HttpPost]
        //[Route("saveattachment")]

        //public IHttpActionResult SaveAttachment(OperatorAttachment attachments)
        //{
        //    try
        //    {
        //        var result = new OperatorBO().SaveAttachment(attachments);
        //        if (result)
        //            return Ok(UTILITY.SUCCESSMSG);
        //        else
        //            return BadRequest();
        //    }
        //    catch (Exception ex)
        //    {

        //        return InternalServerError(ex);
        //    }
        //}
        [HttpDelete]
        [Route("{OPEratorID}")]
        public IHttpActionResult DeleteOperator(string operatorID)
        {
            try
            {
                var result = new OperatorBO().DeleteOperator(new Operator {OperatorID = operatorID });
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
        [HttpPost]
        [Route("save")]
        public IHttpActionResult SaveOperator(Operator OPerator)
        {
            try
            {
                OPerator.CreatedBy = UTILITY.DEFAULTUSER;
                OPerator.ModifiedBy = UTILITY.DEFAULTUSER;
               
                if (OPerator.AddressList != null && OPerator.AddressList.Count > 0)
                {
                    OPerator.AddressList.ForEach(x =>
                    {
                        x.AddressLinkId = OPerator.OperatorID;
                        x.AddressType = "Operator";
                        x.CreatedBy = UTILITY.DEFAULTUSER;
                        x.CreatedOn = DateTime.Now;
                        x.ModifiedBy = UTILITY.DEFAULTUSER;
                        x.ModifiedOn = DateTime.Now;
                        x.IsActive = true;
                    });
                }
                var result = new OperatorBO().SaveOperator(OPerator);
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
        [Route("{operatorID}")]
        public IHttpActionResult OperatorInfo(string operatorID)
        {
            try
            {
                var OPerator = new OperatorBO().GetOperatorDetails(operatorID);
                if (OPerator != null)
                    return Ok(OPerator);
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
      
    }
}
