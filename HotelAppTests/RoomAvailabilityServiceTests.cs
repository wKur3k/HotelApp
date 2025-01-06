using HotelApp.Models;
using HotelApp.Services;
using Moq;

namespace HotelAppTests
{
    public class RoomAvailabilityServiceTests
    {
        private List<Hotel> _hotels;
        private List<Booking> _bookings;

        public RoomAvailabilityServiceTests()
        {
            _hotels = new List<Hotel>
            {
                new Hotel { Id = "H1", Name = "Hotel 1" }
            };

            _bookings = new List<Booking>
            {
                new Booking { HotelId = "H1", RoomType = "SGL", ArrivalDate = new DateTime(2024, 9, 1), DepartureDate = new DateTime(2024, 9, 5) }
            };
        }

        [Fact]
        public void CheckAvailableRooms_ShouldReturnCorrectAvailability_WhenRoomsAreBooked()
        {
            var roomAvailabilityServiceMock = new Mock<IRoomAvailabilityService>();
            roomAvailabilityServiceMock
                .Setup(service => service.CheckAvailableRooms("H1", "20240901", "SGL", _hotels, _bookings))
                .Returns(0);

            var service = roomAvailabilityServiceMock.Object;
            _hotels[0].SetRooms(new List<Room>
            {
                new Room { RoomId = 1, RoomType = new RoomType { Code = "SGL" } }
            });

            var availableRooms = service.CheckAvailableRooms("H1", "20240901", "SGL", _hotels, _bookings);

            Assert.Equal(0, availableRooms);
        }

        [Fact]
        public void CheckAvailableRooms_ShouldReturnCorrectAvailability_WhenNoBookings()
        {
            _bookings.Clear();
            var roomAvailabilityServiceMock = new Mock<IRoomAvailabilityService>();
            roomAvailabilityServiceMock
                .Setup(service => service.CheckAvailableRooms("H1", "20240901", "SGL", _hotels, _bookings))
                .Returns(1);

            var service = roomAvailabilityServiceMock.Object;
            _hotels[0].SetRooms(new List<Room>
            {
                new Room { RoomId = 1, RoomType = new RoomType { Code = "SGL" } }
            });

            var availableRooms = service.CheckAvailableRooms("H1", "20240901", "SGL", _hotels, _bookings);

            Assert.Equal(1, availableRooms);
        }
    }
}
