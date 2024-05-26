using BlueStarMVC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlueStarMVC.Pages.Server.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly BluestarContext _dbContext;
        public AccountController(BluestarContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] Account account)
        {
            var user_staff = _dbContext.Accounts
                .FirstOrDefault(a => a.Email == account.Email && a.Password == account.Password);


            if (user_staff != null)
            {
                return Ok(new { RedirectUrl = "/ChuyenBay" });
            }

            // Đăng nhập không thành công
            return BadRequest("Invalid credentials");
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] Account account)
        {
            try
            {
                // Kiểm tra xem email đã tồn tại chưa
                var existingUser = _dbContext.Accounts.FirstOrDefault(a => a.Email == account.Email);
                if (existingUser != null)
                {
                    return BadRequest("Email already exists");
                }

                // Thêm tài khoản mới vào cơ sở dữ liệu
                _dbContext.Accounts.Add(account);
                _dbContext.SaveChanges();

                // Tạo phản hồi thành công và chuyển hướng đến trang đăng nhập
                return Ok(new { RedirectUrl = "/sign-in" });
            }
            catch (Exception ex)
            {
                // Xử lý lỗi nếu có
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
        }

        // AccountController.cs
        [HttpGet]
        [Route("GetAccountDetails")]
        public IActionResult GetAccountDetails([FromQuery] string email)
        {
            try
            {
                if (string.IsNullOrEmpty(email))
                {
                    return BadRequest("Invalid email addresses");
                }

                var emailAddresses = email.Split(',');

                var emailDetails = _dbContext.Accounts.Where(c => emailAddresses.Contains(c.Email)).ToList();

                return Ok(emailDetails);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut]
        [Route("UpdateAccount")]
        public async Task<IActionResult> UpdateAccount(Account objAccount)
        {
            Console.WriteLine(objAccount);
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
                var existingAccount = await _dbContext.Accounts.FindAsync(objAccount.Email);

                if (existingAccount == null)
                {
                    return NotFound("Account not found");
                }
                existingAccount.Name = objAccount.Name;


                // Lưu các thay đổi vào cơ sở dữ liệu
                await _dbContext.SaveChangesAsync();

                // Trả về thông tin khách hàng đã được cập nhật
                return Ok(existingAccount);
            }
            catch (Exception ex)
            {
                // Xử lý lỗi và trả về lỗi 500 nếu có lỗi xảy ra
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        //[HttpGet]
        //[Route("GetPoints")]
        //public IActionResult GetPoints([FromQuery] string email)
        //{
        //    try
        //    {
        //        if (string.IsNullOrEmpty(email))
        //        {
        //            return BadRequest("Invalid email addresses");
        //        }

        //        var emailAddresses = email.Split(',');

        //        var count = _dbContext.Tickets.Count(c => emailAddresses.Contains(c.Mail));

        //        return Ok(count);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"Internal server error: {ex.Message}");
        //    }
        //}


    }
}
