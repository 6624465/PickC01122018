
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using Master.BusinessFactory;

namespace PickCApi.Areas.Master.Controllers
{
    [RoutePrefix("api/master/cargotype")]
    public class CargoTypeController : ApiController
    {
        //[HttpGet]
        //[Route("list")]
        //public IHttpActionResult GetCargoTypeList()
        //{
        //    try
        //    {
        //        var typelist = new LookUpBO().GetCargoTypeList();
        //        if (typelist != null)
        //            return Ok(typelist);
        //        else
        //            return NotFound();

        //    }
        //    catch (Exception ex)
        //    {
        //        return InternalServerError(ex);
        //    }
        //}
    }
}
