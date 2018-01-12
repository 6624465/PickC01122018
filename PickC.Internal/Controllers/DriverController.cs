using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Threading.Tasks;

using Master.Contract;
using PickC.Services;
using PickC.Services.DTO;
using PickC.Internal.ViewModals;

namespace PickC.Internal.Controllers
{
    [WebAuthFilter]
    [PickCEx]
    public class DriverController : BaseController
    {        
        [HttpGet]
        public async Task<ActionResult> Driver()
        {
            var driverList = await new DriverService(AUTHTOKEN, p_mobileNo).DriversListAsync();
            return View(driverList);
        }

        [HttpGet]
        public async Task<ActionResult> Add()
        {
                     var driverVm = new DriverVm
            {
                driverLookupDTO = await new DriverService(AUTHTOKEN, p_mobileNo).LookUpDataAsync(),
                driver = new Driver()
            };

            return View(driverVm);
        }

        [HttpGet]
        public async Task<ActionResult> Edit(string driverID)
        {
            Task<Driver> taskDriver = new DriverService(AUTHTOKEN, p_mobileNo).DriverInfoAsync(driverID);
            Task<DriverLookupDTO> taskDriverLookupDTO = new DriverService(AUTHTOKEN, p_mobileNo).LookUpDataAsync();

            await Task.WhenAll(taskDriver, taskDriverLookupDTO);            

            var driverVm = new DriverVm
            {
                driverLookupDTO = await taskDriverLookupDTO,
                driver = await taskDriver
            };

            return View(driverVm);
        }

        [HttpPost]
        public async Task<ActionResult> SaveDriver(Driver driver)
        {            
            var result = await new DriverService(AUTHTOKEN, p_mobileNo).SaveDriverAsync(driver);
            return RedirectToAction("Driver", "Driver");
        }
    }
}