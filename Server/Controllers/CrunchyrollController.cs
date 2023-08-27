using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using Server.Data;

namespace Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CrunchyrollController : ControllerBase
{
    private readonly CrunchyrollDBContext _context;
    private readonly Random rand;
    private readonly string pw;
    public CrunchyrollController(IServiceProvider service)
    {
        _context = service.GetRequiredService<CrunchyrollDBContext>();
        pw = service.GetRequiredService<IConfiguration>()["pw"];
        rand = new Random();
    }

    [HttpGet]
    public async Task<ActionResult> GetAllAnimes()
    {
        var animes = _context.Animes.FindAsync(_ => true).Result.ToList<Models.Anime>();
        if (animes is not null)
            return Ok(animes);
        return BadRequest("No Animes Found");
    }

    [HttpGet]
    [Route("id")]
    public async Task<ActionResult> GetAnimeByID(string id)
    {
        var animes = _context.Animes.FindAsync(_ => true).Result.ToList<Models.Anime>();
        var anime = animes.Where(x => x._id == id).FirstOrDefault();
        return Ok(anime);
    }

    // Anime Eintragen oder Updaten
    [HttpPost]
    public async Task<ActionResult> CreateOrUpdate(Models.Anime a, string password)
    {
        if(password.Equals(pw))
        {
            var anime = _context.Animes.FindAsync(_ => true).Result.ToList().Where(x => x._id.Equals(a._id)).FirstOrDefault();
            if (anime is not null)
                await UpdateAnime(a);
            else
                await CreateAnime(a);
        return Ok();
        }
        return Ok("False Password");
    }

    private async Task CreateAnime(Models.Anime anime)
    {
        await _context.Animes.InsertOneAsync(anime);
    }
    private async Task UpdateAnime(Models.Anime anime)
    {
        var filter = Builders<Models.Anime>.Filter.Eq(x => x._id, anime._id);

        await _context.Animes.ReplaceOneAsync(filter, anime, new ReplaceOptions { IsUpsert = true });
    }
}
