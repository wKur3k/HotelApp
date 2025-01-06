using HotelApp.Models;
using System.Globalization;

namespace HotelApp.Services
{
    public class RoomAvailabilityService : IRoomAvailabilityService
    {
        public int CheckAvailableRooms(string hotelId, string date, string roomType, List<Hotel> hotels, List<Booking> bookings)
        {
            DateTime startDate, endDate;
            if (date.Contains("-"))
            {
                startDate = DateTime.ParseExact(date.Split('-')[0], "yyyyMMdd", CultureInfo.InvariantCulture);
                endDate = DateTime.ParseExact(date.Split('-')[1], "yyyyMMdd", CultureInfo.InvariantCulture);
            }
            else
            {
                startDate = DateTime.ParseExact(date, "yyyyMMdd", CultureInfo.InvariantCulture);
                endDate = startDate;
            }

            var conflictingBookings = bookings
                .Where(b => b.HotelId == hotelId && b.RoomType == roomType &&
                            ((startDate >= b.ArrivalDate && startDate < b.DepartureDate) ||
                             (endDate > b.ArrivalDate && endDate <= b.DepartureDate) ||
                             (startDate < b.ArrivalDate && endDate > b.DepartureDate)))
                .Count();

            return hotels.FirstOrDefault(h => h.Id == hotelId).GetRooms(roomType).Count() - conflictingBookings;
        }
    }
}
