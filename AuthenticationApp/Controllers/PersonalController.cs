using AuthenticationApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonalController : Controller
    {
        [Authorize]
        [Route("getlogin")]
        public IActionResult GetLogin()
        {
            return Ok($"Your login: {User.Identity.Name}");
        }

        [Authorize(Roles = "Admin")]
        [Route("getrole")]
        public IActionResult GetAdminRole()
        {
            return Ok("Your role is: Administrator");
        }

        [Authorize(Roles = "Moderator")]
        [Route("getrole")]
        public IActionResult GetModeratorRole()
        {
            return Ok("Your role is: Moderator");
        }
    }
}