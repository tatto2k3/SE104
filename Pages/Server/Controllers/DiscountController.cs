using BlueStarMVC.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlueStarMVC.Pages.Server.Controllers
{
    [Route("api/discount")]
    [ApiController]
    public class DiscountController : ControllerBase
    {
        private readonly BluestarContext _dbContext;
        public DiscountController(BluestarContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpGet]
        [Route("GetDiscounts")]
        public IActionResult GetDiscounts()
        {
            List<Discount> list = _dbContext.Discounts.ToList();
            return StatusCode(StatusCodes.Status200OK, list);
        }

        [HttpPost]
        [Route("AddDiscount")]
        public IActionResult Adddiscount([FromBody] Discount discount)
        {
            if (discount == null)
            {
                return BadRequest("Invalid discount data");
            }

            try
            {
                _dbContext.Discounts.Add(discount);
                _dbContext.SaveChanges();
                return Ok("discount added successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpGet]
        [Route("GetDiscountDetails")]
        public IActionResult GetDiscountDetails([FromQuery] string dIds)
        {
            try
            {
                if (string.IsNullOrEmpty(dIds))
                {
                    return BadRequest("Invalid discount IDs");
                }

                var discountIds = dIds.Split(',');

                var discountDetails = _dbContext.Discounts.Where(c => discountIds.Contains(c.DId)).ToList();

                return Ok(discountDetails);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("GetDiscountPercent")]
        public IActionResult GetDiscountPercent([FromQuery] string dIds)
        {
            try
            {
                if (string.IsNullOrEmpty(dIds))
                {
                    return BadRequest("Invalid discount IDs");
                }

                var discountIds = dIds.Split(',');

                var discountDetails = _dbContext.Discounts
                    .Where(c => discountIds.Contains(c.DId))
                    .ToList();

                // Extract DPercent values
                var discountPercentages = discountDetails.Select(d => d.DPercent).ToList();

                return Ok(discountPercentages);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        [HttpGet]
        [Route("GetDiscountById")]
        public async Task<IActionResult> GetDiscountById([FromQuery] string discountID)
        {
            var result = await _dbContext.Discounts
            .FirstOrDefaultAsync(d => d.DId == discountID);
            return Ok(result);
        }

        [HttpPut]
        [Route("UpdateDiscount")]
        public async Task<IActionResult> UpdateDiscount(Discount objDiscount)
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
                var existingDiscount = await _dbContext.Discounts.FindAsync(objDiscount.DId);

                if (existingDiscount == null)
                {
                    return NotFound("discount not found");
                }

                // Cập nhật thông tin của khách hàng từ dữ liệu mới
                existingDiscount.DName = objDiscount.DName;
                existingDiscount.DPercent = objDiscount.DPercent;
                existingDiscount.DStart = objDiscount.DStart;
                existingDiscount.DFinish = objDiscount.DFinish;

                // Lưu các thay đổi vào cơ sở dữ liệu
                await _dbContext.SaveChangesAsync();

                // Trả về thông tin khách hàng đã được cập nhật
                return Ok(existingDiscount);
            }
            catch (Exception ex)
            {
                // Xử lý lỗi và trả về lỗi 500 nếu có lỗi xảy ra
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteDiscounts([FromBody] List<string> discountIds)
        {
            if (discountIds == null || !discountIds.Any())
            {
                return BadRequest("No discount IDs provided");
            }

            var discounts = await _dbContext.Discounts.Where(c => discountIds.Contains(c.DId)).ToListAsync();
            if (!discounts.Any())
            {
                return NotFound("No matching discounts found");
            }

            _dbContext.Discounts.RemoveRange(discounts);
            await _dbContext.SaveChangesAsync();
            return Ok("discounts deleted successfully");
        }

        [HttpGet("GetDiscountCount")]
        public async Task<ActionResult<int>> GetDiscountCount()
        {
            try
            {
                var discountCount = await _dbContext.Discounts.CountAsync();
                return Ok(new { Count = discountCount });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpGet]
        [Route("SearchDiscounts")]
        public IActionResult SearchDiscounts([FromQuery] string searchKeyword)
        {
            try
            {
                if (string.IsNullOrEmpty(searchKeyword))
                {
                    return BadRequest("Invalid search keyword");
                }

                // Search customers by name containing the provided keyword
                var searchResults = _dbContext.Discounts
                .Where(c => c.DId.Contains(searchKeyword) || c.DName.Contains(searchKeyword) || c.DStart.Contains(searchKeyword) || c.DFinish.Contains(searchKeyword))
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