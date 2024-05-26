using BlueStarMVC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlueStarMVC.Pages.Server.Controllers
{
    [Route("api/food")]
    [ApiController]
    public class FoodController : ControllerBase
    {
        private readonly BluestarContext _dbContext;
        public FoodController(BluestarContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpGet]
        [Route("GetFoods")]
        public IActionResult GetFoods()
        {
            List<Food> list = _dbContext.Foods.ToList();
            return StatusCode(StatusCodes.Status200OK, list);
        }

        [HttpGet]
        [Route("GetAllFood")]
        public async Task<IActionResult> GetAllFood()
        {
            try
            {
                // Retrieve the first 6 items from the Food table
                var foods = await _dbContext.Foods.Take(6).ToListAsync();

                return Ok(foods);
            }
            catch (Exception ex)
            {
                // Handle exceptions appropriately, log, and return an error response
                return StatusCode(500, $"Lỗi server: {ex.Message}");
            }
        }

        [HttpPost]
        [Route("AddFood")]
        public IActionResult AddFood([FromBody] Food Food)
        {
            if (Food == null)
            {
                return BadRequest("Invalid Food data");
            }

            try
            {
                _dbContext.Foods.Add(Food);
                _dbContext.SaveChanges();
                return Ok("Food added successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpGet]
        [Route("GetFoodDetails")]
        public IActionResult GetFoodDetails([FromQuery] string fIds)
        {
            try
            {
                if (string.IsNullOrEmpty(fIds))
                {
                    return BadRequest("Invalid Food IDs");
                }

                var FoodIds = fIds.Split(',');

                var FoodDetails = _dbContext.Foods.Where(c => FoodIds.Contains(c.FId)).ToList();

                return Ok(FoodDetails);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpPut]
        [Route("UpdateFood")]
        public async Task<IActionResult> UpdateFood(Food objFood)
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
                var existingFood = await _dbContext.Foods.FindAsync(objFood.FId);

                if (existingFood == null)
                {
                    return NotFound("Food not found");
                }

                // Cập nhật thông tin của khách hàng từ dữ liệu mới
                existingFood.FId = objFood.FId;
                existingFood.FName = objFood.FName;
                existingFood.FPrice = objFood.FPrice;

                // Lưu các thay đổi vào cơ sở dữ liệu
                await _dbContext.SaveChangesAsync();

                // Trả về thông tin khách hàng đã được cập nhật
                return Ok(existingFood);
            }
            catch (Exception ex)
            {
                // Xử lý lỗi và trả về lỗi 500 nếu có lỗi xảy ra
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteFoods([FromBody] List<string> FoodIds)
        {
            if (FoodIds == null || !FoodIds.Any())
            {
                return BadRequest("No Food IDs provided");
            }

            var Foods = await _dbContext.Foods.Where(c => FoodIds.Contains(c.FId)).ToListAsync();
            if (!Foods.Any())
            {
                return NotFound("No matching Foods found");
            }

            _dbContext.Foods.RemoveRange(Foods);
            await _dbContext.SaveChangesAsync();
            return Ok("Foods deleted successfully");
        }

        [HttpGet]
        [Route("SearchFoods")]
        public IActionResult SearchFoods([FromQuery] string searchKeyword)
        {
            try
            {
                if (string.IsNullOrEmpty(searchKeyword))
                {
                    return BadRequest("Invalid search keyword");
                }

                // Search customers by name containing the provided keyword
                var searchResults = _dbContext.Foods
                .Where(c => c.FId.Contains(searchKeyword) || c.FName.Contains(searchKeyword) || c.FPrice.Contains(searchKeyword))
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