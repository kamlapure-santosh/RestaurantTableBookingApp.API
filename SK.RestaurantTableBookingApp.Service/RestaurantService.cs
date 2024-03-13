using SK.RestaurantTableBookingApp.Core.ViewModels;
using SK.RestaurantTableBookingApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SK.RestaurantTableBookingApp.Service
{
    public class RestaurantService : IRestaurantService
    {
        private readonly IRestaurantRepository _restaurantRepository;

        public RestaurantService(IRestaurantRepository restaurantRepository) {
            this._restaurantRepository = restaurantRepository;
        }
        Task<IEnumerable<RestaurantModel>> IRestaurantService.GetAllRestaurantsAsync()
        {
            return _restaurantRepository.GetAllRestaurantAsync();
        }
        public Task<IEnumerable<RestaurantBranchModel>> GetRestaurantBranchsByRestaurantIdAsync(int restaurantId)
        {
            return _restaurantRepository.GetRestaurantBranchsByRestaurantIdAsync(restaurantId);
        }

        public Task<IEnumerable<DiningTableWithTimeSlotsModel>> GetDiningTablesByBranchAsync(int branchId, DateTime date)
        {
            return _restaurantRepository.GetDiningTablesByBranchAsync(branchId, date);
        }

        public Task<IEnumerable<DiningTableWithTimeSlotsModel>> GetDiningTablesByBranchAsync(int branchId)
        {
            return _restaurantRepository.GetDiningTablesByBranchAsync(branchId);
        }
    }
}
