using SK.RestaurantTableBookingApp.Core.ViewModels;

namespace SK.RestaurantTableBookingApp.Service
{
    public interface IRestaurantService
    {
        Task<IEnumerable<RestaurantModel>> GetAllRestaurantsAsync();
        Task<IEnumerable<RestaurantBranchModel>> GetRestaurantBranchsByRestaurantIdAsync(int restaurantId);
        Task<IEnumerable<DiningTableWithTimeSlotsModel>> GetDiningTablesByBranchAsync(int branchId, DateTime date);
        Task<IEnumerable<DiningTableWithTimeSlotsModel>> GetDiningTablesByBranchAsync(int branchId);
    }
}
