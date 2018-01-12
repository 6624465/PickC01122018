
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using Master.BusinessFactory;

namespace PickCApi.Areas.Master.Controllers
{
    [RoutePrefix("api/master/loadingunloading")]
    public class LoadingUnLoadingController : ApiController
    {
        [Route("list"), HttpGet]
        public IHttpActionResult List()
        {
            try
            {
                var list = new LookUpBO().GetLoadingUnLoadingList();
                if (list != null)
                    return Ok(list);
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
