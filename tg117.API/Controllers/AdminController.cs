using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using tg117.API.Dtos;
using tg117.Domain;
using static tg117.API.Classes.GenericClass;

namespace tg117.API.Controllers
{
    [Authorize(Roles = Roles.RoleAdmin)]
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
        public async Task<IActionResult> RegisterAdminUser([FromBody] RegisterDto model)
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
                if (!_roleManager.RoleExistsAsync(roleName: Roles.RoleAdmin).GetAwaiter().GetResult())
                {
                    _ = _roleManager.CreateAsync(new IdentityRole(Roles.RoleAdmin)).GetAwaiter().GetResult();
                }
                _ = await _userManager.AddToRoleAsync(user, Roles.RoleAdmin);
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
        public async Task<IActionResult> AssignRole([FromBody] UserRoleDto model)
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
        public async Task<IActionResult> GetUsers([FromBody] QueryParams queryParams)
        {
            if (queryParams == null)
            {
                return BadRequest("User not found");
            }
            IQueryable<AppUser> query = _userManager.Users;

            PagedList<AppUser> tt =
                    await PagedList<AppUser>
                        .CreateAsync
                            (query,
                                queryParams.PageNumber,
                                queryParams.PageSize
                            )
                     ;

            return new JsonResult(tt);
        }

        [HttpGet("GetRoles")]
        public async Task<IActionResult> GetRoles([FromBody] QueryParams queryParams)
        {
            if (queryParams == null)
            {
                return BadRequest("User not found");
            }
            IQueryable<IdentityRole> query = _roleManager.Roles;

            PagedList<IdentityRole> tt =
                    await PagedList<IdentityRole>
                        .CreateAsync
                            (query,
                                queryParams.PageNumber,
                                queryParams.PageSize
                            )
                     ;

            return new JsonResult(tt);
        }

        [HttpGet("UserDetails")]
        public async Task<IActionResult> UserDetails([FromBody] string username)
        {
            if (username == null)
            {
                return BadRequest("User not found");
            }
            AppUser? user = await _userManager.FindByNameAsync(username);
            if (user == null)
            {
                return BadRequest("User not found");
            }

            return new JsonResult(user);
        }

        [HttpGet("UsersInRole")]
        public async Task<IActionResult> UsersInRole([FromBody] string roleName)
        {
            if (roleName == null)
            {
                return BadRequest("User not found");
            }

            IList<AppUser> result = await _userManager.GetUsersInRoleAsync(roleName);

            return new JsonResult(result);
        }

        #endregion AllGetRequest
    }
}