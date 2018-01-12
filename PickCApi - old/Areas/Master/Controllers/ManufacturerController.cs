using Master.BusinessFactory;
using PickC.Services.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PickCApi.Areas.Master.Controllers
{
    [RoutePrefix("api/master/manufacturer")]
    public class ManufacturerController : ApiController
    {
        [Route("save"),HttpPost]

        public IHttpActionResult Save(DriverManufacturerDTO manufacturer)
        {
            try
            {
                var result = new ManufacturerBO().Save(manufacturer);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

        }
    }
}
