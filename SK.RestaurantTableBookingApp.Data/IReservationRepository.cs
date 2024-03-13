using SK.RestaurantTableBookingApp.Core.ViewModels;
using SK.RestaurantTableBookingApp.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SK.RestaurantTableBookingApp.Data
{
    public interface IReservationRepository
    {
        Task<int> CreateOrUpdateReservationAsync(ReservationModel reservation);
        Task<TimeSlot> GetTimeSlotByIdAsync(int timeSlotId);

        Task<DiningTableWithTimeSlotsModel> UpdateReservationAsync(DiningTableWithTimeSlotsModel reservation);
        Task<List<ReservationDetailsModel>> GetReservationDetailsAsync();
    }
}
