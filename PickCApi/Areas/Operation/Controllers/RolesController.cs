using Master.BusinessFactory;
using Operation.BusinessFactory;
using Operation.Contract;
using PickCApi.Areas.Operation.DTO;
using PickCApi.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace PickCApi.Areas.Operation.Controllers
{
    [RoutePrefix("api/security/role")]
    [ApiAuthFilter]
    public class RolesController : ApiBase
    {


        [HttpPost]
        [Route("SaveRights")]
        public IHttpActionResult SaveRoleRights()
        {
            try
            {
                var RoleRightsObj = new RoleRightsBO();
                if (RoleRightsObj != null)
                    return Ok(RoleRightsObj);
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