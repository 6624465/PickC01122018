using PickC.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace PickC.Internal.Areas.DriverWeb.Controllers
{
    public class PaymentController : BaseController
    {        
        public async  Task<ActionResult> Summary(string driverID)
        {
            driverID = "DR161100001";
            var result = await new DriverSummaryService("C636A9C5-C4C5-400E-8381-5884EE1CDC24", "1234554321").GetDriverSummary(driverID);
            result.driverId = driverID;
            return View(result);
        }

        public async Task<ActionResult> Breakup(string driverID)
        {
            var result = await new DriverSummaryService("C636A9C5-C4C5-400E-8381-5884EE1CDC24", "1234554321").GetDriverSummary(driverID);
            result.pickcCommission = (result.TodaySummary / 100) * 10;
            result.driverId = driverID;
            return View(result);
        }

        public ActionResult Reverify()
        {
            return View();
        }

        public async Task<ActionResult> DayWiseHistory(string driverID)
        {
            var result = await new DriverSummaryService("C636A9C5-C4C5-400E-8381-5884EE1CDC24", "1234554321").GetPayment(driverID);

            return View(result);
        }

        public async Task<ActionResult> Payments(string driverID)
        {
            var result = await new DriverSummaryService("C636A9C5-C4C5-400E-8381-5884EE1CDC24", "1234554321").GetPayment(driverID);

            return View(result);
        }

        public ActionResult PaymentDetails(string driverID)
        {
           // var result = await new DriverSummaryService("C636A9C5-C4C5-400E-8381-5884EE1CDC24", "1234554321").GetPaymentDetails(driverID);
            return View();
        }

        public ActionResult Incentive()
        {
            return View();
        }
    }
}