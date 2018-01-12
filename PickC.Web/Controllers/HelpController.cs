using Master.Contract;
using PickC.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace PickC.Web.Controllers
{
    public class HelpController : BaseController
    {
        public ActionResult menu(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return View("menu", "_Layout");
            else
                return View(name, "_Layout");
        }
        public ActionResult FindCRN()
        {
            if (ISLOGGEDIN)
                return View();
            else
                return RedirectToAction("Login", "Account");
        }
        public ActionResult Services()
        {
            return View();
        }
        public ActionResult mobilehelp()
        {
            return View();
        }
        public ActionResult DriverInvoice()
        {
            return View();
        }
        public ActionResult CompanyInvoice()
        {
            return View();
        }
        public async Task<ActionResult> TRIPINVOICE(string bookingNo)
        {
            var bookingHistoryList = await new CustomerService().TripInvoiceList(bookingNo);

            return View(bookingHistoryList);
        }

        public ActionResult TRIPINVOICEMOBILE()
        {
            return View();
        }
        public ActionResult AboutUs()
        {
            return View();
        }

        public ActionResult ContactUs()
        {
            return View();
        }

        public async Task<ActionResult> SendMail(ContactUs contactUs)
        {
            string test= await new CustomerService().SendMail(contactUs);
            if (contactUs.Type == "ContactUs")
                return Content("<script language='javascript' type='text/javascript'>alert('Your Request is Received.!');window.location = '/Help/ContactUs';</script>");

            //return View("ContactUs");
            else if (contactUs.Type == "CustomerSupport")
                return View("Support");
            else if (contactUs.Type == "FeedBack")
                return View("FeedBack");
            else if (contactUs.Type == "Careers")
                return View("Careers");
            else
                return View("ContactUs");

        }

        public ActionResult FeedBack()
        {
            return View();
        }

        public ActionResult FAQS()
        {
            return View();
        }

        public ActionResult Terms()
        {
            return View();
        }

        public ActionResult Careers()
        {
            return View();
        }

        public ActionResult PrivacyPolicy()
        {
            return View();
        }
        public ActionResult Support() {
            return View();
        }
    }
}