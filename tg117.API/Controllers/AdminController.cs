using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using tg117.API.Models.Account;
using tg117.Domain;

namespace tg117.API.Controllers
{
    [Authorize(Roles = roles.RoleAdmin)]
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;

        public AdminController(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok("You have accessed the Admin controller.");
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAdminUser([FromBody] Register model)
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
                if (!_roleManager.RoleExistsAsync(roleName: roles.RoleAdmin).GetAwaiter().GetResult())
                {
                    _ = _roleManager.CreateAsync(new IdentityRole(roles.RoleAdmin)).GetAwaiter().GetResult();
                }
                _ = await _userManager.AddToRoleAsync(user, roles.RoleAdmin);
                return Ok(new { message = "User registered as Admin Role successfully" });
            }

            return BadRequest(result.Errors);
        }

        [HttpPost("add-role")]
        public async Task<IActionResult> AddRole([FromBody] string role)
        {
            if (!await _roleManager.RoleExistsAsync(role))
            {
                IdentityResult result = await _roleManager.CreateAsync(new IdentityRole(role));
                if (result.Succeeded)
                {
                    return Ok(new { message = "Role added successfully" });
                }

                return BadRequest(result.Errors);
            }

            return BadRequest("Role already exists");
        }

        [HttpPost("assign-role")]
        public async Task<IActionResult> AssignRole([FromBody] UserRole model)
        {
            AppUser? user = await _userManager.FindByNameAsync(model.Username);
            if (user == null)
            {
                return BadRequest("User not found");
            }

            IdentityResult result = await _userManager.AddToRoleAsync(user, model.Role);
            if (result.Succeeded)
            {
                return Ok(new { message = "Role assigned successfully" });
            }

            return BadRequest(result.Errors);
        }

        #region AllGetRequest

        [HttpGet("GetUsers")]
        public async Task<IActionResult> GetUsers([FromBody] UserRole model)
        {
            AppUser? user = await _userManager.FindByNameAsync(model.Username);
            if (user == null)
            {
                return BadRequest("User not found");
            }

            IdentityResult result = await _userManager.AddToRoleAsync(user, model.Role);
            if (result.Succeeded)
            {
                return Ok(new { message = "Role assigned successfully" });
            }

            return BadRequest(result.Errors);
        }

        [HttpGet("GetRoles")]
        public async Task<IActionResult> GetRoles([FromBody] UserRole model)
        {
            AppUser? user = await _userManager.FindByNameAsync(model.Username);
            if (user == null)
            {
                return BadRequest("User not found");
            }

            IdentityResult result = await _userManager.AddToRoleAsync(user, model.Role);
            if (result.Succeeded)
            {
                return Ok(new { message = "Role assigned successfully" });
            }

            return BadRequest(result.Errors);
        }

        [HttpGet("UserDetails")]
        public async Task<IActionResult> UserDetails([FromBody] UserRole model)
        {
            AppUser? user = await _userManager.FindByNameAsync(model.Username);
            if (user == null)
            {
                return BadRequest("User not found");
            }

            IdentityResult result = await _userManager.AddToRoleAsync(user, model.Role);
            if (result.Succeeded)
            {
                return Ok(new { message = "Role assigned successfully" });
            }

            return BadRequest(result.Errors);
        }

        [HttpGet("UsersInRole")]
        public async Task<IActionResult> UsersInRole([FromBody] UserRole model)
        {
            AppUser? user = await _userManager.FindByNameAsync(model.Username);
            if (user == null)
            {
                return BadRequest("User not found");
            }

            IdentityResult result = await _userManager.AddToRoleAsync(user, model.Role);
            if (result.Succeeded)
            {
                return Ok(new { message = "Role assigned successfully" });
            }

            return BadRequest(result.Errors);
        }

        #endregion AllGetRequest
    }
}