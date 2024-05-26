using BlueStarMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BlueStarMVC.Pages.Server.DTOs;
using System.Globalization;

namespace BlueStarMVC.Pages.Server.Controllers
{
    [Route("api/flight")]
    [ApiController]
    public class FlightController : ControllerBase
    {
        private readonly BluestarContext BlueContext;
        public FlightController(BluestarContext blueContext)
        {
            BlueContext = blueContext;
        }
        [HttpGet]
        [Route("SearchFlight")]
        public async Task<IActionResult> SearchFlight([FromQuery] QueryFlightDTOs query)
        {
            string departureDay = null;

            // Extract the day value and convert it to a string
            string day = query.DepatureDay.Substring(8, 2);
            string month = query.DepatureDay.Substring(5, 2);
            string year = query.DepatureDay.Substring(0, 4);

            departureDay = $"{day}-{month}-{year}";

            Console.WriteLine(departureDay);

            var flightsQuery = BlueContext.Chuyenbays
                .Where(c =>
                    c.FromLocation == query.FromLocation &&
                    c.ToLocation == query.ToLocation &&
                    c.DepartureDay == query.DepatureDay &&
                    c.SeatEmpty > c.SeatBooked
                );
                

            if (query.DepartureTime != null && query.ArrivalTime != null)
            {
                TimeSpan departureTimeSpan = TimeSpan.Parse(query.DepartureTime);
                TimeSpan arrivalTimeSpan = TimeSpan.Parse(query.ArrivalTime);

                // Parse DepartureTime values outside of the LINQ query
                var parsedFlights = await flightsQuery.ToListAsync();

                // Use parsed values within the LINQ query
                var filteredFlights = parsedFlights
                    .Where(f => TimeSpan.Parse(f.DepartureTime) >= departureTimeSpan && TimeSpan.Parse(f.DepartureTime) <= arrivalTimeSpan)
                    .ToList();

                int totalFlights = filteredFlights.Count;

                var result = new
                {
                    total_flight = totalFlights,
                    flight = filteredFlights
                };

                return Ok(result);
            }

            var allFlights = await flightsQuery.ToListAsync();
            int totalAllFlights = allFlights.Count;

            var allFlightsResult = new
            {
                total_flight = totalAllFlights,
                flight = allFlights
            };

            return Ok(allFlightsResult);
        }

    }
}