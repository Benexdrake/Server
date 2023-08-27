using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;


namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AboutMeController : ControllerBase
    {
        private readonly string pw;
        public AboutMeController(IConfiguration conf)
        {
            pw = conf["pw"];
        }
        [HttpGet]
        public async Task<ActionResult> GetMe(string password)
        {
            if(password.Equals(pw))
            {
                using StreamReader sr = new StreamReader("EC03EE70-CBA9-4CC7-9BD9-03087C6CBED6.json");
                var json = sr.ReadToEnd();
                var result = JsonConvert.DeserializeObject(json);
                return Ok(json);
            }
            return Ok();
        }
    }
}
