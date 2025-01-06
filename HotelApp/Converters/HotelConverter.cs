using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using HotelApp.Models;

namespace HotelApp.Converters
{
    public class HotelConverter : Newtonsoft.Json.JsonConverter<Hotel>
    {
        public override Hotel ReadJson(JsonReader reader, Type objectType, Hotel existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            JObject obj = JObject.Load(reader);

            var hotel = new Hotel
            {
                Id = obj["id"]?.ToString(),
                Name = obj["name"]?.ToString()
            };

            var roomTypes = obj["roomTypes"]?.ToObject<List<RoomType>>(serializer);
            var rooms = new List<Room>();

            foreach (var roomObj in obj["rooms"])
            {
                var roomTypeCode = roomObj["roomType"]?.ToString();
                var matchingRoomType = roomTypes?.Find(rt => rt.Code == roomTypeCode);

                rooms.Add(new Room
                {
                    RoomId = int.Parse(roomObj["roomId"].ToString()),
                    RoomType = matchingRoomType
                });
            }

            hotel.SetRoomTypes(roomTypes);
            hotel.SetRooms(rooms);

            return hotel;
        }

        public override void WriteJson(JsonWriter writer, Hotel value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
