using KarmaStore.DTO;
using KarmaStore.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration.UserSecrets;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using BCrypt.Net;
using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;

namespace KarmaStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ShopDbContext _context;
        public static User_Model user = new User_Model();
        private readonly IConfiguration _configuration;
        public AuthController(ShopDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
       
        [HttpPost("Register")]

        public async Task<ActionResult<User_Model>> Register(User_Model model)
        {
            
            try 
            { 
            // validate
            var validateEmail = _context.Users.SingleOrDefault(x => x.Email == model.Email);
            if (validateEmail != null)
                return BadRequest("Email has already used!");
            
                if (ModelState.IsValid)
                {
                    DTO_User user = new DTO_User
                    {
                        Email = model.Email,
                        Password = Sha1(model.Password),
                        Phone = model.Phone,
                        Address = model.Address,
                        Name = model.Name
                    };
                    _context.Add(user);
                    await _context.SaveChangesAsync();
                    return Ok(user);

                }


            }
            catch
            {
                return BadRequest();
            }
            return Ok();

        }

        [HttpPost("Login")]
        public async Task<ActionResult> Login(Login_Model model)
        {
            string passWord = Sha1(model.Password);
            var userDB = _context.Users.Where(x => x.Email == model.Email && x.Password == passWord).FirstOrDefault();
            if(userDB == null)
            {
                return BadRequest("Email or Password is incorrect!");
            }
            string token = CreateToken(model);
            return Ok(token);
        }

        [HttpPost("Login2")]
        public async Task<IActionResult> Login(string email, string password)
        {
           
            if(email != null && password != null)
            {
                DTO_User user = await GetUser(email, password);
                if(user != null)
                {
                    var claims = new[]
                    {
                    new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                    new Claim("Name", user.Name),
                    new Claim("Email", user.Email),
                    new Claim("Role", user.Role)
                    };
                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(
                        _configuration["Jwt:Issuer"],
                        _configuration["Jwt:Audience"],
                        claims,
                        expires: DateTime.UtcNow.AddMinutes(10),
                        signingCredentials: signIn);

                    return Ok(new JwtSecurityTokenHandler().WriteToken(token));
                }
                else
                {
                    return BadRequest("Invalid credentials");
                }
            }
            else
            {
                return BadRequest();
            }
        }

        private async Task<DTO_User> GetUser(string email, string password)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email && u.Password == password);
        }


        public static string Sha1(string password)
        {
            using (var hash = SHA1.Create())
            {
                var bytes = hash.ComputeHash(Encoding.UTF8.GetBytes(password));
                var sb = new StringBuilder();
                foreach (byte b in bytes)
                {
                    sb.AppendFormat("{0:x2}", b);
                }
                return sb.ToString();
            }
        }
       
        private string CreateToken(Login_Model model)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, model.Email)
            };
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }


        //[HttpPost]
        //public async Task<IActionResult> Login(string email, string pass)
        //{
        //    if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(pass))
        //    {
        //        return BadRequest(new { Stustus = false, Message = "Email và mật khẩu không được để trống" });
        //    }
        //    var model = LoginUser(email, Sha1(pass));

        //    if (model != null && model.MemberID != 0)
        //    {
        //        var claims = new List<Claim>
        //        {
        //            new Claim(ClaimTypes.Name, "Name"),
        //            new Claim(ClaimTypes.Email, model.Email),
        //            new Claim("Pass", pass),
        //            new Claim(ClaimTypes.NameIdentifier, model.MemberID.ToString())
        //        };
        //        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

        //        var authProperties = new AuthenticationProperties
        //        {
        //        };
        //        // sign-in
        //        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

        //        return Json(new { Status = true, Data = "/" });
        //    }
        //    return Json(new { Status = false, Message = "Email hoặc mật khẩu đăng nhập không đúng." });
        //}
        //private MemberEntity LoginUser(string email, string passWord)
        //{
        //    return Singleton.Instance<MemberService>().GetMemberByEmailAndPassWordyId(email, passWord);


        //}
        

        
    }
}
