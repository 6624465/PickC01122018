using PickC.Services;
using PickC.Services.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace PickC.Internal.Areas.Internal.Controllers
{
    [WebAuthFilter]
    public class SearchController : BaseController
    {
        // GET: Internal/Search

        [HttpGet]
        public async Task<ActionResult> CurrentBookingSearch()
        {

            BookingDTO bookingSearch = new BookingDTO();

            var task1 = new VehicleGroupService(AUTHTOKEN, p_mobileNo).GetDataAsync();
            var task2 = new VehicleTypeService(AUTHTOKEN, p_mobileNo).GetDataAsync();

            var result = await Task.WhenAll(task1, task2);

            bookingSearch.VehicleGroupList = result[0];
            bookingSearch.VehicleTypeList = result[1];
            return View(bookingSearch);
        }

        [HttpPost]
        public async Task<ActionResult> CurrentBookingSearchData(BookingDTO search)
        {
            var obj = await new SearchService(AUTHTOKEN, p_mobileNo).SearchCurrentBookingAsync(search);
            return View(obj);
        }
    }
}