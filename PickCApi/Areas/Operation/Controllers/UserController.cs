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
    [RoutePrefix("api/security/user")]
    [ApiAuthFilter]
    public class UserController : ApiBase
    {

        [HttpGet]
        [Route("GetUsers")]
        public IHttpActionResult GetUserList()
        {
            try
            {
                var userlist = new UsersBO().GetList();
                if (userlist != null)
                    return Ok(userlist);
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }


        [HttpPost]
        [Route("EditUser")]
        public IHttpActionResult EditUser(Users userObject)
        {
            try
            {
                var result = new UsersBO().SaveUsers(userObject);
                return Ok(result ? UTILITY.SUCCESSMSG : UTILITY.FAILEDMSG);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }


        [HttpPost]
        [Route("SaveUser")]
        public IHttpActionResult SaveUser(Users userObject)
        {
            try
            {
                var result = new UsersBO().SaveUsers(userObject);
                return Ok(result ? UTILITY.SUCCESSMSG : UTILITY.FAILEDMSG);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }




        [HttpGet]
        [Route("DeleteUser")]
        public IHttpActionResult DeleteUser()
        {
            try
            {
                var UsersObj = new UsersBO();
                if (UsersObj != null)
                    return Ok(UsersObj);
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