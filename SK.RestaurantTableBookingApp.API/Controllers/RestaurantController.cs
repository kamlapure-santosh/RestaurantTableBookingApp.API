using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SK.RestaurantTableBookingApp.Core;
using SK.RestaurantTableBookingApp.Core.ViewModels;
using SK.RestaurantTableBookingApp.Service;

namespace SK.RestaurantTableBookingApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantController : ControllerBase
    {
        private readonly IRestaurantService _restaurantService;

        public RestaurantController(IRestaurantService restaurantService)
        {
            _restaurantService = restaurantService;
        }

        [HttpGet("restaurant")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<RestaurantModel>))]
        public async Task<ActionResult> GetAllRestaurant() {
            var restaurant = await _restaurantService.GetAllRestaurantsAsync();
            return Ok(restaurant); // ok will reurn 200 http status and the model will be in json format
        }
        [HttpGet("branches/{restaurantId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<RestaurantBranchModel>))]
        [ProducesResponseType(404)]
        public async Task<ActionResult<IEnumerable<RestaurantBranchModel>>> GetRestaurantBranchsByRestaurantIdAsync(int restaurantId)
        {
            var branches = await _restaurantService.GetRestaurantBranchsByRestaurantIdAsync(restaurantId);
            if (branches == null)
            {
                return NotFound();
            }
            return Ok(branches);
        }

        [HttpGet("diningtables/{branchId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<DiningTableWithTimeSlotsModel>))]
        [ProducesResponseType(404)]
        public async Task<ActionResult<IEnumerable<DiningTableWithTimeSlotsModel>>> GetDiningTablesByBranchAsync(int branchId)
        {
            var diningTables = await _restaurantService.GetDiningTablesByBranchAsync(branchId);
            if (diningTables == null)
            {
                return NotFound();
            }
            return Ok(diningTables);
        }

        [HttpGet("diningtables/{branchId}/{date}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<DiningTableWithTimeSlotsModel>))]
        [ProducesResponseType(404)]
        public async Task<ActionResult<IEnumerable<DiningTableWithTimeSlotsModel>>> GetDiningTablesByBranchAndDateAsync(int branchId, DateTime date)
        {
            var diningTables = await _restaurantService.GetDiningTablesByBranchAsync(branchId, date);
            if (diningTables == null)
            {
                return NotFound();
            }
            return Ok(diningTables);
        }
    }
}
