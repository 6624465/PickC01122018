using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Threading.Tasks;
using Master.Contract;
using PickC.Services;

namespace PickC.Internal.Controllers
{
    [WebAuthFilter]
    [PickCEx]
    public class AddressController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<PartialViewResult> Address(int addressID)
        {
            Address address;
            if (addressID != -1)
                address = await new AddressService(AUTHTOKEN, p_mobileNo).AddressAsync(addressID);            
            else
                address = new Address();

            return PartialView(address);
        } 
    }
}