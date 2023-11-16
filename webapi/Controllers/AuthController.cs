using cndcAPI.Models;
using cndcAPI.Oracle;
using cndcAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace cndcAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public User user = new User();
        public static Dictionary<string ,User> users = new Dictionary<string ,User>();
        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;

        public AuthController(IConfiguration configuration, IUserService userService)
        {
            _configuration = configuration;
            _userService = userService;
        }

        [HttpGet, Authorize]
        public ActionResult<string> GetMyName()
        {
            return Ok(_userService.GetMyName());

            //var userName = User?.Identity?.Name;
            //var roleClaims = User?.FindAll(ClaimTypes.Role);
            //var roles = roleClaims?.Select(c => c.Value).ToList();
            //var roles2 = User?.Claims
            //    .Where(c => c.Type == ClaimTypes.Role)
            //    .Select(c => c.Value)
            //    .ToList();
            //return Ok(new { userName, roles, roles2 });
        }

        [HttpPost("register"),  Authorize(Roles = "Admin")]
        public ActionResult<User> Register(UserDto request)
        {

            //user = UserOracle.Instance.GetUser(request);
            user = UserOracle.Instance.RegisterUser(request);

            /*
            string passwordHash
                = BCrypt.Net.BCrypt.HashPassword(request.Password);
            user = new User();
            user.Username = request.Username;
            user.PasswordHash = passwordHash;

            if (!users.ContainsKey(request.Username) ) {
            users.Add(request.Username, user);
            }
            else
            {
                user = users[request.Username];
            }*/


            return Ok(user);
        }

        [HttpPost("login")]
        public ActionResult<User> Login(UserDto request)
        {

          user =  UserOracle.Instance.GetUser(request);
            if (user == null) { return BadRequest("User not found."); }

            if (!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            {
                return BadRequest("Wrong password.");
            }
            
       

            string token = CreateToken(user);

            return Ok(token);
        }

        private string CreateToken(User user)
        {
            List<Claim> claims = null;
            
            if (user.Rol ==1 )
            {
                claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.Role, "Admin"),
                    new Claim(ClaimTypes.Role, "User"),
                };
            }
                else
                {
                claims = new List<Claim> {
                new Claim(ClaimTypes.Name, user.Username),

                new Claim(ClaimTypes.Role, "User"),
                        };
                }
                
              
 
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.Now.AddDays(1),
                    signingCredentials: creds
                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
    }
}
