using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BlueStarMVC.Models;
using Microsoft.EntityFrameworkCore;
using BlueStarMVC.Pages.Server.DTOs;
using System.Globalization;
using System.Net.Sockets;

namespace BlueStarMVC.Pages.Server.Controllers
{
    [Route("api/chuyenbay")]
    [ApiController]
    public class ChuyenbayController : ControllerBase
    {
        private readonly BluestarContext _dbContext;
        public ChuyenbayController(BluestarContext dbContext)
        {
            _dbContext = dbContext;
        }


        [HttpGet]
        [Route("GetChuyenbays")]
        public IActionResult GetChuyenbays()
        {

            List<Chuyenbay> list = _dbContext.Chuyenbays.OrderByDescending(cb => cb.DepartureDay).ToList();

            List<ChuyenbayDTO> chuyenbayDTOs = new List<ChuyenbayDTO>();

            


            foreach (var chuyenbay in list)
            {









                int countTicket = _dbContext.Tickets.Count(p => p.FlyId == chuyenbay.FlyId);
                chuyenbay.SeatBooked = countTicket;
                


                var trunggianList = _dbContext.Chuyenbay_Sanbays
                    .Where(cs => cs.FlyId == chuyenbay.FlyId)
                    .Select(cs => cs.AirportId)
                    .ToList();



                ChuyenbayDTO chuyenbayDTO = new ChuyenbayDTO
                {
                    FlyId = chuyenbay.FlyId,
                    OriginalPrice = chuyenbay.OriginalPrice,
                    FromLocation = chuyenbay.FromLocation,
                    ToLocation = chuyenbay.ToLocation,
                    DepartureTime = chuyenbay.DepartureTime,
                    DepartureDay = chuyenbay.DepartureDay,
                    FlightTime = chuyenbay.FlightTime,
                    SeatBooked = countTicket,
                    SeatEmpty = chuyenbay.SeatEmpty,
                    TrungGian = trunggianList 
                };

                // Thêm DTO vào danh sách chuyến bay kết quả
                chuyenbayDTOs.Add(chuyenbayDTO);
            }

            // Trả về danh sách chuyến bay kèm thông tin sân bay trung gian
            return StatusCode(200, chuyenbayDTOs);
        }

