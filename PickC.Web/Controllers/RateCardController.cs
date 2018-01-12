using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;

using PickC.Web.ViewModels;
using PickC.Services;
using PickC.Services.DTO;

namespace PickC.Web.Controllers
{
    public class RateCardController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult> mobile()
        {
            var list = await new RateCardService(AUTHTOKEN, p_mobileNo).GetDataAsync();
            return View(list);
        }

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            var list = await new RateCardService(AUTHTOKEN, p_mobileNo).GetDataAsync();
            return View(list);
        }

        [HttpGet]
        public async Task<ActionResult> Ratecard()
        {
            var rateCardVm = new RateCardVm();
            var task1 = new VehicleGroupService(AUTHTOKEN, p_mobileNo).GetDataAsync();
            var task2 = new VehicleTypeService(AUTHTOKEN, p_mobileNo).GetDataAsync();

            var result = await Task.WhenAll(task1, task2);

            rateCardVm.VehicleGrouplookUpData = result[0];
            rateCardVm.VehicleTypelookUpData = result[1];

            return View(rateCardVm);
        }

        [HttpGet]
        public ActionResult RideEstimate()
        {
            return View();
        }
    }
}