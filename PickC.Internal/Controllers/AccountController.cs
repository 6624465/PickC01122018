using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Threading.Tasks;
using Master.Contract;

using PickC.Services;
using PickC.Services.DTO;

using Operation.Contract;

namespace PickC.Internal.Controllers
{
    [PickCEx]
    public class AccountController : BaseController
    {
        [HttpGet]
        public ActionResult Login()
        {
            return View(new Customer());
        }

        [HttpPost]
        public async Task<ActionResult> Login(Customer customer)
        {
            var loginDTO = await new CustomerService().LoginAsync(customer);
            if(loginDTO != null && !string.IsNullOrWhiteSpace(loginDTO.token))
            {
                HttpContext.Session["SSN_TOKEN"] = loginDTO.token;
                var customerObj = await new CustomerService().GetCustomerInfoAsync(loginDTO.token, customer.MobileNo);
                HttpContext.Session["SSN_CUSTOMER"] = customerObj;

                return RedirectToAction("GetDriversList", "Dashboard", new { Area = "Internal" });
            }
            else
                return View("Login");
        }

        [HttpGet]
        [WebAuthFilter]
        public async Task<ActionResult> Logout()
        {
            var result = await new CustomerService(AUTHTOKEN, p_mobileNo).LogoutAsync();
            Session.Abandon();
            Session.Clear();

            return RedirectToAction("Login", "Account", null);
        }
    }
}