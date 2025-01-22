using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using tg117.API.Dtos;
using tg117.Domain;

namespace tg117.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;

        private readonly ILogger<AccountController> _logger;

        private readonly RoleManager<IdentityRole> _roleManager;

        private readonly IConfiguration _configuration;

        public AccountController(UserManager<AppUser> userManager,
        RoleManager<IdentityRole> roleManager,
        IConfiguration configuration, ILogger<AccountController> logger
        )
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _logger = logger;
        }

        // api/account/register

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult<string>> Register(RegisterDto registerDto)
        {
            try
            {
                if (registerDto == null)
                {
                    return BadRequest("Details is null");
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid details " + ModelState);
                }

                var user = new AppUser
                {
                    Email = registerDto.Email,
                    UserName = registerDto.Username,
                    FirstName = registerDto.FirstName,
                    LastName = registerDto.LastName,
                    MiddelName = registerDto.MiddelName,
                    City = registerDto.City,
                    State = registerDto.State,
                    Country = registerDto.Country,
                    PostalCode = registerDto.PostalCode
                };

                var result = await _userManager.CreateAsync(user, registerDto.Password);

                if (!result.Succeeded)
                {
                    return BadRequest(result.Errors);
                }

                if (registerDto.Roles is null)
                {
                    await _userManager.AddToRoleAsync(user, Roles.RoleBasic);
                }
                else
                {
                    foreach (var role in registerDto.Roles)
                    {
                        await _userManager.AddToRoleAsync(user, role);
                    }
                }

                return Ok(new AuthResponseDto
                {
                    IsSuccess = true,
                    Message = "Account Created Successfully!"
                });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside the Register action: {ex}");
                return StatusCode(500, "Internal server error");
            }
        }

        //api/account/login
        [AllowAnonymous]
        [HttpPost("login")]
        [ResponseCache(Duration = 60, VaryByQueryKeys = new[] { "login" })]
        public async Task<ActionResult<AuthResponseDto>> Login(LoginDto loginDto)
        {
            try
            {
                if (loginDto == null)
                {
                    return BadRequest("Details is null");
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid details " + ModelState);
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                AppUser? user = await _userManager.FindByNameAsync(loginDto.Username);
                if (user == null)
                {
                    user = await _userManager.FindByEmailAsync(loginDto.Username);
                }

                if (user is null)
                {
                    return Unauthorized(new AuthResponseDto
                    {
                        IsSuccess = false,
                        Message = "User not found",
                    });
                }

                var result = await _userManager.CheckPasswordAsync(user, loginDto.Password);

                if (!result)
                {
                    _logger.LogError($"Something went wrong inside the Login action: Invalid Password");
                    return Unauthorized(new AuthResponseDto
                    {
                        IsSuccess = false,
                        Message = "Invalid Password."
                    });
                }

                var token = GenerateToken(user);

                return Ok(new AuthResponseDto
                {
                    Token = token,
                    IsSuccess = true,
                    Message = "Login Success."
                });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside the CreateOwner action: {ex}");
                return StatusCode(500, "Internal server error");
            }
        }

        private string GenerateToken(AppUser user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII
            .GetBytes(_configuration.GetSection("JWTSetting").GetSection("securityKey").Value!);

            var roles = _userManager.GetRolesAsync(user).Result;

            List<Claim> claims =
            [
                new (JwtRegisteredClaimNames.Email,user.Email??""),
                new (JwtRegisteredClaimNames.UniqueName,user.UserName??""),
                new (JwtRegisteredClaimNames.Name,user.FirstName??""),
                new (JwtRegisteredClaimNames.Name,user.LastName??""),
                new (JwtRegisteredClaimNames.NameId,user.Id ??""),
                new (JwtRegisteredClaimNames.Aud,
                _configuration.GetSection("JWTSetting").GetSection("validAudience").Value!),
                new (JwtRegisteredClaimNames.Iss,_configuration.GetSection("JWTSetting").GetSection("validIssuer").Value!)
            ];

            foreach (var role in roles)

            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256
                )
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        //api/account/detail
        [HttpGet("detail")]
        public async Task<ActionResult<UserDetailDto>> GetUserDetail()
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(currentUserId!);

            if (user is null)
            {
                return NotFound(new AuthResponseDto
                {
                    IsSuccess = false,
                    Message = "User not found"
                });
            }

            return Ok(new UserDetailDto
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                MiddleName = user.MiddelName,
                Roles = [.. await _userManager.GetRolesAsync(user)],
                PhoneNumber = user.PhoneNumber,
                PhoneNumberConfirmed = user.PhoneNumberConfirmed,
                AccessFailedCount = user.AccessFailedCount,
            });
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDetailDto>>> GetUsers()
        {
            var users = await _userManager.Users.Select(u => new UserDetailDto
            {
                Id = u.Id,
                Email = u.Email,
                FirstName = u.FirstName,
                LastName = u.LastName,
                MiddleName = u.MiddelName,
                Roles = _userManager.GetRolesAsync(u).Result.ToArray()
            }).ToListAsync();

            return Ok(users);
        }
    }
}