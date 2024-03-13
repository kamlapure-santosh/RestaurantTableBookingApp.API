using SK.RestaurantTableBookingApp.Core;
using SK.RestaurantTableBookingApp.Core.ViewModels;
using SK.RestaurantTableBookingApp.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.Identity.Web.Resource;

namespace SK.RestaurantTableBookingApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationService reservationService;
        public ReservationController(IReservationService reservationService)
        {
            this.reservationService = reservationService;
        }

        [HttpGet("{id}", Name = "GetReservation")]
        [RequiredScope(RequiredScopesConfigurationKey = "AzureAdB2C:Scopes:Read")]
        public async Task<ActionResult<Reservation>> GetReservation(int id)
        {
            // Your logic to retrieve and return a reservation
            return Ok();
        }
        [HttpPost("CheckIn")]
        [ProducesResponseType(200, Type = typeof(ReservationModel))]
        [ProducesResponseType(400)]
        [RequiredScope(RequiredScopesConfigurationKey = "AzureAdB2C:Scopes:Write")]
        public async Task<ActionResult<ReservationModel>> CheckInReservationAsync(DiningTableWithTimeSlotsModel reservation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Check if the selected time slot exists
            var timeSlot = await reservationService.TimeSlotIdExistAsync(reservation.TimeSlotId);
            if (!timeSlot)
            {
                return NotFound("Selected time slot not found.");
            }
            var response = await reservationService.CheckInReservationAsync(reservation);
       //     await emailNotification.SendCheckInEmailAsync(reservation);
            return Ok(response);
        }
    }
}
