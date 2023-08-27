using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using Server.Data;
using Server.Models;

namespace Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class IMDbController : ControllerBase
{
    private readonly IMDbDBContext _context;
    private readonly Random rand;
    public IMDbController(IServiceProvider service)
    {
        _context = service.GetRequiredService<IMDbDBContext>();
        rand = new Random();
    }

    [HttpGet("movies")]
    public async Task<ActionResult> GetAllMovies()
    {
        var movies = _context.Movies.FindAsync(_ => true).Result.ToList();
        return Ok(movies);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetMovieById(string id)
    {
        var filter = Builders<Movie>.Filter.Eq(x => x._id, id);
        var movie = _context.Movies.FindAsync(filter).Result.FirstOrDefault();
        return Ok(movie);
    }

    [HttpGet("movierandom")]
    public async Task<ActionResult> GetMovieByRandom()
    {
        var movies = _context.Movies.FindAsync(_ => true).Result.ToList();
        var movie = movies[rand.Next(0, movies.Count)];
        return Ok(movie);
    }

    [HttpGet("moviebyname")]
    public async Task<ActionResult> GetMovieByName(string title)
    {
        var filter = Builders<Movie>.Filter.Where(x => x.title.Contains(title));
        var movies = _context.Movies.FindAsync(filter).Result.ToList();
        return Ok(movies);
    }

    [HttpGet("moviebygenre")]
    public async Task<ActionResult> GetMovieByGenre(string genre)
    {
        var filter = Builders<Movie>.Filter.Where(x => x.genres.Contains(genre));
        var movies = _context.Movies.FindAsync(filter).Result.ToList();
        return Ok(movies);
    }

    [HttpGet("moviebyrating")]
    public async Task<ActionResult> GetMovieByRating(double rating)
    {
        //var filter = Builders<Movie>.Filter.AnyGte("rating", rating);
        //var movies = _context.Movies.FindAsync(filter).Result.ToList();
        //return Ok(movies);
        return Ok();
    }

    [HttpGet("moviebycast")]
    public async Task<ActionResult> GetMovieByCast(string name)
    {
        var filter = Builders<Movie>.Filter.Where(x => x.mainCast.Contains(name));
        var movies = _context.Movies.FindAsync(filter).Result.ToList();
        if (movies.Count > 0)
            return Ok(movies);
        return BadRequest();
    }
}
