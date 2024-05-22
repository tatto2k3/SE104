using BlueStarMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BlueStarMVC.Pages.Server.Controllers
{
    [Route("api/doanhthu")]
    [ApiController]
    public class DoanhthuController : ControllerBase
    {
        private readonly BluestarContext _dbContext;

        public DoanhthuController(BluestarContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        [Route("GetDoanhSo")]
        public IActionResult GetDoanhSo([FromQuery] string year)
        {
            int[] month = new int[12];

            var result = from chuyenBay in _dbContext.Chuyenbays 
                         where chuyenBay.DepartureDay.Substring(0, 4) == year
                         select new
                         {
                             chuyenBay.FlyId,
                             chuyenBay.DepartureDay
                         };

            foreach (var item in result)
            {
                if (int.TryParse(item.DepartureDay.Substring(5, 2), out int monthNumber))
                {
                    // Assuming 1-based index for months
                    month[monthNumber - 1]++;
                }
            }
            return Ok(month);
        }

        [HttpGet]
        [Route("GetDoanhThuNam")]
        public IActionResult GetDoanhThuNam([FromQuery] string year)
        {
            try
            {
                // Ensure the year is not null or empty
                if (string.IsNullOrEmpty(year))
                {
                    return BadRequest("Year parameter is required.");
                }

                var result = from ticket in _dbContext.Tickets
                             join chuyenBay in _dbContext.Chuyenbays on ticket.FlyId equals chuyenBay.FlyId
                             where chuyenBay.DepartureDay.Substring(0, 4) == year
                             select new
                             {
                                 ticket.FlyId,
                                 chuyenBay.DepartureDay,
                                 ticket.TicketPrice
                             };

                // Calculate total revenue for the year
                decimal totalRevenue = result.Sum(item => (decimal) item.TicketPrice);

                var details = _dbContext.Tickets
            .Join(
                _dbContext.Chuyenbays,
                ticket => ticket.FlyId,
                flight => flight.FlyId,
                (ticket, flight) => new
                {
                    TId = ticket.TId,
                    Cccd = ticket.Cccd,
                    Name = ticket.Name,
                    FlyId = flight.FlyId,
                    DepartureDay = flight.DepartureDay,
                    Price = ticket.TicketPrice
                }
            )
            .Where(item => item.DepartureDay.Substring(0, 4) == year)
            .ToList();

                // You can return the result along with the total revenue
                return Ok(new { TotalRevenue = totalRevenue, Details = details });

                // You can return the result along with the total revenue
                return Ok(totalRevenue);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpGet]
        [Route("GetDoanhThuThang")]
        public IActionResult GetDoanhThuThang([FromQuery] string year, string month)
        {
            try
            {
                // Ensure the year is not null or empty
                if (string.IsNullOrEmpty(year) || string.IsNullOrEmpty(month))
                {
                    return BadRequest("Year parameter is required.");
                }

                var result = from ticket in _dbContext.Tickets
                             join chuyenBay in _dbContext.Chuyenbays on ticket.FlyId equals chuyenBay.FlyId
                             where chuyenBay.DepartureDay.Substring(0, 4) == year && chuyenBay.DepartureDay.Substring(5, 2) == month
                             select new
                             {
                                 ticket.FlyId,
                                 chuyenBay.DepartureDay,
                                 ticket.TicketPrice
                             };

                decimal totalRevenue = (decimal) result.Sum(item => item.TicketPrice);
                int sovetrongthang = result.Count();

                var details = _dbContext.Tickets
            .Join(
                _dbContext.Chuyenbays,
                ticket => ticket.FlyId,
                flight => flight.FlyId,
                (ticket, flight) => new
                {
                    TId = ticket.TId,
                    Cccd = ticket.Cccd,
                    Name = ticket.Name,
                    FlyId = flight.FlyId,
                    DepartureDay = flight.DepartureDay,
                    Price = ticket.TicketPrice
                }
            )
            .Where(item => item.DepartureDay.Substring(0, 4) == year && item.DepartureDay.Substring(5, 2) == month)
            .ToList();

                // You can return the result along with the total revenue
                return Ok(new { TotalRevenue = totalRevenue, Details = details , Sovetrongthang = sovetrongthang});
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpGet("GetDetails")]
        public IActionResult GetDetails()
        {
            try
            {
                // Assuming you have models for Ticket and Flight in your DbContext
                var details = _dbContext.Tickets
                    .Join(
                        _dbContext.Chuyenbays,
                        ticket => ticket.FlyId,
                        flight => flight.FlyId,
                        (ticket, flight) => new
                        {
                            TId = ticket.TId,
                            Cccd = ticket.Cccd,
                            Name = ticket.Name,
                            FlyId = flight.FlyId,
                            DepartureDay = flight.DepartureDay,
                            Price = flight.OriginalPrice
                        }
                    )
                    .ToList();

                return Ok(details);
            }
            catch (Exception ex)
            {
                // Log or handle exceptions
                return StatusCode(500, "Internal Server Error");
            }
        }

    }
}
