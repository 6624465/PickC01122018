using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using Master.Contract;
using Master.BusinessFactory;
using PickCApi.Core;

namespace PickCApi.Areas.Master.Controllers
{
    [RoutePrefix("api/master/ratecard")]
    //[ApiAuthFilter]
    public class RateCardController : ApiController
    {
        //[HttpGet]
        //[Route("list")]
        //public IHttpActionResult GetRateCardList()
        //{
        //    try
        //    {
        //        var rateCardList = new RateCardBO().GetList();

        //        if (rateCardList != null)
        //            return Ok(rateCardList);
        //        else
        //            return NotFound();
        //    }
        //    catch (Exception ex)
        //    {
        //        return InternalServerError(ex);
        //    }
        //}
        [HttpGet]
        [Route("{VehicleType}/{VehicleCategory}")]
        public IHttpActionResult GetRateCardValues(Int16 VehicleType, Int16 VehicleCategory)
        {
            try
            {
                var rateCardList = new RateCardBO().GetList().Where(x=>x.VehicleType== VehicleType && x.Category== VehicleCategory && x.RateType==1350);
                if (rateCardList != null)
                    return Ok(rateCardList);
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        [HttpPost]
        [ApiAuthFilter]
        [Route("save")]
        public IHttpActionResult SaveRateCard(RateCard rateCard)
        {
            try
            {
                var result = new RateCardBO().SaveRateCard(rateCard);
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
        [ApiAuthFilter]
        [Route("{category}")]
        public IHttpActionResult GetRateCard(Int16 category)
        {
            try
            {
                var rateCard = new RateCardBO().GetRateCard(new RateCard { Category = category });
                if (rateCard != null)
                    return Ok(rateCard);
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpDelete]
        [ApiAuthFilter]
        [Route("{category}")]
        public IHttpActionResult DeleteRateCard(Int16 category)
        {
            try
            {
                var result = new RateCardBO().DeleteRateCard(new RateCard { Category = category });
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
    }
}
