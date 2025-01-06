using HotelApp.Converters;
using Newtonsoft.Json;

namespace HotelApp.Models
{
    public class Booking
    {
        public string HotelId { get; set; }

        [JsonConverter(typeof(CustomDateTimeConverter))]
        [JsonProperty("arrival")]
        public DateTime ArrivalDate { get; set; }

        [JsonConverter(typeof(CustomDateTimeConverter))]
        [JsonProperty("departure")]
        public DateTime DepartureDate { get; set; }

        public string RoomType { get; set; }
        public string RoomRate { get; set; }
    }
}
