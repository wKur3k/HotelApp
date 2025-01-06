using HotelApp.Converters;
using Newtonsoft.Json;

namespace HotelAppTests
{
    public class CustomDateTimeConverterTests
    {
        [Fact]
        public void ReadJson_ShouldDeserializeDateCorrectly()
        {
            var dateStr = "20240901";
            var converter = new CustomDateTimeConverter();
            var settings = new JsonSerializerSettings { Converters = new List<JsonConverter> { converter } };
            var date = JsonConvert.DeserializeObject<DateTime>(dateStr, settings);

            Assert.Equal(new DateTime(2024, 9, 1), date);
        }
    }
}
