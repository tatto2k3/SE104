using BlueStarMVC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlueStarMVC.Pages.Server.Controllers
{
    [Route("api/luggage")]
    [ApiController]
    public class LuggageController : ControllerBase
    {
        private readonly BluestarContext _dbContext;

        public LuggageController(BluestarContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        [Route("GetChuyenbay_Sanbays")]
        public IActionResult GetChuyenbay_Sanbays()
        {
            try
            {
                var chuyenbay_sanbays = _dbContext.Set<Chuyenbay_Sanbay>().ToList();
                return Ok(chuyenbay_sanbays);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        [Route("AddLuggage")]
        public IActionResult AddLuggage([FromBody] Chuyenbay_Sanbay chuyenbay_sanbay)
        {
            if (chuyenbay_sanbay == null)
            {
                return BadRequest("Invalid luggage data");
            }

            try
            {
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
        public IActionResult GetLuggageDetails([FromQuery] string luggageCode)
        {
            try
            {
                if (string.IsNullOrEmpty(luggageCode))
                {
                    return BadRequest("Invalid luggage IDs");
                }

                var luggageIds = luggageCode.Split(',');

                var luggageDetails = _dbContext.Chuyenbay_Sanbays.Where(c => luggageIds.Contains(c.FlyId)).ToList();

                return Ok(luggageDetails);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
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
                    foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                    {
                        Console.WriteLine(error.ErrorMessage);
                    }
                    return BadRequest(ModelState);
                }

                var existingLuggage = await _dbContext.Chuyenbay_Sanbays.FindAsync(objChuyenbay_Sanbay.FlyId);

                if (existingLuggage == null)
                {
                    return NotFound("Luggage not found");
                }

                existingLuggage.FlyId = objChuyenbay_Sanbay.FlyId;
                existingLuggage.AirportId = objChuyenbay_Sanbay.AirportId;
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
        [Route("DeleteLuggages")]
        public async Task<ActionResult> DeleteLuggages([FromBody] List<string> luggageIds)
        {
            if (luggageIds == null || !luggageIds.Any())
            {
                return BadRequest("No luggage IDs provided");
            }

            var luggages = await _dbContext.Chuyenbay_Sanbays.Where(c => luggageIds.Contains(c.FlyId)).ToListAsync();
            if (!luggages.Any())
            {
                return NotFound("No matching luggages found");
            }

            _dbContext.Chuyenbay_Sanbays.RemoveRange(luggages);
            await _dbContext.SaveChangesAsync();
            return Ok("Luggages deleted successfully");
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
