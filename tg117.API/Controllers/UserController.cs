using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using tg117.Domain;

namespace tg117.API.Controllers
{
    [Authorize(Roles = Roles.RoleBasic)]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("You have accessed the User controller.");
        }
    }
}