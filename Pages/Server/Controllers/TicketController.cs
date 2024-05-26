using BlueStarMVC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Net.Sockets;

namespace BlueStarMVC.Pages.Server.Controllers
{
    [Route("api/ticket")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly BluestarContext _dbContext;
        public TicketController(BluestarContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpGet]
        [Route("GetTickets")]
        public IActionResult GetTickets()
        {
            List<Ticket> list = _dbContext.Tickets.OrderByDescending(cb => cb.FlyId).ToList();
            return StatusCode(StatusCodes.Status200OK, list);
        }
        [HttpGet]
        [Route("GetSeatID")]
        public IActionResult GetSeatID()
        {
            try
            {
                var flyIDs = _dbContext.Seats.Select(cs => cs.SeatID).Distinct().ToList();
                return Ok(flyIDs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpPost]
        [Route("AddTicket")]
        public IActionResult AddTicket([FromBody] Ticket ticket)
        {
            if (ticket == null)
            {
                return BadRequest("Invalid Ticket data");
            }

            try
            {
                DateTime flightDay; // Đây là ngày khởi hành của chuyến bay
                DateTime departureDay; // Đây là ngày hiện tại

                var dateBooked = _dbContext.Parameters.FirstOrDefault(p => p.Label == "Thời gian chậm nhất khi đặt vé");
                int extractDay = 0;
                if(dateBooked != null)
                {
                    extractDay = (int) dateBooked.Value;
                }

                // Lấy ngày khởi hành của chuyến bay
                var flightRule = _dbContext.Chuyenbays.FirstOrDefault(p => p.FlyId == ticket.FlyId);
                if (flightRule != null)
                {
                    if (DateTime.TryParseExact(flightRule.DepartureDay, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out flightDay))
                    {
                        // Lấy ngày hiện tại
                        departureDay = DateTime.Now.Date;

                        // Lấy chỉ ngày của ngày khởi hành của chuyến bay
                        DateTime flightDateOnly = flightDay.Date;

                        // Tính toán độ chênh lệch giữa hai ngày
                        TimeSpan difference = flightDateOnly - departureDay;

                        // Lấy giá trị tuyệt đối của độ chênh lệch (nếu muốn)
                        TimeSpan absoluteDifference = difference.Duration();

                        if (difference.Days < extractDay) return StatusCode(500, "Không thể đặt vé do quá hạn");
                    }
                    else
                    {
                        return BadRequest("Invalid date format");
                    }
                }
                else
                {
                    return BadRequest("Flight not found");
                }



                // Retrieve the corresponding seat type and its percentage
                var seatType = _dbContext.Seats.FirstOrDefault(s => s.SeatID == ticket.Seat_Type_ID);
                if (seatType == null)
                {
                    return BadRequest("Invalid Seat Type");
                }

                // Retrieve the flight price
                var flight = _dbContext.Chuyenbays.FirstOrDefault(c => c.FlyId == ticket.FlyId);
                if (flight == null)
                {
                    return BadRequest("Invalid Flight");
                }

                float percentage = seatType.percent / 100.0f;
                int ticketPrice = (int)(flight.OriginalPrice * percentage);

                ticket.TicketPrice = ticketPrice;


                var fly = _dbContext.Chuyenbays.FirstOrDefault(p => p.FlyId == ticket.FlyId);
                fly.SeatEmpty = fly.SeatEmpty - 1;

                _dbContext.Tickets.Add(ticket);
                _dbContext.SaveChanges();

                return Ok("Ticket added successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("GetTicketDetails")]
        public IActionResult GetTicketDetails([FromQuery] string tIds)
        {
            try
            {
                
                if (string.IsNullOrEmpty(tIds))
                {
                    return BadRequest("Invalid Ticket IDs");
                }

                var ticketIds = tIds.Split(',');

                var ticketDetails = _dbContext.Tickets.Where(c => ticketIds.Contains(c.TId)).ToList();

                return Ok(ticketDetails);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpPut]
        [Route("UpdateTicket")]
        public async Task<IActionResult> UpdateTicket(Ticket objTicket)
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
                // Tìm kiếm khách hàng dựa trên id (hoặc mã khách hàng, tùy thuộc vào cách bạn xác định)
                var existingTicket = await _dbContext.Tickets.FindAsync(objTicket.TId);

                if (existingTicket == null)
                {
                    return NotFound("Ticket not found");
                }

                DateTime flightDay; // Đây là ngày khởi hành của chuyến bay
                DateTime departureDay; // Đây là ngày hiện tại

                // Lấy ngày khởi hành của chuyến bay
                var flightRule = _dbContext.Chuyenbays.FirstOrDefault(p => p.FlyId == objTicket.FlyId);
                if (flightRule != null)
                {
                    if (DateTime.TryParseExact(flightRule.DepartureDay, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out flightDay))
                    {
                        // Lấy ngày hiện tại
                        departureDay = DateTime.Now.Date;

                        // Lấy chỉ ngày của ngày khởi hành của chuyến bay
                        DateTime flightDateOnly = flightDay.Date;

                        // Tính toán độ chênh lệch giữa hai ngày
                        TimeSpan difference = flightDateOnly - departureDay;

                        // Lấy giá trị tuyệt đối của độ chênh lệch (nếu muốn)
                        TimeSpan absoluteDifference = difference.Duration();

                        if (difference.Days < 0) return StatusCode(500, "Không thể sửa vé do quá hạn");
                    }
                    else
                    {
                        return StatusCode(500, "Không thể sửa vé do quá hạn");
                    }
                }
                else
                {
                    return StatusCode(500, "Không thể sửa vé do quá hạn");
                }

                // Cập nhật thông tin của khách hàng từ dữ liệu mới
                existingTicket.TId = objTicket.TId;
                existingTicket.Cccd = objTicket.Cccd;
                existingTicket.Name = objTicket.Name;
                existingTicket.FlyId = objTicket.FlyId;
           
                existingTicket.SDT = objTicket.SDT;
                existingTicket.Seat_Type_ID = objTicket.Seat_Type_ID;
                

                var flight = _dbContext.Chuyenbays.FirstOrDefault(c => c.FlyId == objTicket.FlyId);
                var seatType = _dbContext.Seats.FirstOrDefault(s => s.SeatID == objTicket.Seat_Type_ID);
                float percentage = seatType.percent / 100.0f;
                int ticketPrice = (int)(flight.OriginalPrice * percentage);
                existingTicket.TicketPrice = ticketPrice;



                // Lưu các thay đổi vào cơ sở dữ liệu
                await _dbContext.SaveChangesAsync();

                // Trả về thông tin khách hàng đã được cập nhật
                return Ok(existingTicket);
            }
            catch (Exception ex)
            {
                // Xử lý lỗi và trả về lỗi 500 nếu có lỗi xảy ra
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteTickets([FromQuery] string ticketIds)
        {

            var Tickets = await _dbContext.Tickets.Where(c => ticketIds.Contains(c.TId)).ToListAsync();

            DateTime flightDay; // Đây là ngày khởi hành của chuyến bay
            DateTime departureDay; // Đây là ngày hiện tại

            var dateBooked = _dbContext.Parameters.FirstOrDefault(p => p.Label == "Thời gian chậm nhất khi hủy vé");
            int extractDay = 0;
            if (dateBooked != null)
            {
                extractDay = (int)dateBooked.Value;
            }

            // Lấy ngày khởi hành của chuyến bay
            var flightRule = _dbContext.Chuyenbays.FirstOrDefault(p => p.FlyId == Tickets[0].FlyId);
            if (flightRule != null)
            {
                if (DateTime.TryParseExact(flightRule.DepartureDay, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out flightDay))
                {
                    // Lấy ngày hiện tại
                    departureDay = DateTime.Now.Date;

                    // Lấy chỉ ngày của ngày khởi hành của chuyến bay
                    DateTime flightDateOnly = flightDay.Date;

                    // Tính toán độ chênh lệch giữa hai ngày
                    TimeSpan difference = flightDateOnly - departureDay;

                    // Lấy giá trị tuyệt đối của độ chênh lệch (nếu muốn)
                    TimeSpan absoluteDifference = difference.Duration();

                    if (difference.Days < extractDay) return StatusCode(500, "Không thể hủy vé do quá hạn");
                }
                else
                {
                    return StatusCode(500, "Không thể hủy vé do quá hạn");
                }
            }
            else
            {
                return StatusCode(500, "Không thể hủy vé do quá hạn");
            }


            _dbContext.Tickets.RemoveRange(Tickets);
            await _dbContext.SaveChangesAsync();
            return Ok("Tickets deleted successfully");
        }

        [HttpGet]
        [Route("SearchTickets")]
        public IActionResult SearchTickets([FromQuery] string searchKeyword)
        {
            try
            {
                if (string.IsNullOrEmpty(searchKeyword))
                {
                    return BadRequest("Invalid search keyword");
                }

                // Search customers by name containing the provided keyword
                var searchResults = _dbContext.Tickets
                .Where(c => c.Cccd.Contains(searchKeyword) || c.Name.Contains(searchKeyword) || c.FlyId.Contains(searchKeyword) || c.TId.Contains(searchKeyword) ||  c.Seat_Type_ID.Contains(searchKeyword))
                .ToList();

                return Ok(searchResults);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("GetTicketReviewDetails")]
        public IActionResult GetTicketReviewDetails([FromQuery] string name)
        {
            try
            {
                if (string.IsNullOrEmpty(name))
                {
                    return BadRequest("Invalid name");
                }

                var ticketReviewDetails = _dbContext.Tickets
                    .Where(t => t.Name == name) // Sửa đổi dòng này để tìm kiếm theo tên
                    .Join(_dbContext.Chuyenbays,
                          ticket => ticket.FlyId,
                          chuyenbay => chuyenbay.FlyId,
                          (ticket, chuyenbay) => new
                          {
                              ticket.Name,
                              ticket.Cccd,
                              ticket.Seat_Type_ID,
                              ticket.FlyId,
                              chuyenbay.DepartureDay,
                              chuyenbay.DepartureTime,
                              chuyenbay.FlightTime
                          })
                    .FirstOrDefault();

                if (ticketReviewDetails == null)
                {
                    return NotFound("Ticket review details not found");
                }

                return Ok(ticketReviewDetails);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

    }
}
