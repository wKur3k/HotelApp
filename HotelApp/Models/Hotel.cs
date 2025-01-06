namespace HotelApp.Models
{
    public class Hotel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        private List<RoomType> RoomTypes { get; set; }
        private List<Room> Rooms { get; set; }

        public void SetRooms(List<Room> rooms)
        {
            Rooms = rooms;
        }

        public List<Room> GetRooms(string roomTypeCode)
        {
            return Rooms.Where(r => r.RoomType.Code == roomTypeCode).ToList();
        }

        public void SetRoomTypes(List<RoomType> roomTypes)
        {
            RoomTypes = roomTypes;
        }
    }
}
