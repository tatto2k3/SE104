using BlueStarMVC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlueStarMVC.Pages.Server.Controllers
{
    [Route("api/plane")]
    [ApiController]
    public class PlaneController : ControllerBase
    {
        private readonly BluestarContext _dbContext;
        public PlaneController(BluestarContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpGet]
        [Route("GetPlanes")]
        public IActionResult GetPlanes()
        {
            List<Plane> list = _dbContext.Planes.ToList();
            return StatusCode(StatusCodes.Status200OK, list);
        }
        [HttpPost]
        [Route("AddPlane")]
        public IActionResult AddPlane([FromBody] Plane plane)
        {
            if (plane == null)
            {
                return BadRequest("Invalid plane data");
            }

            try
            {
                _dbContext.Planes.Add(plane);
                _dbContext.SaveChanges();
                return Ok("Customer added successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpGet]
        [Route("GetPlaneDetails")]
        public IActionResult GetPlaneDetails([FromQuery] string plIds)
        {
            try
            {
                if (string.IsNullOrEmpty(plIds))
                {
                    return BadRequest("Invalid customer IDs");
                }

                var planeIds = plIds.Split(',');

                var planeDetails = _dbContext.Planes.Where(c => planeIds.Contains(c.PlId)).ToList();

                return Ok(planeDetails);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpPut]
        [Route("UpdatePlane")]
        public async Task<IActionResult> UpdatePlane(Plane objPlane)
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
                var existingPlane = await _dbContext.Planes.FindAsync(objPlane.PlId);

                if (existingPlane == null)
                {
                    return NotFound("Plane not found");
                }

                // Cập nhật thông tin của khách hàng từ dữ liệu mới
                existingPlane.Typeofplane = objPlane.Typeofplane;
                existingPlane.BusinessCapacity = objPlane.BusinessCapacity;
                existingPlane.EconomyCapacity = objPlane.EconomyCapacity;


                // Lưu các thay đổi vào cơ sở dữ liệu
                await _dbContext.SaveChangesAsync();

                // Trả về thông tin khách hàng đã được cập nhật
                return Ok(existingPlane);
            }
            catch (Exception ex)
            {
                // Xử lý lỗi và trả về lỗi 500 nếu có lỗi xảy ra
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete]
        public async Task<ActionResult> DeletePlanes([FromBody] List<string> planeIds)
        {
            if (planeIds == null || !planeIds.Any())
            {
                return BadRequest("No plane IDs provided");
            }

            var planes = await _dbContext.Planes.Where(c => planeIds.Contains(c.PlId)).ToListAsync();
            if (!planes.Any())
            {
                return NotFound("No matching planes found");
            }

            _dbContext.Planes.RemoveRange(planes);
            await _dbContext.SaveChangesAsync();
            return Ok("Customers deleted successfully");
        }
        [HttpGet]
        [Route("SearchPlanes")]
        public IActionResult SearchPlanes([FromQuery] string searchKeyword)
        {
            try
            {
                if (string.IsNullOrEmpty(searchKeyword))
                {
                    return BadRequest("Invalid search keyword");
                }

                // Search customers by name containing the provided keyword
                var searchResults = _dbContext.Planes
                .Where(c => c.PlId.Contains(searchKeyword) || c.Typeofplane.Contains(searchKeyword))
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
