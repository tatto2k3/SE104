using BlueStarMVC.Models;
using BlueStarMVC.Pages.Server.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System.Globalization;

namespace BlueStarMVC.Pages.Server.Controllers
{
    [Route("api/parameters")]
    [ApiController]
    public class ParametersController : ControllerBase
    {
        private readonly BluestarContext _dbContext;
        public ParametersController(BluestarContext dbContext)
        {
            _dbContext = dbContext;
        }


        [HttpGet]
        [Route("GetParameters")]
        public IActionResult GetParameters()
        {
            List<Parameter> list = _dbContext.Parameters.ToList();
            return StatusCode(StatusCodes.Status200OK, list);
        }
        [HttpPost]
        [Route("AddParameter")]
        public IActionResult AddParameter([FromBody] Parameter Parameter)
        {
            if (Parameter == null)
            {
                return BadRequest("Invalid Parameter data");
            }

            try
            {
                _dbContext.Parameters.Add(Parameter);
                _dbContext.SaveChanges();
                return Ok("Parameter added successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpGet]
        [Route("GetParameterDetails")]
        public IActionResult GetParameterDetails([FromQuery] string dIds)
        {
            try
            {
                if (string.IsNullOrEmpty(dIds))
                {
                    return BadRequest("Invalid Parameter IDs");
                }

                var ParameterDetails = _dbContext.Parameters.FirstOrDefault(p => p.Label == dIds);

                return Ok(ParameterDetails);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut]
        [Route("UpdateParameter")]
        public async Task<IActionResult> UpdateParameter(Parameter objParameter)
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
                var existingParameter = await _dbContext.Parameters.FindAsync(objParameter.Label);

                if (existingParameter == null)
                {
                    return NotFound("Parameter not found");
                }

                if (existingParameter.Label == "Số lượng ghế hạng 1")
                {
                    List<Chuyenbay> list = _dbContext.Chuyenbays.ToList();

                    foreach (var chuyenbay in list)
                    {
                        int SeatBookedRule = 0;
                        int SeatEmptyRule = 0;

                        int Seat1 = 0;
                        int Seat2 = 0;


                        DateTime flightDay; // Đây là ngày khởi hành của chuyến bay
                        DateTime departureDay; // Đây là ngày hiện tại
                        if (DateTime.TryParseExact(chuyenbay.DepartureDay, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out flightDay))
                        {
                            // Lấy ngày hiện tại
                            departureDay = DateTime.Now.Date;

                            // Lấy chỉ ngày của ngày khởi hành của chuyến bay
                            DateTime flightDateOnly = flightDay.Date;

                            // Tính toán độ chênh lệch giữa hai ngày
                            TimeSpan difference = flightDateOnly - departureDay;

                            // Lấy giá trị tuyệt đối của độ chênh lệch (nếu muốn)
                            TimeSpan absoluteDifference = difference.Duration();

                            if (difference.Days > 0)
                            {
                                int SeatType1Rule = (int)objParameter.Value;
                                var SeatType2Rule = _dbContext.Parameters.FirstOrDefault(p => p.Label == "Số lượng ghế hạng 2");
                                if (SeatType1Rule != null) Seat1 = SeatType1Rule;
                                if (SeatType2Rule != null) Seat2 = (int)SeatType2Rule.Value;

                                SeatBookedRule = _dbContext.Tickets.Count(p => p.FlyId == chuyenbay.FlyId);

                                SeatEmptyRule = Seat1 + Seat2;

                                chuyenbay.SeatEmpty = SeatEmptyRule;
                                chuyenbay.SeatBooked = SeatBookedRule;
                                Storage.Seat1Before = Seat1;
                                Storage.Seat2Before = Seat2;
                            }
                        }
                    }
                }
                if (existingParameter.Label == "Số lượng ghế hạng 2")
                {
                    List<Chuyenbay> list = _dbContext.Chuyenbays.ToList();

                    foreach (var chuyenbay in list)
                    {
                        int SeatBookedRule = 0;
                        int SeatEmptyRule = 0;

                        int Seat1 = 0;
                        int Seat2 = 0;


                        DateTime flightDay; // Đây là ngày khởi hành của chuyến bay
                        DateTime departureDay; // Đây là ngày hiện tại
                        if (DateTime.TryParseExact(chuyenbay.DepartureDay, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out flightDay))
                        {
                            // Lấy ngày hiện tại
                            departureDay = DateTime.Now.Date;

                            // Lấy chỉ ngày của ngày khởi hành của chuyến bay
                            DateTime flightDateOnly = flightDay.Date;

                            // Tính toán độ chênh lệch giữa hai ngày
                            TimeSpan difference = flightDateOnly - departureDay;

                            // Lấy giá trị tuyệt đối của độ chênh lệch (nếu muốn)
                            TimeSpan absoluteDifference = difference.Duration();

                            if (difference.Days > 0)
                            {
                                var SeatType1Rule = _dbContext.Parameters.FirstOrDefault(p => p.Label == "Số lượng ghế hạng 1");
                                int SeatType2Rule = (int)objParameter.Value;
                                if (SeatType1Rule != null) Seat1 = (int) SeatType1Rule.Value;
                                if (SeatType2Rule != null) Seat2 = SeatType2Rule;

                                SeatBookedRule = _dbContext.Tickets.Count(p => p.FlyId == chuyenbay.FlyId);

                                SeatEmptyRule = Seat1 + Seat2 - SeatBookedRule;

                                chuyenbay.SeatEmpty = SeatEmptyRule;
                                chuyenbay.SeatBooked = SeatBookedRule;
                                Storage.Seat1Before = Seat1;
                                Storage.Seat2Before = Seat2;
                            }
                        }
                    }
                }

                // Cập nhật thông tin của khách hàng từ dữ liệu mới
                existingParameter.Value = objParameter.Value;

                // Lưu các thay đổi vào cơ sở dữ liệu
                await _dbContext.SaveChangesAsync();

                // Trả về thông tin khách hàng đã được cập nhật
                return Ok(existingParameter);
            }
            catch (Exception ex)
            {
                // Xử lý lỗi và trả về lỗi 500 nếu có lỗi xảy ra
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteParameters([FromBody] List<string> ParameterIds)
        {
            if (ParameterIds == null || !ParameterIds.Any())
            {
                return BadRequest("No Parameter IDs provided");
            }

            var Parameters = await _dbContext.Parameters.Where(c => ParameterIds.Contains(c.Label)).ToListAsync();
            if (!Parameters.Any())
            {
                return NotFound("No matching Parameters found");
            }

            _dbContext.Parameters.RemoveRange(Parameters);
            await _dbContext.SaveChangesAsync();
            return Ok("Parameters deleted successfully");
        }

        
        [HttpGet]
        [Route("SearchParameters")]
        public IActionResult SearchParameters([FromQuery] string searchKeyword)
        {
            try
            {
                if (string.IsNullOrEmpty(searchKeyword))
                {
                    return BadRequest("Invalid search keyword");
                }

                // Search customers by name containing the provided keyword
                var searchResults = _dbContext.Parameters
                .Where(c => c.Label.Contains(searchKeyword))
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
