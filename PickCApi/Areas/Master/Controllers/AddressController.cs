using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using Master.Contract;
using Master.BusinessFactory;

namespace PickCApi.Areas.Master.Controllers
{
        //vijay comments
    [RoutePrefix("api/master/address")]
    public class AddressController : ApiController
    {
        [Route("list/{addressLinkID}"), HttpGet]
        public IHttpActionResult List(string addressLinkID)
        {
            try
            {
                var addressList = new AddressBO().GetList(addressLinkID);
                return Ok(addressList);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Route("{addressID}"), HttpGet]
        public IHttpActionResult GetAddress(int addressID)
        {
            try
            {
                var address = new AddressBO().GetAddress(new Address { AddressId = addressID });
                return Ok(address);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Route("save"), HttpPost]
        public IHttpActionResult SaveAddress(Address address)
        {
            try
            {
                var result = new AddressBO().SaveAddress(address);
                return Ok(result ? UTILITY.SUCCESSMSG : UTILITY.FAILEDMSG);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Route("{addressID}"), HttpDelete]
        public IHttpActionResult DeleteAddress(int addressID)
        {
            try
            {
                var result = new AddressBO().DeleteAddress(new Address { AddressId = addressID });
                return Ok(result ? UTILITY.DELETEMSG : UTILITY.FAILEDMSG);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
