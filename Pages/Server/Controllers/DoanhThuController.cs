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

                decimal totalRevenue = result.Sum(item => item.TicketPrice);


                var details = result
                    .GroupBy(item => item.DepartureDay.Substring(5, 2)) 
                    .Select(group => new
                    {
                        Month = group.Key,
                        NumberOfFlights = group.Select(g => g.FlyId).Distinct().Count(),
                        MonthlyRevenue = group.Sum(item => item.TicketPrice),
                        PercentageOfYearlyRevenue = (group.Sum(item => item.TicketPrice) / totalRevenue) * 100 
                    })
                    .OrderBy(g => g.Month)
                    .ToList();

                return Ok(new { TotalRevenue = totalRevenue, Details = details });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }


        [HttpGet]
        [Route("GetDoanhThuThang")]
        public IActionResult GetDoanhThuThang([FromQuery] string year, string month)
        {
            try
            {

                if (string.IsNullOrEmpty(year) || string.IsNullOrEmpty(month))
                {
                    return BadRequest("Year and month parameters are required.");
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

 
                decimal totalRevenue = result.Sum(item => item.TicketPrice);
                int sovetrongthang = result.Count();


                var allFlight = _dbContext.Chuyenbays.Where(chuyenBay => chuyenBay.DepartureDay.Substring(0, 4) == year && chuyenBay.DepartureDay.Substring(5, 2) == month)
                    .Select(chuyenbay => chuyenbay.FlyId).ToList();






                var details = result
                    .GroupBy(item => new { item.FlyId, item.DepartureDay })
                    .Select(group => new
                    {
                        FlyId = group.Key.FlyId,
                        DepartureDay = group.Key.DepartureDay,
                        sovemoichuyenbay = group.Count(),
                        DoanhThu = group.Sum(item => item.TicketPrice),
                        TyLe = (group.Sum(item => item.TicketPrice) / totalRevenue) * 100 
                    })
                    .ToList();

      
                return Ok(new { TotalRevenue = totalRevenue, Details = details, Sovetrongthang = sovetrongthang });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }


        [HttpGet("GetDetails")]
        public IActionResult GetDetails()
        {
            try
            {
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
