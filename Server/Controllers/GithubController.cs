using Microsoft.AspNetCore.Mvc;
using Octokit;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GithubController : ControllerBase
    {
        readonly GitHubClient client;
        public GithubController(IConfiguration conf)
        {
            var tokenAuth = new Credentials(conf["github"]); // NOTE: not real token
            client = new GitHubClient(new ProductHeaderValue("ok"));
            client.Credentials = tokenAuth;
        }

        [HttpGet("user")]
        public async Task<ActionResult> GetUser(string username)
        {
            var user = await client.User.Get("benexdrake");
            return Ok(user);
        }

        [HttpGet("projects")]
        public async Task<ActionResult> GetProjects(string username)
        {
            var rep = await client.Repository.GetAllForUser("benexdrake");
            return Ok(rep);
        }
    }
}
