using HotelApp.Converters;
using HotelApp.Models;
using HotelApp.Services;
using Newtonsoft.Json;

namespace HotelApp
{
    public class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 4 || args[0] != "--hotels" || args[2] != "--bookings")
            {
                Console.WriteLine("Usage: HotelApp --hotels <hotels.json> --bookings <bookings.json>");
                return;
            }

            IRoomAvailabilityService roomAvailabilityService = new RoomAvailabilityService();

            try
            {
                var hotels = JsonConvert.DeserializeObject<List<Hotel>>(File.ReadAllText(args[1]), new JsonSerializerSettings
                {
                    Converters = { new HotelConverter() }
                });
                var bookings = JsonConvert.DeserializeObject<List<Booking>>(File.ReadAllText(args[3]), new JsonSerializerSettings
                {
                    Converters = { new CustomDateTimeConverter() }
                });
                while (true)
                {
                    Console.WriteLine("Check room availability:\nUsage: HotelId Date(or date range with '-' separator RoomType)\nExample: H1 20240901 SGL\nTo close press Enter\n");
                    String input = Console.ReadLine();
                    if (String.IsNullOrEmpty(input))
                    {
                        break;
                    }
                    Console.WriteLine(roomAvailabilityService.CheckAvailableRooms(input.Split(' ')[0], input.Split(' ')[1], input.Split(' ')[2], hotels, bookings));

                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }

        }

        static void HandleException(Exception ex)
        {
            if (ex is ArgumentException)
            {
                Console.WriteLine("Invalid argument: " + ex.Message);
            }
            else if (ex is InvalidOperationException)
            {
                Console.WriteLine("Invalid operation: " + ex.Message);
            }
            else
            {
                Console.WriteLine("An unexpected error occurred: " + ex.Message);
            }
        }
    }
}
