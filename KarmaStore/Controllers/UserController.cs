using KarmaStore.DTO;
using KarmaStore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;

namespace KarmaStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ShopDbContext _context;
        private readonly AppSettings _appSettings;
        public UserController(ShopDbContext context, IOptionsMonitor<AppSettings> optionsMonitor)
        {
            _context = context;
            _appSettings = optionsMonitor.CurrentValue;
        }

        [HttpPost("Login")]
        public IActionResult Validate(Login_Model model)
        {
            var user = _context.Users.SingleOrDefault(p => p.Email == model.Email && model.Password == p.Password);
            if (user == null)
            {
                return Ok(new ApiResponse
                {
                    Success = false,
                    Message = "Invalid Email/Password"
                });
            }
            //cap token
            return Ok(new ApiResponse
            {
                Success = true,
                Message = "Authenticate success",
                Data = null
            });

        }

        private string GenerateToken(DTO_User user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            return null;
        }
    }
}
