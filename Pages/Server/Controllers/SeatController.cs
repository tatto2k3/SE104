using BlueStarMVC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlueStarMVC.Pages.Server.Controllers
{
    [Route("api/seat")]
    [ApiController]
    public class SeatController : ControllerBase
    {
        private readonly BluestarContext _dbContext;
        public SeatController(BluestarContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpGet]
        [Route("GetSeats")]
        public IActionResult GetSeats()
        {
            List<Seat> list = _dbContext.Seats.ToList();
            return StatusCode(StatusCodes.Status200OK, list);
        }

        [HttpPost]
        [Route("AddSeats")]
        public IActionResult AddCustomer([FromBody] Seat seat)
        {
            if (seat == null)
            {
                return BadRequest("Invalid sanbay data");
            }

            try
            {
                _dbContext.Seats.Add(seat);
                _dbContext.SaveChanges();
                return Ok("seat added successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpGet]
        [Route("GetSeatDetails")]
        public IActionResult GetCustomerDetails([FromQuery] string seatIds)
        {
            try
            {
                if (string.IsNullOrEmpty(seatIds))
                {
                    return BadRequest("Invalid customer IDs");
                }

                var sanbayIds = seatIds.Split(',');

                var sanbayDetails = _dbContext.Seats.Where(c => sanbayIds.Contains(c.SeatID)).ToList();

                return Ok(sanbayDetails);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpPut]
        [Route("UpdateSanbay")]
        public async Task<IActionResult> UpdateSanbay(Seat objSanbay)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                    {
                        Console.WriteLine(error.ErrorMessage);
                    }
                    return BadRequest(ModelState);
                }

                var existingSanbay = await _dbContext.Seats.FindAsync(objSanbay.SeatID);

                if (existingSanbay == null)
                {
                    return NotFound("Customer not found");
                }


                existingSanbay.percent = objSanbay.percent;

                await _dbContext.SaveChangesAsync();


                return Ok(existingSanbay);
            }
            catch (Exception ex)
            {
                // Xử lý lỗi và trả về lỗi 500 nếu có lỗi xảy ra
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteSanbays([FromBody] List<string> seatIds)
        {
            if (seatIds == null || !seatIds.Any())
            {
                return BadRequest("No sanbay IDs provided");
            }

            var sanbays = await _dbContext.Seats.Where(c => seatIds.Contains(c.SeatID)).ToListAsync();
            if (!sanbays.Any())
            {
                return NotFound("No matching customers found");
            }

            _dbContext.Seats.RemoveRange(sanbays);
            await _dbContext.SaveChangesAsync();
            return Ok("Customers deleted successfully");
        }
        [HttpGet]
        [Route("SearchSanbays")]
        public IActionResult SearchSanbays([FromQuery] string searchKeyword)
        {
            try
            {
                if (string.IsNullOrEmpty(searchKeyword))
                {
                    return BadRequest("Invalid search keyword");
                }

                // Search customers by name containing the provided keyword
                var searchResults = _dbContext.Sanbays
                .Where(c => c.AirportId.Contains(searchKeyword) || c.AirportName.Contains(searchKeyword) || c.Place.Contains(searchKeyword))
                .ToList();

                return Ok(searchResults);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

    }
}
