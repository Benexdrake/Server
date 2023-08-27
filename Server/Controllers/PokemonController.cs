using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using Server.Data;

namespace Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PokemonController : ControllerBase
{
    private readonly PokemonDBContext _context;
    private readonly Random rand;
    public PokemonController(IServiceProvider service)
    {
        _context = service.GetRequiredService<PokemonDBContext>();
        rand = new Random();
    }

    [HttpGet]
    public async Task<ActionResult> GetAllPokemons()
    {
        var pokemons = _context.Pokemons.Find(_ => true).ToList();
        return Ok(pokemons);
    }

    [HttpGet("nr")]
    public async Task<ActionResult> GetPokemonsById(int nr)
    {
        var pokemons = _context.Pokemons.Find(_ => true).ToList().Where(x => x.nr == nr);
        return Ok(pokemons);
    }
}