        [HttpGet]
        [Route("GetPlace")]
        public IActionResult GetPlace()
        {
            try
            {
                var flyIDs = _dbContext.Sanbays.Select(cs => cs.Place).Distinct().ToList();
                return Ok(flyIDs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        [HttpPost]
        [Route("AddChuyenbay")]
        public IActionResult AddChuyenbay([FromBody] Chuyenbay chuyenbay)
        {
            if (chuyenbay == null)
            {
                return BadRequest("Invalid chuyenbay data");
            }

            try
            {
                int hour = int.Parse(chuyenbay.FlightTime.Substring(0, 2));
                int minutes = int.Parse(chuyenbay.FlightTime.Substring(3, 2));
                int timeFlight = hour * 60 + minutes;

                var timeRuleParameter = _dbContext.Parameters.FirstOrDefault(p => p.Label == "Thời gian bay tối thiểu");

                if (timeRuleParameter != null)
                {
                    int timeRule = (int) timeRuleParameter.Value;
                    if (timeFlight < timeRule) return StatusCode(500, "Thời gian bay nhỏ hơn thời gian bay tối thiểu.");
                }

                if (chuyenbay.ToLocation == chuyenbay.FromLocation)
                {
                    return StatusCode(500, "Điểm đến và điểm đi không được trùng nhau.");
                }


                _dbContext.Chuyenbays.Add(chuyenbay);
                _dbContext.SaveChanges();
                return Ok("chuyenbay added successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpGet]
        [Route("GetChuyenbayDetails")]
        public IActionResult GetChuyenbayDetails([FromQuery] string flyIds)
        {
            try
            {
                if (string.IsNullOrEmpty(flyIds))
                {
                    return BadRequest("Invalid chuyenbay IDs");
                }

                var chuyenbayIds = flyIds.Split(',');

                var chuyenbayDetails = _dbContext.Chuyenbays.Where(c => chuyenbayIds.Contains(c.FlyId)).ToList();

                var trunggianList = _dbContext.Chuyenbay_Sanbays
                    .Where(cs => cs.FlyId == flyIds)
                    .Select(cs => cs.AirportId)
                    .ToList();

                var responseData = new
                {
                    chuyenbayDetails = chuyenbayDetails,
                    trunggianList = trunggianList
                };

                return Ok(responseData);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpPut]
        [Route("UpdateChuyenbay")]
        public async Task<IActionResult> UpdateChuyenbay(Chuyenbay objChuyenbay)
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
                var existingChuyenbay = await _dbContext.Chuyenbays.FindAsync(objChuyenbay.FlyId);

                if (existingChuyenbay == null)
                {
                    return NotFound("chuyenbay not found");
                }

                int hour = int.Parse(objChuyenbay.FlightTime.Substring(0, 2));
                int minutes = int.Parse(objChuyenbay.FlightTime.Substring(3, 2));
                int timeFlight = hour * 60 + minutes;

                var timeRuleParameter = _dbContext.Parameters.FirstOrDefault(p => p.Label == "Thời gian bay tối thiểu");

                if (timeRuleParameter != null)
                {
                    int timeRule = (int)timeRuleParameter.Value;
                    if (timeFlight < timeRule) return StatusCode(500, "Thời gian bay nhỏ hơn thời gian bay tối thiểu.");
                }

                // Cập nhật thông tin của khách hàng từ dữ liệu mới
                existingChuyenbay.SeatEmpty = objChuyenbay.SeatEmpty;
                existingChuyenbay.FromLocation = objChuyenbay.FromLocation;
                existingChuyenbay.ToLocation = objChuyenbay.ToLocation;
                existingChuyenbay.OriginalPrice = objChuyenbay.OriginalPrice;
                existingChuyenbay.SeatBooked = objChuyenbay.SeatBooked;
                existingChuyenbay.DepartureTime = objChuyenbay.DepartureTime;
                existingChuyenbay.FlightTime = objChuyenbay.FlightTime;
                existingChuyenbay.DepartureDay = objChuyenbay.DepartureDay;

                // Lưu các thay đổi vào cơ sở dữ liệu
                await _dbContext.SaveChangesAsync();

                // Trả về thông tin khách hàng đã được cập nhật
                return Ok(existingChuyenbay);
            }
            catch (Exception ex)
            {
                // Xử lý lỗi và trả về lỗi 500 nếu có lỗi xảy ra
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteChuyenbays([FromBody] List<string> chuyenbayIds)
        {
            if (chuyenbayIds == null || !chuyenbayIds.Any())
            {
                return BadRequest("No chuyenbay IDs provided");
            }

            var chuyenbays = await _dbContext.Chuyenbays.Where(c => chuyenbayIds.Contains(c.FlyId)).ToListAsync();
            if (!chuyenbays.Any())
            {
                return NotFound("No matching chuyenbays found");
            }

            var chuyenbaysanbay = _dbContext.Chuyenbay_Sanbays.FirstOrDefault(p => p.FlyId == chuyenbays[0].FlyId);
            var ticket = _dbContext.Tickets.FirstOrDefault(p => p.FlyId == chuyenbays[0].FlyId);
            if (chuyenbaysanbay != null || ticket != null) return BadRequest("No chuyenbay IDs provided");



            _dbContext.Chuyenbays.RemoveRange(chuyenbays);
            await _dbContext.SaveChangesAsync();
            return Ok("chuyenbays deleted successfully");
        }
        [HttpGet]
        [Route("SearchChuyenbays")]
        public IActionResult SearchChuyenbays([FromQuery] string searchKeyword)
        {
            try
            {
                if (string.IsNullOrEmpty(searchKeyword))
                {
                    return BadRequest("Invalid search keyword");
                }

                // Search customers by name containing the provided keyword
                var searchResults = _dbContext.Chuyenbays
                .Where(c => c.FlyId.Contains(searchKeyword) || c.DepartureDay.Contains(searchKeyword)  || c.FromLocation.Contains(searchKeyword) || c.ToLocation.Contains(searchKeyword) )
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
