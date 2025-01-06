using HotelApp.Models;

namespace HotelApp.Services
{
    public interface IRoomAvailabilityService
    {
        int CheckAvailableRooms(string hotelId, string date, string roomType, List<Hotel> hotels, List<Booking> bookings);
    }
}