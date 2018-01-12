using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Threading.Tasks;
using PickC.Services;


namespace PickC.Internal.Areas.Internal.Controllers
{
    [WebAuthFilter]
    [PickCEx]
    public class CustomersController : BaseController
    {
        public async Task<ActionResult> Index()
        {
            var customerList = await new CustomerService(AUTHTOKEN, p_mobileNo).GetCustomerListAsync();
            return View(customerList);
        }
    }
}