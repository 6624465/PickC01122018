using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;

using Master.Contract;

using PickC.Services;
using PickC.Services.DTO;

namespace PickC.Web.Controllers
{
    public class AccountController : BaseController
    {
        [HttpGet]
        public ActionResult Login()
        {
            return View(new Customer());
        }
        public ActionResult Register()
         {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Register(Customer customer)
        {
            Session["MobileNo"] = customer.MobileNo;
            var result = await new CustomerService().RegisterAsync(customer);
            customer.OTP = result;
            return View(customer);

        }
        [HttpPost]
        public async Task<JsonResult> RegisterOTP(string OTP)
        {
            string MobNo = Session["MobileNo"].ToString();
            var result = await new CustomerService().RegisterOTPAsync(MobNo,OTP);
            return Json(result,JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public async Task<ActionResult> Login(Customer customer)
        {
            try
            {
                var loginDTO = await new CustomerService().LoginAsync(customer);
                if (loginDTO != null && !string.IsNullOrWhiteSpace(loginDTO.token))
                {
                    HttpContext.Session["SSN_TOKEN"] = loginDTO.token;
                    var customerObj = await new CustomerService().GetCustomerInfoAsync(loginDTO.token, customer.MobileNo);
                    HttpContext.Session["SSN_CUSTOMER"] = customerObj;

                    return RedirectToAction("Index", "Home");
                }
                else
                    return View("Login");
            }
            catch (Exception ex)
            {
                ViewBag.ErrMsg = ex.Message;
                return Content(ex.Message);
            }

        }

        [HttpGet]
        [WebAuthFilter]
        public async Task<JsonResult> Logoff()
        {
            var result = await new CustomerService(AUTHTOKEN, p_mobileNo).LogoutAsync();
            bool isSuccess = false;
            if (result == "\"USER LOGGEDOUT SUCCESSFULLY\"")
                isSuccess = true;
            else
                isSuccess = false;

                return Json(isSuccess, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [WebAuthFilter]
        public ActionResult CustomerProfile()
        {
            var customerObj = (Customer)HttpContext.Session["SSN_CUSTOMER"];
            return View(customerObj);
        }

        [HttpPost]
        [WebAuthFilter]
        public async Task<ActionResult> CustomerProfile(Customer customer)
        {
            var result = await new CustomerService().UpdateCustomerAsync(customer);
            if (result == "SAVED SUCSSFULLY")
                HttpContext.Session["SSN_CUSTOMER"] = customer;
            return RedirectToAction("Index", "Home");
        }


    }
}