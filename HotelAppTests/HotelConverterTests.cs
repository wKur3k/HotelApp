using HotelApp.Converters;
using HotelApp.Models;
using Newtonsoft.Json;

namespace HotelAppTests
{
    public class HotelConverterTests
    {
        [Fact]
        public void ReadJson_ShouldDeserializeHotelCorrectly()
        {
            var json = @"
            {
                ""id"": ""H1"",
                ""name"": ""Hotel 1"",
                ""roomTypes"": [{""code"": ""SGL""}],
                ""rooms"": [{""roomId"": 1, ""roomType"": ""SGL""}]
            }";

            var converter = new HotelConverter();
            var settings = new JsonSerializerSettings { Converters = new List<JsonConverter> { converter } };
            var hotel = JsonConvert.DeserializeObject<Hotel>(json, settings);

            Assert.Equal("H1", hotel.Id);
            Assert.Equal("Hotel 1", hotel.Name);
            Assert.Single(hotel.GetRooms("SGL"));
        }
    }
}
