using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using tg117.API.Models.Account;
using tg117.Domain;

namespace tg117.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;

        public AccountController(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] Register model)
        {
            AppUser user = new()
            {
                UserName = model.Username,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                MiddelName = model.MiddelName,
                City = model.City,
                State = model.State,
                Country = model.Country,
                PostalCode = model.PostalCode
            };
            IdentityResult result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                if (!_roleManager.RoleExistsAsync(roleName: roles.RoleBasic).GetAwaiter().GetResult())
                {
                    _ = _roleManager.CreateAsync(new IdentityRole(roles.RoleBasic)).GetAwaiter().GetResult();
                }
                _ = await _userManager.AddToRoleAsync(user, roles.RoleBasic);
                return Ok(new { message = "User registered successfully" });
            }

            return BadRequest(result.Errors);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Login model)
        {
            AppUser? user = await _userManager.FindByNameAsync(model.Username);
            if (user == null)
            {
                user = await _userManager.FindByEmailAsync(model.Username);
            }
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                IList<string> userRoles = await _userManager.GetRolesAsync(user);

                List<Claim> authClaims = new()
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.UserName!),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

                authClaims.AddRange(userRoles.Select(role => new Claim(ClaimTypes.Role, role)));

                JwtSecurityToken token = new(
                    issuer: _configuration["Jwt:Issuer"],
                    expires: DateTime.Now.AddMinutes(double.Parse(_configuration["Jwt:ExpiryMinutes"]!)),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!)),
                    SecurityAlgorithms.HmacSha256));

                //tg117.API.Service.MimeKit mimeKit = new();
                //mimeKit.SendEmailFromMailKit("PradeepkMahor@gmail.com", "User Login", $@"User Nmae : {user.UserName}, Email :  {user.Email} and Jwt :{token.ToString()}");
                //tg117.API.Service.EmailService emailService = new();

                //emailService.SendEmail("PradeepkMahor@gmail.com", "User Login", $@"User Nmae : {user.UserName}, Email :  {user.Email} and Jwt :{token.ToString()}");

                return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
            }

            return Unauthorized();
        }
    }
}