using SK.RestaurantTableBookingApp.Core.ViewModels;

namespace SK.RestaurantTableBookingApp.Service
{
    public interface IReservationService
    {
        Task<int> CreateOrUpdateReservationAsync(ReservationModel reservation);
        Task<bool> TimeSlotIdExistAsync(int timeSlotId);
        Task<DiningTableWithTimeSlotsModel> CheckInReservationAsync(DiningTableWithTimeSlotsModel reservation);
        Task<List<ReservationDetailsModel>> GetReservationDetails();
    }
}
