using MongoDB.Driver;

namespace Server.Data
{
    public class PokemonDBContext
    {
        public IMongoCollection<Models.Pokemon> Pokemons { get; set; }
        //public IMongoCollection<PokemonCard> PokemonCards { get; set; }

        public PokemonDBContext(IServiceProvider service)
        {
            var config = service.GetRequiredService<IConfiguration>();
            var client = service.GetRequiredService<MongoClient>();

            var db = client.GetDatabase(config["MongoDb:DatabaseNames:Pokemon"]);

            Pokemons = db.GetCollection<Models.Pokemon>(config["MongoDb:CollectionNames:PokemonDex"]);
            //PokemonCards = db.GetCollection<PokemonCard>(config["MongoDb:CollectionNames:PokemonCards"]);
        }
    }
}
