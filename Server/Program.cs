using MongoDB.Driver;
using Server.Data;
using Server.DL;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<MongoClient>(new MongoClient(builder.Configuration["MongoDb:IP"]));
builder.Services.AddScoped<CrunchyrollDBContext>();
builder.Services.AddScoped<PokemonDBContext>();
builder.Services.AddScoped<IMDbDBContext>();
builder.Services.AddScoped<DiscordLogic>();

builder.Services.AddHttpClient();


var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddCors(o =>
{
    o.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.AllowAnyHeader().AllowAnyOrigin();
                      });
});

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI();


app.UseCors(MyAllowSpecificOrigins);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
