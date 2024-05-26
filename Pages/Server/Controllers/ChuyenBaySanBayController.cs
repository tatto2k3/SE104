using BlueStarMVC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace BlueStarMVC.Pages.Server.Controllers
{
    [Route("api/chuyenbaysanbay")]
    [ApiController]
    public class ChuyenBaySanBayController : ControllerBase
    {
        private readonly BluestarContext _dbContext;

        public ChuyenBaySanBayController(BluestarContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpGet]
        [Route("GetChuyenbay_Sanbays")]
        public IActionResult GetChuyenbay_Sanbays()
        {
            try
            {
                var chuyenbay_sanbays = _dbContext.Chuyenbay_Sanbays.OrderByDescending(cs => cs.FlyId).ToList();
                return Ok(chuyenbay_sanbays);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpGet]
        [Route("GetFlyID")]
        public IActionResult GetFlyID()
        {
            try
            {
                var currentDate = DateTime.Now.Date;
                var dateFormat = "yyyy-MM-dd";

                var flyIDs = _dbContext.Chuyenbays
                    .AsEnumerable()
                    .Where(cb => DateTime.TryParseExact(cb.DepartureDay, dateFormat, null, System.Globalization.DateTimeStyles.None, out var departureDate) && departureDate.Date > currentDate)
                    .Select(cb => cb.FlyId)
                    .Distinct()
                    .ToList();

                return Ok(flyIDs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }



        [HttpGet]
        [Route("GetAirportID")]
        public IActionResult GetAirportID()
        {
            try
            {
                var flyIDs = _dbContext.Sanbays.Select(cs => cs.AirportId).Distinct().ToList();
                return Ok(flyIDs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        [HttpPost]
        [Route("AddChuyenbay_Sanbays")]
        public IActionResult AddChuyenbay_Sanbays([FromBody] Chuyenbay_Sanbay chuyenbay_sanbay)
        {
            if (chuyenbay_sanbay == null)
            {
                return BadRequest("Invalid luggage data");
            }

            try
            {
                int count = _dbContext.Chuyenbay_Sanbays.Count(p => p.FlyId == chuyenbay_sanbay.FlyId);
                var numAirportRule = _dbContext.Parameters.FirstOrDefault(p => p.Label == "Số sân bay trung gian tối đa");

                if (numAirportRule != null)
                {
                    int numRule = (int) numAirportRule.Value;
                    if (count >= numRule) return StatusCode(500, "Số lượng sân bay trung gian đã đạt mức tối đa.");
                }

                var timeMinRule = _dbContext.Parameters.FirstOrDefault(p => p.Label == "Thời gian dừng tối thiểu");
                var timeMaxRule = _dbContext.Parameters.FirstOrDefault(p => p.Label == "Thời gian dừng tối đa");
                    
                if (timeMinRule != null)
                {
                    int timeRule = (int) timeMinRule.Value;
                    if (chuyenbay_sanbay.Time < timeRule) return StatusCode(500, "Thời gian dừng nhỏ hơn mức cho phép");
                }

                if (timeMaxRule != null)
                {
                    int timeRule = (int)timeMaxRule.Value;
                    if (chuyenbay_sanbay.Time > timeRule) return StatusCode(500, "Thời gian dừng lớn hơn mức cho phép");
                }

                _dbContext.Chuyenbay_Sanbays.Add(chuyenbay_sanbay);
                _dbContext.SaveChanges();
                return Ok("Luggage added successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("GetLuggageDetails")]
        public async Task<IActionResult> GetLuggageDetails(string FlyId, string AirportId)
        {
            try
            {
                if (string.IsNullOrEmpty(FlyId) || string.IsNullOrEmpty(AirportId))
                {
                    return BadRequest("Thông tin hành lý không hợp lệ");
                }

                var luggageDetails = await _dbContext.Chuyenbay_Sanbays.FirstOrDefaultAsync(c => c.FlyId == FlyId && c.AirportId == AirportId);

                if (luggageDetails == null)
                {
                    return NotFound("Không tìm thấy thông tin hành lý");
                }

                return Ok(luggageDetails);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi máy chủ nội bộ: {ex.Message}");
            }
        }



        [HttpPut]
        [Route("UpdateLuggage")]
        public async Task<IActionResult> UpdateLuggage(Chuyenbay_Sanbay objChuyenbay_Sanbay)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var existingLuggage = await _dbContext.Chuyenbay_Sanbays
                    .FirstOrDefaultAsync(c => c.FlyId == objChuyenbay_Sanbay.FlyId && c.AirportId == objChuyenbay_Sanbay.AirportId);

                if (existingLuggage == null)
                {
                    return NotFound("Luggage not found");
                }



                var timeMinRule = _dbContext.Parameters.FirstOrDefault(p => p.Label == "Thời gian dừng tối thiểu");
                var timeMaxRule = _dbContext.Parameters.FirstOrDefault(p => p.Label == "Thời gian dừng tối đa");

                if (timeMinRule != null)
                {
                    int timeRule = (int)timeMinRule.Value;
                    if (objChuyenbay_Sanbay.Time < timeRule) return StatusCode(500, "Thời gian dừng nhỏ hơn mức cho phép");
                }

                if (timeMaxRule != null)
                {
                    int timeRule = (int)timeMaxRule.Value;
                    if (objChuyenbay_Sanbay.Time > timeRule) return StatusCode(500, "Thời gian dừng lớn hơn mức cho phép");
                }

                DateTime flightDay;
                DateTime departureDay;

                // Lấy ngày khởi hành của chuyến bay
                var flightRule = _dbContext.Chuyenbays.FirstOrDefault(p => p.FlyId == objChuyenbay_Sanbay.FlyId);
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

                        if (difference.Days < 0) return StatusCode(500, "Không thể sửa do quá hạn");
                    }
                    else
                    {
                        return StatusCode(500, "Không thể sửa do quá hạn");
                    }
                }
                else
                {
                    return StatusCode(500, "Không thể sửa vé do quá hạn");
                }

                existingLuggage.Time = objChuyenbay_Sanbay.Time;
                existingLuggage.Note = objChuyenbay_Sanbay.Note;

                await _dbContext.SaveChangesAsync();

                return Ok(existingLuggage);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        [HttpDelete]
        [Route("DeleteLuggage")]
        public async Task<ActionResult> DeleteLuggage([FromBody] Chuyenbay_Sanbay luggage)
        {
            if (luggage == null || string.IsNullOrEmpty(luggage.FlyId) || string.IsNullOrEmpty(luggage.AirportId))
            {
                return BadRequest("No luggage provided");
            }

            try
            {
                var luggageToDelete = await _dbContext.Chuyenbay_Sanbays.FirstOrDefaultAsync(c => c.FlyId == luggage.FlyId && c.AirportId == luggage.AirportId);
                if (luggageToDelete == null)
                {
                    return NotFound("Luggage not found");
                }

                _dbContext.Chuyenbay_Sanbays.Remove(luggageToDelete);
                await _dbContext.SaveChangesAsync();

                return Ok("Luggage deleted successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Failed to delete luggage: {ex.Message}");
            }
        }



        [HttpGet]
        [Route("SearchLuggages")]
        public IActionResult SearchLuggages([FromQuery] string searchKeyword)
        {
            try
            {
                if (string.IsNullOrEmpty(searchKeyword))
                {
                    return BadRequest("Invalid search keyword");
                }

                var searchResults = _dbContext.Chuyenbay_Sanbays
                    .Where(c => c.FlyId.Contains(searchKeyword) || c.AirportId.Contains(searchKeyword))
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

