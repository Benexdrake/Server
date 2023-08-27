using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Server.DL;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscordIdentityController : ControllerBase
    {
        private readonly DiscordLogic _logic;

        public DiscordIdentityController(DiscordLogic logic)
        {
            _logic = logic;
        }
        [HttpGet("token")]
        public async Task<ActionResult> GetToken(string code, string redirectUrl)
        {
            var token = await _logic.GetToken(code, redirectUrl);
            return Ok(token);
        }

        [HttpPost("user")]
        public async Task<ActionResult> GetUser(string token)
        {
            var user = await _logic.GetUser(token);
            return Ok(user);
        }
    }
}
