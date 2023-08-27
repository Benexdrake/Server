using MongoDB.Bson;

namespace Server.Models;
public class Pokemon
{
    public ObjectId Id { get; set; }
    public int nr { get; set; }
    public string name { get; set; }
    public string imageUrl { get; set; }
    public List<string> descriptions { get; set; }
    public string height { get; set; }
    public string category { get; set; }
    public string weight { get; set; }
    public List<Ability> abilities { get; set; }
    public List<string> genders { get; set; }
    public List<string> types { get; set; }
    public List<string> weaknesses { get; set; }
    public List<int> evolutionIds { get; set; }
    public int hp { get; set; }
    public int attack { get; set; }
    public int defense { get; set; }
    public int specialAttack { get; set; }
    public int specialDefense { get; set; }
    public int speed { get; set; }
    public bool variant { get; set; }
}

public class Ability
{
    public string name { get; set; }
    public string description { get; set; }
}
