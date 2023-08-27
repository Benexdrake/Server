using MongoDB.Driver;
using Server.Models;

namespace Server.Data;

public class IMDbDBContext
{
    public IMongoCollection<Movie> Movies { get; set; }

    public IMDbDBContext(IServiceProvider service)
    {
        var config = service.GetRequiredService<IConfiguration>();
        var client = service.GetRequiredService<MongoClient>();

        var db = client.GetDatabase(config["MongoDb:DatabaseNames:IMDb"]);

        Movies = db.GetCollection<Movie>(config["MongoDb:CollectionNames:Movies"]);
    }
}
